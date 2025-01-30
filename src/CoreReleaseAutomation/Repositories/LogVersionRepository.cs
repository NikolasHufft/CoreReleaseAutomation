using CoreReleaseAutomation.Data;
using CoreReleaseAutomation.Interfaces;
using CoreReleaseAutomation.Models;
using System.Linq;

namespace CoreReleaseAutomation.Repositories
{
    public class LogVersionRepository : Repository<LogVersion>, ILogVersionRepository
    {
        private readonly ApplicationDataContext _context;

        public LogVersionRepository(ApplicationDataContext context) : base(context)
        {
            _context = context;
        }

        public LogVersion GetNewVersion(string releaseType) 
        {
            int newSprint = 0;
            LogVersion newVersion = null;            
            var oldVersion = (from item in _context.LogVersions orderby item.Version descending select item).FirstOrDefault();
            var newPatch = "";

            if (releaseType == "Hotfix")
            {
                if ((oldVersion.Patch).Contains("."))
                {
                    var hotfixNumber = (oldVersion.Patch).Split('.');
                    int.TryParse(hotfixNumber[1], out newSprint);
                    newSprint++;
                    newPatch = $"{hotfixNumber[1]}.{newSprint.ToString()}";                    
                }
                else
                {
                    int.TryParse(oldVersion.Patch, out newSprint);
                    newSprint++;                    
                    newPatch = $"{newSprint.ToString()}.1";
                }
            }
            else 
            {
                if ((oldVersion.Patch).Contains("."))
                {
                    var endOfSprint = (oldVersion.Patch).Split('.');
                    int.TryParse(endOfSprint[0], out newSprint);                    
                }
                else
                {
                    int.TryParse(oldVersion.Patch, out newSprint);                    
                }

                newSprint++;
                newPatch = newSprint.ToString();
            }

            newVersion = new LogVersion() { Version = oldVersion.Version, Patch = newPatch };

            return newVersion;
        }

        public LogVersion GetOldVersion()
        {           
            return (from item in _context.LogVersions orderby item.Version descending select item).FirstOrDefault();
        }
    }
}
