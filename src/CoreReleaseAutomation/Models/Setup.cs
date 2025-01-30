using CoreReleaseAutomation.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreReleaseAutomation.Models
{
    public class Setup : ISetup
    {
        private readonly IConfiguration config;
        
        public string PathHotfix { get; }
        public string PathEndOfSprint { get; }
        public string PathDestiny { get; }
        public string Description { get; }
        public string JobTrackerUser { get; }
        public string JobTrackerPassword { get; }
        public string EmailFrom { get; }
        public string EmailTo { get; }
        public string EmailSubject { get; }
        public string EmailBody { get; }
        public string InterPortUrl { get; }
        public string Smtp { get; }

        public List<string> Managers { get; }
        public List<string> StatusList { get; }


        public Setup(IConfiguration iConfig)
        {
            config = iConfig;            
            
            //List of Manager
            Managers = config.GetValue<string>("Setup:Managers").Split(',').ToList() ?? throw new ArgumentNullException("Manager List is empty");

            //Patch and Release Folder
            PathHotfix = config.GetSection("Setup").GetSection("PathHotfix").Value ?? throw new ArgumentNullException("Path Hotfix is empty");
            PathEndOfSprint = config.GetSection("Setup").GetSection("PathEndOfSprint").Value ?? throw new ArgumentNullException("Path EndOfSprint is empty");
            PathDestiny = config.GetSection("Setup").GetSection("PathDestiny").Value ?? throw new ArgumentNullException("Path Destiny is empty");
            
            //Default Description
            Description = config.GetSection("Setup").GetSection("Description").Value ?? throw new ArgumentNullException("Default Description is empty");
            
            //Job Tracker Configuration
            JobTrackerUser = config.GetSection("Setup").GetSection("JobTrackerUser").Value ?? throw new ArgumentNullException("Job Tracker User is empty");
            JobTrackerPassword = config.GetSection("Setup").GetSection("JobTrackerPassword").Value ?? throw new ArgumentNullException("Job Tracker Password is empty");

            //Email Configuration
            EmailFrom = config.GetSection("Setup").GetSection("EmailFrom").Value ?? throw new ArgumentNullException("EmailFrom is empty");
            EmailTo = config.GetSection("Setup").GetSection("EmailTo").Value ?? throw new ArgumentNullException("EmailTo is empty");
            EmailSubject = config.GetSection("Setup").GetSection("EmailSubject").Value ?? throw new ArgumentNullException("EmailSubject is empty");
            EmailBody = config.GetSection("Setup").GetSection("EmailBody").Value ?? throw new ArgumentNullException("Email Body is empty");
            Smtp = config.GetSection("Setup").GetSection("SMTP").Value ?? throw new ArgumentNullException("SMTP is empty");

            //Status List
            StatusList = config.GetValue<string>("Setup:StatusRelease").Split(',').ToList() ?? throw new ArgumentNullException("Status List is empty");

            //InterPort
            InterPortUrl = config.GetSection("Setup").GetSection("InterPortUrl").Value ?? throw new ArgumentNullException("InterPort Url is empty");
        }
    }    
}
