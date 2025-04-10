using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Data.Context
{
    /** This is the database context - it maintains a connection to the real database.
     *  Each DbSet represents a table in the database.
     *  Queries that are sent through the IQuerier implementing object retrieve data
     *  through this context.
     */
    public class Database: DbContext
    {
        public Database(DbContextOptions<Database> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<RecurringTransaction> RecurringTransactions { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
