using System.Collections.Generic;
using System.IO;

namespace CoreReleaseAutomation.Helpers
{
    public static class FileHelper
    {
        public static void CloneDirectory(string root, string dest)
        {
            foreach (var directory in Directory.GetDirectories(root))
            {
                string dirName = Path.GetFileName(directory);
                if (!Directory.Exists(Path.Combine(dest, dirName)))
                {
                    Directory.CreateDirectory(Path.Combine(dest, dirName));
                }
                CloneDirectory(directory, Path.Combine(dest, dirName));
            }

            foreach (var file in Directory.GetFiles(root))
            {
                File.Copy(file, Path.Combine(dest, Path.GetFileName(file)));
            }
        }

        public static string CreateRootFolder(string path)
        {
            if (Directory.Exists(path)) Directory.Delete(path, true);
            
            Directory.CreateDirectory(path);

            return "\nCreate Release Folders completed";
        }

        public static string AppendVersionToFile(List<string> paths, string version) 
        {            
            foreach (var path in paths)
            {
                if (!File.Exists(path))
                {
                    File.Create(path);
                }
                using StreamWriter sw = File.AppendText(path);
                sw.WriteLine(version);
            }

            return "\nAppend Version To TXT Files completed";
        }

        public static string DeleteConfigFiles(List<string> paths, string version)
        {
            foreach (var path in paths)
            {
                if (!File.Exists(path)) File.Delete(path);
            }

            return "\nDelete Config Files completed";
        }

        public static string DeleteEndOfSprintAndHotFixFolder(List<string> paths)
        {
            foreach (var path in paths)
            {
                if(Directory.Exists(path)) Directory.Delete(path, true);
            }

            return "\nDelete End Of Sprint And HotFix Folder Files and folder completed";
        }
    }
}
