using System.IO;
using System.Xml;

namespace CoreReleaseAutomation.Helpers
{
    public static class JobTrackerHelper
    {
        public static string SendJobTracker(string user, string releaseDescription, string pathDestiny, string releaseName, 
                                            string operatorName, string password, string releaseNumber, 
                                            string inteportUrl, string xmlForder)
        {                        
            string request = File.ReadAllText(Path.Combine(xmlForder, @"Xmls\jobtracker.xml"));

            request = request.Replace("#User", user)                   
                   .Replace("#ReleaseDescription", releaseDescription)
                   .Replace("#PathDestiny", pathDestiny)
                   .Replace("#ReleaseName", releaseName)                   
                   .Replace("#Operator", operatorName)
                   .Replace("#password", password)                   
                   .Replace("#ReleaseNumber", releaseNumber);

            var xml = new XmlDocument();

            xml.LoadXml(request);

            return Services.InteportService.ExecuteRequestAsync(inteportUrl, xml).ToString();
        }

        public static bool CheckStatusJobTracker(string inteportUrl, string request)
        {
            var xml = new XmlDocument();

            xml.LoadXml(request);

            var response = Services.InteportService.ExecuteRequestAsync(inteportUrl, xml);

            if (response.SelectSingleNode("inteflow/response/application/result/cd_status_current/@tx_description").InnerText == "Pending UAT")
            {
                return true;
            }
            else
            {
                return false;
            }            
        }
    }
}
