using CoreRelease.Repositories;
using CoreReleaseAutomation.Data;
using CoreReleaseAutomation.Interfaces;
using CoreReleaseAutomation.Repositories;
using System;
using System.Threading.Tasks;

namespace CoreReleaseAutomation.UoW
{
    public class UnitOfWork : IUnitOfWork
    {        
        private readonly ApplicationDataContext _context;
        private bool disposed = false;
        
        private ILogVersionRepository logVersionRepository;
        private IReleaseRepository releaseRepository;

        public UnitOfWork(ApplicationDataContext context)
        {
            _context = context ?? throw new ArgumentNullException("Application Data Context is null");
        }
        
        public ILogVersionRepository LogVersionRepository
        {
            get
            {
                if (logVersionRepository == null) logVersionRepository = new LogVersionRepository(_context);

                return logVersionRepository;
            }
        }

        public IReleaseRepository ReleaseRepository
        {
            get
            {
                if (this.releaseRepository == null) this.releaseRepository = new ReleaseRepository(_context);

                return releaseRepository;
            }
        }

        public async Task<int> Commit() => await _context.SaveChangesAsync();            

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
