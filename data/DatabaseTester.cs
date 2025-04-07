using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PersonalFinanceTracker.data
{
    public class DatabaseTester
    {
        private readonly FinanceContext context_;

        public DatabaseTester(FinanceContext context)
        {
            context_ = context;
        }

        public async Task<bool> TestConnectionAsync()
        {
            bool connection = await context_.Database.CanConnectAsync();
            if (connection)
            {
                Console.WriteLine("Database connecton successful");
            }
            else
            {
                Console.WriteLine("Failed to connect to the database");
            }

            return connection;
        }
    }
}
