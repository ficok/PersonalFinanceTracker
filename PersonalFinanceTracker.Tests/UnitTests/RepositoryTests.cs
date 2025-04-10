using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using PersonalFinanceTracker.Data.Context;
using PersonalFinanceTracker.Models;
using PersonalFinanceTracker.Data.Repositories;
using PersonalFinanceTracker.Data.Specifications;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;

namespace PersonalFinanceTracker.Tests.UnitTests
{
    public class RepositoryTests
    {
        // Helper method: create unique in-memory database operations per test
        private DbContextOptions<Database> GetInMemoryOptions(string dbName) => new DbContextOptionsBuilder<Database>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;
        [Fact]
        public void AllMethod_ReturnsAllRecords()
        {
            var options = GetInMemoryOptions(nameof(AllMethod_ReturnsAllRecords));
            using (var context = new Database(options))
            {
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
                context.SaveChanges();

                var querier = new Querier<Transaction>(context);
                var allTransactions = querier.All().ToList();

                Assert.Equal(2, allTransactions.Count);
            }
        }

        [Fact]
        public async Task AllSynchMethod_ReturnsAllRecords()
        {
            var options = GetInMemoryOptions(nameof(AllSynchMethod_ReturnsAllRecords));
            using (var context = new Database(options))
            {
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
                context.SaveChanges();

                var querier = new Querier<Transaction>(context);
                var allTransactions = (await querier.AllAsync()).ToList();

                Assert.Equal(2, allTransactions.Count);
            }
        }

        private class MinAmount: BaseQuery<Transaction>
        {
            private readonly decimal amount_;
            public MinAmount(decimal amount) { this.amount_ = amount; }
            public override Expression<Func<Transaction, bool>> Condition => t => t.Amount >= amount_;
        }

        [Fact]
        public void QueryMethod_FiltersRecords()
        {
            var options = GetInMemoryOptions(nameof(QueryMethod_FiltersRecords));
            using (var context = new Database(options))
            {
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

                context.SaveChanges();

                var querier = new Querier<Transaction>(context);
                var query = new MinAmount(20);

                var filteredTransactions = querier.Query(query).ToList();
                Assert.Single(filteredTransactions);
                Assert.True(filteredTransactions[0].Amount >= 20);
            }
        }

        [Fact]
        public async Task QueryAsyncMethod_FiltersRecords()
        {
            var options = GetInMemoryOptions(nameof(QueryAsyncMethod_FiltersRecords));
            using (var context = new Database(options))
            {
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

                context.SaveChanges();

                var querier = new Querier<Transaction>(context);
                var query = new MinAmount(20);

                var filteredTransactions = (await querier.QueryAsync(query)).ToList();

                Assert.Single(filteredTransactions);
                Assert.True(filteredTransactions[0].Amount >= 20);
            }
        }

        [Fact]
        public void QueryMethod_IncludesNavigationProperties()
        {
            var options = GetInMemoryOptions(nameof(QueryMethod_IncludesNavigationProperties));
            using (var context = new Database(options))
            {
                var accountId = Guid.NewGuid();
                var categoryId = Guid.NewGuid();

                var account = new Account
                {
                    Id = accountId,
                    Name = "Test Account",
                    Type = "Checking",
                    Description = "Test account description",
                    Currency = "USD",
                    CreatedAt = DateTime.Now
                };

                var category = new Category
                {
                    Id = categoryId,
                    Name = "Test Category",
                    Type = "Bla",
                    CreatedAt = DateTime.Now
                };

                context.Accounts.Add(account);
                context.Categories.Add(category);

                context.Transactions.Add(new Transaction
                {
                    Id = Guid.NewGuid(),
                    Amount = 123,
                    AccountId = accountId,
                    CategoryId = categoryId,
                    TransactionDate = DateTime.Now,
                    TransactionType = "Expense",
                    CreatedAt = DateTime.Now
                });

                context.SaveChanges();

                var querier = new Querier<Transaction>(context);
                var transactions = querier.Query(new AllTransactions()).ToList();

                Assert.Single(transactions);
                var result = transactions[0];
                Assert.NotNull(result.Account);
                Assert.NotNull(result.Category);
                Assert.Equal("Test Account", result.Account.Name);
                Assert.Equal("Test Category", result.Category.Name);
            }
        }

        [Fact]
        public void AllMethod_WithOrdering_ReturnsOrderedRecords()
        {
            var options = GetInMemoryOptions(nameof(AllMethod_WithOrdering_ReturnsOrderedRecords));
            using (var context = new Database(options))
            {
                context.Transactions.Add(new Transaction
                {
                    Id = Guid.NewGuid(),
                    Amount = 100,
                    AccountId = Guid.NewGuid(),
                    CategoryId = null,
                    TransactionDate = DateTime.Now,
                    TransactionType = "Expense",
                    CreatedAt = DateTime.Now
                });
                context.Transactions.Add(new Transaction
                {
                    Id = Guid.NewGuid(),
                    Amount = 50,
                    AccountId = Guid.NewGuid(),
                    CategoryId = null,
                    TransactionDate = DateTime.Now,
                    TransactionType = "Income",
                    CreatedAt = DateTime.Now
                });
                context.Transactions.Add(new Transaction
                {
                    Id = Guid.NewGuid(),
                    Amount = 150,
                    AccountId = Guid.NewGuid(),
                    CategoryId = null,
                    TransactionDate = DateTime.Now,
                    TransactionType = "Expense",
                    CreatedAt = DateTime.Now
                });
                context.SaveChanges();

                var querier = new Querier<Transaction>(context);
                var transactions = querier.All(q => q.OrderBy(t => t.Amount));
                Assert.Equal(50, transactions.First().Amount);
            }
        }
    }
}
