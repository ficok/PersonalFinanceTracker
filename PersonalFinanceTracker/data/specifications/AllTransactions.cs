using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Data.Specifications
{
    /** This class is an implementation of the IQuery interface.
     *  It represents one query that the business logic can execute on the database.
     *  In case of needing a new kind of query, a new Query class will be implemented,
     *  specifying the conditions for the query to filer the records and the includes
     *  for populating the navigational properties with associated data.
     */
    public class AllTransactions: Query<Transaction>
    {
        public AllTransactions()
        {
            // No conditions - get all transactions
            AddCondition(t => true);
            // Get data about each record's associated category and account data
            AddInclude(t => t.Category);
            AddInclude(t => t.Account);
        }
    }
}
