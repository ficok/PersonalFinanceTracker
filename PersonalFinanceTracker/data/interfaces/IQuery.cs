using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceTracker.data.interfaces
{
    /** Following the SPECIFICATION pattern.
     *  This provides the interface that each query object must satisfy.
     *  Objects implementing this interface represent the queries the
     *  business logic can execute on the database.
     *  This interface provides a way to set the conditions (similar to WHERE clause)
     *  and includes to populate navigation properties with associated data
     *  (similar to JOIN clause).
     */
    public interface IQuery<T>
    {
        // The contitions that the entity must satisfy
        Expression<Func<T, bool>> Condition { get; }
        // Collection of eagerly-loaded navigation properties
        List<Expression<Func<T, object>>> Includes { get; }
    }
}
