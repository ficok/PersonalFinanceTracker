using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceTracker.Data.Context;

namespace PersonalFinanceTracker.Tests.Data.IntegrationTests
{
    public class DatabaseConnectionTests: TestBase
    {
        [Fact]
        public async Task CanConnectToPostgreSQLDatabase()
        {
            var options = GetNpgsqlOptions();
            using var context = new Database(options);

            await context.Database.OpenConnectionAsync();
            await context.Database.EnsureCreatedAsync();

            bool canConnect = await context.Database.CanConnectAsync();

            Assert.True(canConnect, "Should be able to connect to the PostgreSQL DB.");
        }
    }
}
