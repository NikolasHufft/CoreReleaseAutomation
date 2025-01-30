using CoreReleaseAutomation.Models;
using System.Collections.Generic;

namespace CoreReleaseAutomation.Interfaces
{
    public interface IReleaseRepository : IRepository<Release>
    {
        Release GetReleaseWaitingApprovalById(string id);
        IEnumerable<Release> GetAllReleaseWaitingApproval();
    }
}
