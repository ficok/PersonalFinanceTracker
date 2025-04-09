using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceTracker.data.context;

namespace PersonalFinanceTracker.data
{
    /** This class is used to test the connection to the database.
     *  So far, it is used in startup logic in the beginning of the development
     *  cycle to ensure everything is properly set up.
     */
    public class DatabaseTester
    {
        private readonly Database db_;

        public DatabaseTester(Database db)
        {
            db_ = db;
        }

        public async Task<bool> TestConnectionAsync()
        {
            try
            {
                bool connection = await db_.Database.CanConnectAsync();
                if (!connection)
                {
                    Debug.WriteLine("Database connecton unsuccessful");
                }

                bool hasdata = await db_.Categories.AnyAsync();
                Debug.WriteLine(hasdata
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
