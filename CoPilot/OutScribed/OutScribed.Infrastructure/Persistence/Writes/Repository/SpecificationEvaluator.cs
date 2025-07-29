using OutScribed.SharedKernel.Abstract.OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.Abstract;
using Microsoft.EntityFrameworkCore;
using OutScribed.SharedKernel.Interfaces;

namespace OutScribed.Infrastructure.Persistence.Writes.Repository
{
    internal static class SpecificationEvaluator<T> where T : AggregateRoot
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
        {
            var query = inputQuery;

            // Apply the criteria (WHERE clause)
            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            // Apply includes (eager loading)
            query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

            // Apply sorting
            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            else if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            // Apply paging
            if (specification.IsPagingEnabled)
            {
                query = query.Skip(specification.Skip).Take(specification.Take);
            }

            return query;
        }
    }
}
