using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonalFinanceTracker.data.interfaces
{
    /** Following the REPOSITORY pattern
     *  This provides an interface that a Querier must provide.
     *  It is through this interface that the business logic communicates with the DB.
     *  This is the mediator between the logic and the Database context.
     */
    internal interface IQuerier<T> where T : class
    {
        IEnumerable<T> All();
        IEnumerable<T> Query(IQuery<T> query);

        /** Future: other methods
         *  Adding data
         *  Deleting data
         *  Et cetera */
    }
}
