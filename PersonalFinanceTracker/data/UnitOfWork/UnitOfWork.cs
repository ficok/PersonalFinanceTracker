using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceTracker.Data.Context;
using PersonalFinanceTracker.Data.Repositories;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Data.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly Database db_;

        private readonly Dictionary<Type, object> queriers_ = new();

        public UnitOfWork(Database db)
        {
            db_ = db;
        }

        public IQuerier<T> GetQuerier<T>() where T: class
        {
            var type = typeof(T);
            if (!queriers_.ContainsKey(type))
            {
                queriers_[type] = new Querier<T>(db_);
            }

            return (IQuerier<T>)queriers_[type];
        }

        public void Add<T>(T record) where T: class
        {
            GetQuerier<T>().Add(record);
        }

        public async Task AddAsync<T>(T record) where T: class
        {
            await GetQuerier<T>().AddAsync(record);
        }

        public void Delete<T>(T record) where T: class
        {
            GetQuerier<T>().Delete(record);
        }


        public async Task<int> CommitAsync()
        {
            return await db_.SaveChangesAsync();
        }

        public void Dispose()
        {
            db_.Dispose();
        }


    }
}
