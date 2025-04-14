using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PersonalFinanceTracker.Data.UnitOfWork;
using PersonalFinanceTracker.Data.Context;

namespace PersonalFinanceTracker.Services.CoreServices
{
    public abstract class BaseCoreService: IDisposable
    {
        protected IServiceScope scope_;
        protected UnitOfWork uow_;

        public BaseCoreService()
        {
            scope_ = App.AppHost.Services.CreateScope();
            var db = scope_.ServiceProvider.GetService<Database>();
            uow_ = new UnitOfWork(db);
        }
        public void Dispose()
        {
            scope_.Dispose();
            uow_.Dispose();
        }
    }
}
