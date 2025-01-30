using CoreReleaseAutomation.Data;
using CoreReleaseAutomation.Interfaces;
using CoreReleaseAutomation.Models;
using CoreReleaseAutomation.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace CoreRelease.Repositories
{
    public class ReleaseRepository : Repository<Release>, IReleaseRepository
    {
        private readonly ApplicationDataContext _context;

        public ReleaseRepository(ApplicationDataContext context) : base(context)
        {
            _context = context;
        }
        
        public Release GetReleaseWaitingApprovalById(string id)
        {
            return (from item in _context.Releases select item).Where(p => p.Status != "Pending UAT" && p.ReleaseId == id).FirstOrDefault();
        }

        public IEnumerable<Release> GetAllReleaseWaitingApproval()
        {
            return (from items in _context.Releases select items).Where(p => p.Status != "Pending UAT").ToList().AsReadOnly();
        }
    }
}
