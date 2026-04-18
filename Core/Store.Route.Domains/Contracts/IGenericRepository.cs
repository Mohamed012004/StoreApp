using Store.Route.Domains.Entities;

namespace Store.Route.Domains.Contracts
{
    public interface IGenericRepository<TKey, TEntity> where TEntity : BaseEntity<TKey>
    {
        public Task<IEnumerable<TEntity>> GetAllAsync(bool changeTracker);
        public Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TKey, TEntity> spec, bool changeTracker);

        public Task<TEntity?> GetAsync(TKey Id);
        public Task<TEntity?> GetAsync(ISpecification<TKey, TEntity> spec);
        public Task<int> CountAsync(ISpecification<TKey, TEntity> spec);

        public Task AddAsync(TEntity entity);
        public void Update(TEntity entity);
        public void Delete(TEntity entity);
    }
}
