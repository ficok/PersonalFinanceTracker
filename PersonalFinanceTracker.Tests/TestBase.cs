using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceTracker.Data.Context;

namespace PersonalFinanceTracker.Tests
{
    public abstract class TestBase
    {
        protected DbContextOptions<Database> GetInMemoryOptions(string dbName) =>
            new DbContextOptionsBuilder<Database>()
                .UseInMemoryDatabase(dbName)
                .Options;
        
        protected DbContextOptions<Database> GetNpgsqlOptions()
        {
            string connectionString = "Host=localhost;Port=5432;Database=PersonalFinanceDB;Username=postgres;Password=feartheoldblood";
            return new DbContextOptionsBuilder<Database>()
                .UseNpgsql(connectionString)
                .Options;
        }
    }
}
