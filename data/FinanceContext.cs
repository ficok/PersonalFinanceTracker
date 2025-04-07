using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceTracker.models;

namespace PersonalFinanceTracker.data
{
    public class FinanceContext: DbContext
    {
        public FinanceContext(DbContextOptions<FinanceContext> options) : base(options)
        {
        }

        //public DbSet<Account> Accounts { get; set; }
        //public DbSet<Transaction> Transactions { get; set; }
        //public DbSet<RecurringTransaction> RecurringTransactions { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
