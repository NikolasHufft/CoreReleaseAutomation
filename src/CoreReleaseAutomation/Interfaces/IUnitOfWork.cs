using CoreReleaseAutomation.Models;
using CoreReleaseAutomation.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreReleaseAutomation.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> Commit();
        ILogVersionRepository LogVersionRepository { get; }
        IReleaseRepository ReleaseRepository { get; }
    }
}
