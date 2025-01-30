using CoreReleaseAutomation.Data;
using CoreReleaseAutomation.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CoreReleaseAutomation.Repositories
{
    //TODO: Why DI Core is complaning about abstract class here????
    public class Repository<T> : IRepository<T>  where T : class
    {
        private readonly DbContext Context;

        public Repository(DbContext applicationDataContext)
        {
            Context = applicationDataContext;
        }

        public async Task<T> GetById(string id) => await Context.Set<T>().FindAsync(id);        

        public Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate) => Context.Set<T>().FirstOrDefaultAsync(predicate);

        public async Task Add(T entity) => await Context.Set<T>().AddAsync(entity);                    

        public void Update(T entity) => Context.Entry(entity).State = EntityState.Modified;
            
        public void Remove(T entity) => Context.Set<T>().Remove(entity);
            
        public async Task<IEnumerable<T>> GetAll() => await Context.Set<T>().ToListAsync();

        public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate) => await Context.Set<T>().Where(predicate).ToListAsync();

        public Task<int> CountAll() => Context.Set<T>().CountAsync();

        public Task<int> CountWhere(Expression<Func<T, bool>> predicate) => Context.Set<T>().CountAsync(predicate);     
    }
}
