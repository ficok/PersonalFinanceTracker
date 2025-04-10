using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PersonalFinanceTracker.Data.Specifications;

namespace PersonalFinanceTracker.Data.Repositories
{
    /** Following the REPOSITORY pattern
     *  This provides an interface that a Querier must provide.
     *  It is through this interface that the business logic communicates with the DB.
     *  This is the mediator between the logic and the Database context.
     */
    public interface IQuerier<T> where T: class
    {
        IEnumerable<T> All(Func<IQueryable<T>, IOrderedQueryable<T>>? orderby = null);
        IEnumerable<T> Query(IQuery<T> query, Func<IQueryable<T>, IOrderedQueryable<T>>? orderby = null);
        Task<IEnumerable<T>> AllAsync(Func<IQueryable<T>, IOrderedQueryable<T>>? orderby = null);
        Task<IEnumerable<T>> QueryAsync(IQuery<T> query, Func<IQueryable<T>, IOrderedQueryable<T>>? orderby = null);

        /** Future: other CRUD methods, both sync and async
         *  Adding data
         *  Deleting data
         *  Updating data */
    }
}
