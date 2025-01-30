using System.Collections.Generic;

namespace CoreReleaseAutomation.Interfaces
{
    public interface ISetup
    {
        string PathHotfix { get; }
        string PathEndOfSprint { get; }
        string PathDestiny { get; }
        string Description { get; }
        string JobTrackerUser { get; }
        string JobTrackerPassword { get; }
        string EmailFrom { get; }
        string EmailTo { get; }
        string EmailSubject { get; }
        string EmailBody { get; }
        string InterPortUrl { get; }
        string Smtp { get; }

        List<string> Managers { get; }
        List<string> StatusList { get; }
    }
}
