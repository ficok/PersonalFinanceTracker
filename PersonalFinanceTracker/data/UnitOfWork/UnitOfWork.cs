using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceTracker.Data.Context;
using PersonalFinanceTracker.Data.Repositories;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Data.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly Database db_;

        public IQuerier<Account> Accounts { get; }
        public IQuerier<Transaction> Transactions { get; }
        public IQuerier<RecurringTransaction> RecurringTransactions { get; }
        public IQuerier<Category> Categories { get; }

        public UnitOfWork(Database db)
        {
            db_ = db;

            Accounts = new Querier<Account>(db_);
            Transactions = new Querier<Transaction>(db_);
            RecurringTransactions = new Querier<RecurringTransaction>(db_);
            Categories = new Querier<Category>(db_);
        }

        public async Task<int> CommitAsync()
        {
            return await db_.SaveChangesAsync();
        }

        public void Dispose()
        {
            db_.Dispose();
        }


    }
}
