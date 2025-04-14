using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceTracker.Data.Context;
using PersonalFinanceTracker.Data.Repositories;
using PersonalFinanceTracker.Data.Specifications;
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

        [Fact]
        public async Task UnitOfWork_AddMethod_AddsRecords()
        {
            var options = GetInMemoryOptions(nameof(UnitOfWork_AddMethod_AddsRecords));
            using (var context = new Database(options))
            {
                var uow = new UnitOfWork(context);
                uow.Add(new Transaction
                {
                    Id = Guid.NewGuid(),
                    Amount = 20,
                    CategoryId = null,
                    TransactionDate = DateTime.Now,
                    TransactionType = "Expense",
                    CreatedAt = DateTime.Now,
                });

                int result = await uow.CommitAsync();
                Assert.True(result > 0);

                var transactionCount = context.Transactions.Count();
                Assert.Equal(1, transactionCount);
            }
        }

        [Fact]
        public async Task UnitOfWork_AddAsyncMethod_AddsRecords()
        {
            var options = GetInMemoryOptions(nameof(UnitOfWork_AddAsyncMethod_AddsRecords));
            using (var context = new Database(options))
            {
                var uow = new UnitOfWork(context);
                await uow.AddAsync(new Transaction
                {
                    Id = Guid.NewGuid(),
                    Amount = 20,
                    CategoryId = null,
                    TransactionDate = DateTime.Now,
                    TransactionType = "Income",
                    CreatedAt = DateTime.Now,
                });

                int result = await uow.CommitAsync();
                Assert.True(result > 0);

                var transactionCount = context.Transactions.Count();
                Assert.Equal(1, transactionCount);
            }
        }
        private class ExactAmount : Query<Transaction>
        {
            private readonly decimal amount_;
            public ExactAmount(decimal amount)
            {
                amount_ = amount;
                AddCondition(t => t.Amount == amount_);
            }
        }

        [Fact]
        public async Task UnitOfWork_DeleteMethod_DeletesRecords()
        {
            var options = GetInMemoryOptions(nameof(UnitOfWork_DeleteMethod_DeletesRecords));
            using (var context = new Database(options))
            {
                context.Transactions.Add(new Transaction
                {
                    Id = Guid.NewGuid(),
                    Amount = 20,
                    CategoryId = null,
                    TransactionDate = DateTime.Now,
                    TransactionType = "Income",
                    CreatedAt = DateTime.Now
                });

                context.Transactions.Add(new Transaction
                {
                    Id = Guid.NewGuid(),
                    Amount = 10,
                    CategoryId = null,
                    TransactionDate = DateTime.Now,
                    TransactionType = "Expense",
                    CreatedAt = DateTime.Now
                });

                context.SaveChanges();
            }

            using (var context = new Database(options))
            {
                var uow = new UnitOfWork(context);
                var querier = uow.GetQuerier<Transaction>();

                var t1 = querier.Query(new ExactAmount(20)).ToList().First();
                Assert.Equal(20, t1.Amount);
                uow.Delete(t1);
                await uow.CommitAsync();
            }

            using (var context = new Database(options))
            {
                var uow = new UnitOfWork(context);
                var querier = uow.GetQuerier<Transaction>();

                var transactions = querier.All().ToList();
                Assert.Single(transactions);
                Assert.Equal(10, transactions.First().Amount);
            }
        }
    }
}
