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
                    Debug.WriteLine("Database connecton unsuccessful");
                }

                bool hasdata = await context_.Categories.AnyAsync();
                Debug.WriteLine(hasdata
                    ? "Database connection successful and data is accessible"
                    : "Database connection successful, but no accounts were found (database may be empty)");

                try
                {
                    var categories = await context_.Categories.ToListAsync();
                    Debug.WriteLine("Cateogires in the database:");
                    foreach (var cat in categories)
                    {
                        Debug.WriteLine($"- Id: {cat.Id}\n" +
                            $"- Name: {cat.Name}\n" +
                            $"- Type: {cat.Type} \n" +
                            $"- ParentCategoryId: {cat.ParentCategoryId} \n" +
                            $"- Created At: {cat.CreatedAt} \n");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Excepiton thrown while querying the database: " + ex.ToString());
                }

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
