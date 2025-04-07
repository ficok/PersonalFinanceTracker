using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            try
            {
                bool connection = await context_.Database.CanConnectAsync();
                if (!connection)
                {
                    Console.WriteLine("Database connecton unsuccessful");
                }

                bool hasdata = await context_.Categories.AnyAsync();
                Console.WriteLine(hasdata
                    ? "Database connection successful and data is accessible"
                    : "Database connection successful, but no accounts were found (database may be empty)");

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception occurred while trying to connect to the database");
                Debug.WriteLine(ex.ToString());
                Debug.WriteLine("Stack Trace: " + ex.StackTrace);
            }
            return false;
        }
    }
}
