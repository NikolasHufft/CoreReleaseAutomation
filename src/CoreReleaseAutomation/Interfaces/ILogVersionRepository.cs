using CoreReleaseAutomation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreReleaseAutomation.Interfaces
{
    public interface ILogVersionRepository : IRepository<LogVersion>
    {
        LogVersion GetNewVersion(string releaseType);
        LogVersion GetOldVersion();
    }
}
