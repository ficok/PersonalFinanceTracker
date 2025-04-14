using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PersonalFinanceTracker;
using PersonalFinanceTracker.Data.Context;
using PersonalFinanceTracker.Data.UnitOfWork;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Services.CoreServices
{
    /** Let's make this a generic, where type T can be Transaction, Account, Category, RecurrentTransac... */
    public class Editor: BaseCoreService
    {
        public Editor(): base()
        {
        }

        public void Add<T>(T record) where T: class
        {
            uow_.Add(record);
            uow_.CommitAsync().Wait();
        }

        public void Delete<T>(T record) where T : class
        {
            uow_.Delete(record);
            uow_.CommitAsync().Wait();
        }
    }
}
