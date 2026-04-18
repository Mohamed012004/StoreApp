using Microsoft.EntityFrameworkCore;
using Store.Route.Domains.Contracts;
using Store.Route.Domains.Entities;

namespace Store.Route.Persistence
{
    public static class SpecificationsEvaluator
    {
        // _context.Products.Include(P => P.Brand).Include(P => P.Type).Where(P => P.Id == key as int?).FirstOrDefaultAsync() as TEntity;

        // Generate Dynamic Query
        public static IQueryable<TEntity> GetQuery<Tkey, TEntity>(IQueryable<TEntity> inputQuery, ISpecification<Tkey, TEntity> spec) where TEntity : BaseEntity<Tkey>
        {
            var query = inputQuery; // _context.Products

            // Check criteria To Filteration
            if (spec.Criteria is not null)
            {
                query = query.Where(spec.Criteria); // _context.Products.Where(P => P.Id == 120);
            }

            // Check Expression To Order By with
            if (spec.OrderBy is not null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            else if (spec.OrderByDescending is not null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }

            // Create Pagination
            if (spec.IsPagination)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            // _context.Products.Where(P => P.Id == 120).Include(P => P.Brand)
            // _context.Products.Where(P => P.Id == 120).Include(P => P.Brand).Include(P => P.Type)...
            query = spec.Includes.Aggregate(query, (query, includeExpression) => query.Include(includeExpression)); // Aggregate Query: Make Iteration On List of Spec.Includes

            return query;
        }
    }
}
