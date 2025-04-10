using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalFinanceTracker.Data.Repositories;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Data.UnitOfWork
    /** Interface for the Unit of work pattern. Enforces the UnitOfWork class to
     *  define the CommitAsync method and to hold the Queriers. */
{
    public interface IUnitOfWork: IDisposable
    {
        IQuerier<Account> Accounts { get; }
        IQuerier<Transaction> Transactions { get; }
        IQuerier<RecurringTransaction> RecurringTransactions { get; }
        IQuerier<Category> Categories { get; }

        Task<int> CommitAsync();
    }
}
