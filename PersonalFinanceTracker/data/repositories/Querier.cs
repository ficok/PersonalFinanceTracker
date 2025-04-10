using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceTracker.Data.Context;
using PersonalFinanceTracker.Data.Specifications;

namespace PersonalFinanceTracker.Data.Repositories
{
    /**
     * This Querier is following the REPOSITORY pattern.
     * It's an intermediate between the Database context and the business logic.
     * All queries are executed through this interface.
     * This is a generic class: the generic type T denotes the model (table in the DB).
     * This way, there can be a Querier for each table that will return records from that table.
     * So if you need a list of transactions, you will use Querier<Transaction>.
     */
    public class Querier<T>: IQuerier<T> where T : class
    {
        // Database context
        protected readonly Database db_;
        public Querier(Database db)
        {
            db_ = db;
        }
        /** Return all records from the table with no conditions applied. */
        public IEnumerable<T> All()
        {
            return db_.Set<T>().ToList();
        }

        public async Task<IEnumerable<T>> AllAsync()
        {
            return await db_.Set<T>().ToListAsync();
        }

        /** Given the object that implements IQuery with type T (Transaction, Account...)
         *  apply the condition in that query object, apply the includes and execute the query
         *  via EF Core.
         *  It returns an object that implements IEnumerable, containing all records from the
         *  database that the query had found.
         */
        public IEnumerable<T> Query(IQuery<T> query)
        {
            IQueryable<T> table = db_.Set<T>();
            if (query.Condition != null)
            {
                table = table.Where(query.Condition);
            }

            foreach (var include in query.Includes)
            {
                table = table.Include(include);
            }

            return table.ToList();
        }

        public async Task<IEnumerable<T>> QueryAsync(IQuery<T> query)
        {
            IQueryable<T> table = db_.Set<T>();
            if (query.Condition != null)
            {
                table = table.Where(query.Condition);
            }

            foreach (var include in query.Includes)
            {
                table = table.Include(include);
            }

            return await table.ToListAsync();
        }
    }
}
