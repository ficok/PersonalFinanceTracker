using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using PersonalFinanceTracker.Data.Context;
using PersonalFinanceTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace PersonalFinanceTracker.Tests.IntegrationTests
{
    public class NavigationPropertyTests: TestBase
    {
        [Fact]
        public async Task NavigationProperties_AreProperlyLoaded()
        {
            var options = GetNpgsqlOptions();
            using (var context = new Database(options))
            {
                await context.Database.OpenConnectionAsync();
                await context.Database.EnsureCreatedAsync();

                var account = new Account
                {
                    Id = Guid.NewGuid(),
                    Name = "Test Account",
                    Type = "Cash",
                    Description = "This is a test account",
                    Currency = "USD",
                    CreatedAt = DateTime.UtcNow
                };

                var category = new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Test Category",
                    Type = "Expense",
                    CreatedAt = DateTime.UtcNow
                };

                context.Accounts.Add(account);
                context.Categories.Add(category);

                await context.SaveChangesAsync();

                var transaction = new Transaction
                {
                    Id = Guid.NewGuid(),
                    Amount = 100,
                    AccountId = account.Id,
                    CategoryId = category.Id,
                    TransactionDate = DateTime.UtcNow,
                    TransactionType = "Expense",
                    CreatedAt = DateTime.UtcNow
                };

                context.Transactions.Add(transaction);
                await context.SaveChangesAsync();
            }

            /* Use a new context to ensure a fresh query session */
            using (var context = new Database(options))
            {
                await context.Database.OpenConnectionAsync();
                await context.Database.EnsureCreatedAsync();

                var transaction = await context.Transactions
                    .Include(t => t.Account)
                    .Include(t => t.Category)
                    .FirstOrDefaultAsync();

                Assert.NotNull(transaction);
                Assert.NotNull(transaction.Account);
                Assert.Equal("Test Account", transaction.Account.Name);
                Assert.NotNull(transaction.Category);
                Assert.Equal("Test Category", transaction.Category.Name);
            }
        }
    }
}
