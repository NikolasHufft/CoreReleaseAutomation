using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreReleaseAutomation.ViewModels
{
    public class JobTrackerViewModel
    {
        public string Description { get; set; }
        public string ReleaseType { get; set; }
        public string ReleaseName { get; set; }
        public string Version { get; set; }
    }
}
