using Store.Route.Domains.Contracts;
using Store.Route.Domains.Entities;
using System.Linq.Expressions;

namespace Store.Route.Services.Specifications
{
    public class BaseSpecifications<TKey, TEntity> : ISpecification<TKey, TEntity> where TEntity : BaseEntity<TKey>
    {
        public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new List<Expression<Func<TEntity, object>>>();
        public Expression<Func<TEntity, bool>>? Criteria { get; set; }
        public Expression<Func<TEntity, object>>? OrderBy { get; set; }
        public Expression<Func<TEntity, object>>? OrderByDescending { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPagination { get; set; }

        public BaseSpecifications(Expression<Func<TEntity, bool>>? expression)
        {
            Criteria = expression;
        }

        public void AddOrderBy(Expression<Func<TEntity, object>>? expression)
        {
            OrderBy = expression;
        }

        public void AddOrderByDescending(Expression<Func<TEntity, object>>? expression)
        {
            OrderByDescending = expression;
        }

        public void ApplyPagination(int PageIndex, int PageSize)
        {
            IsPagination = true;
            Skip = (PageIndex - 1) * PageSize;
            Take = PageSize;
        }

    }
}
