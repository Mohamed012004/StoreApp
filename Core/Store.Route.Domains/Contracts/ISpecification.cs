using Store.Route.Domains.Entities;
using System.Linq.Expressions;

namespace Store.Route.Domains.Contracts
{
    public interface ISpecification<Tkey, TEntitiy> where TEntitiy : BaseEntity<Tkey>
    {
        public List<Expression<Func<TEntitiy, object>>> Includes { get; set; }
        public Expression<Func<TEntitiy, bool>>? Criteria { get; set; }
        public Expression<Func<TEntitiy, object>>? OrderBy { get; set; }
        public Expression<Func<TEntitiy, object>>? OrderByDescending { get; set; }

        int Skip { get; set; }
        int Take { get; set; }
        bool IsPagination { get; set; }



    }
}
