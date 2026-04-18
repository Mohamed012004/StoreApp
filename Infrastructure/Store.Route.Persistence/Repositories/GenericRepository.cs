using Microsoft.EntityFrameworkCore;
using Store.Route.Domains.Contracts;
using Store.Route.Domains.Entities;
using Store.Route.Persistence.Data.Contexts;

namespace Store.Route.Persistence.Repositories
{
    public class GenericRepository<TKey, TEntity>(StoreDbContext _context) : IGenericRepository<TKey, TEntity> where TEntity : BaseEntity<TKey>
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool changeTracker = false)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                return changeTracker ?
                 await _context.Products.Include(P => P.Brand).Include(P => P.Type).ToListAsync() as IEnumerable<TEntity>
                : await _context.Products.Include(P => P.Brand).Include(P => P.Type).AsNoTracking().ToListAsync() as IEnumerable<TEntity>;
            }

            return changeTracker ?
                 await _context.Set<TEntity>().ToListAsync()
                : await _context.Set<TEntity>().AsNoTracking().ToListAsync();
        }
        public async Task<TEntity?> GetAsync(TKey key)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                //return await _context.Products.Include(P => P.Brand).Include(P => P.Type).FirstOrDefaultAsync(P => P.Id == key as int?) as TEntity;
                return await _context.Products.Include(P => P.Brand).Include(P => P.Type).Where(P => P.Id == key as int?).FirstOrDefaultAsync() as TEntity;

            }
            return await _context.Set<TEntity>().FindAsync(key);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.AddAsync(entity);
        }
        public void Update(TEntity entity)
        {
            _context.Update(entity);
        }
        public void Delete(TEntity entity)
        {
            _context.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TKey, TEntity> spec, bool changeTracker)
        {
            return await ApplySpecifications(spec).ToListAsync();
        }

        public async Task<TEntity?> GetAsync(ISpecification<TKey, TEntity> spec)
        {
            return await ApplySpecifications(spec).FirstOrDefaultAsync();
        }

        public async Task<int> CountAsync(ISpecification<TKey, TEntity> spec)
        {
            return await ApplySpecifications(spec).CountAsync();
        }

        private IQueryable<TEntity> ApplySpecifications(ISpecification<TKey, TEntity> spec)
        {
            return SpecificationsEvaluator.GetQuery(_context.Set<TEntity>(), spec);
        }

    }
}

