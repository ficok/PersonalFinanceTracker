using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceTracker.Data.Specifications
{
    /** This is a base Query class - an abstract class. It is a template for the 
     *  actual queries that shows how they should implement the IQuery interface.
     */
    public abstract class BaseQuery<T>: IQuery<T>
    {
        public abstract Expression<Func<T, bool>> Condition { get; }
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
    }
}
