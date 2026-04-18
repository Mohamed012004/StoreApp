using Store.Route.Domains.Contracts;
using Store.Route.Domains.Entities;
using Store.Route.Persistence.Data.Contexts;
using Store.Route.Persistence.Repositories;
using System.Collections.Concurrent;

namespace Store.Route.Persistence
{
    public class UnitOfWork(StoreDbContext _context) : IUnitOfWork
    {
        //private Dictionary<string, object> _repositories = new Dictionary<string, object>();
        private ConcurrentDictionary<string, object> _repositories = new ConcurrentDictionary<string, object>();

        //public IGenericRepository<TKey, TEntity> GetRepository<TKey, TEntity>() where TEntity : BaseEntity<TKey>
        //{
        //    var type = typeof(TEntity).Name;
        //    if (!_repositories.ContainsKey(type))
        //    {
        //        var repository = new GenericRepository<TKey, TEntity>(_context);
        //        _repositories.Add(type, repository);
        //    }

        //    return (IGenericRepository<TKey, TEntity>) _repositories[type];
        //}



        public IGenericRepository<TKey, TEntity> GetRepository<TKey, TEntity>() where TEntity : BaseEntity<TKey>
        {

            return (IGenericRepository<TKey, TEntity>)_repositories.GetOrAdd(typeof(TEntity).Name, new GenericRepository<TKey, TEntity>(_context));
        }



        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
