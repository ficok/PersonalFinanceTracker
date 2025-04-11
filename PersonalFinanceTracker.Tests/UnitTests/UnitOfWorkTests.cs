using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceTracker.Data.Context;
using PersonalFinanceTracker.Data.UnitOfWork;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Tests.UnitTests
{
    public class UnitOfWorkTests: TestBase
    {
        [Fact]
        public async Task CommitAsyncMethod_SavesAddedTransactions()
        {
            var options = GetInMemoryOptions(nameof(CommitAsyncMethod_SavesAddedTransactions));
            using (var context = new Database(options))
            {
                var uow = new UnitOfWork(context);
                context.Transactions.Add(new Transaction
                {
                    Id = Guid.NewGuid(),
                    Amount = 10,
                    AccountId = Guid.NewGuid(),
                    CategoryId = null,
                    TransactionDate = DateTime.Now,
                    TransactionType = "Expense",
                    CreatedAt = DateTime.Now
                });

                context.Transactions.Add(new Transaction
                {
                    Id = Guid.NewGuid(),
                    Amount = 20,
                    AccountId = Guid.NewGuid(),
                    CategoryId = null,
                    TransactionDate = DateTime.Now,
                    TransactionType = "Income",
                    CreatedAt = DateTime.Now
                });

                /* CommitAsync poziva SaveChangesAsync nad DB, sto vraca broj uspesnih promena, pa i ovo
                 * vraca broj uspesnih promena */
                int result = await uow.CommitAsync();

                Assert.True(result > 0);

                var transactionCount = context.Transactions.Count();
                Assert.Equal(2, transactionCount);
            }
        }

        [Fact]
        public void DisposeMethod_ClosesContext()
        {
            var options = GetInMemoryOptions(nameof(DisposeMethod_ClosesContext));
            var context = new Database(options);
            var uow = new UnitOfWork(context);

            uow.Dispose();

            Assert.Throws<ObjectDisposedException>(() =>
            {
                var count = context.Transactions.Count();
            });
        }
    }
}
