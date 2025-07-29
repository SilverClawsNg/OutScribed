using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;
using OutScribed.SharedKernel.Abstract;

namespace OutScribed.SharedKernel.Interfaces
{
    public interface ISpecification<T> where T : AggregateRoot
    {
        // Criteria for filtering entities (e.g., x => x.EmailAddress == email)
        Expression<Func<T, bool>> Criteria { get; }

        // List of expressions for eager loading related entities (e.g., x => x.RelatedEntities)
        List<Expression<Func<T, object>>> Includes { get; }

        // Adds conditions for sorting (optional)
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }

        // Adds conditions for paging (optional)
        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }
    }
}
