using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Windows.Themes;
using PersonalFinanceTracker.Data.Repositories;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Data.UnitOfWork
    /** Interface for the Unit of work pattern. Enforces the UnitOfWork class to
     *  define the CommitAsync method and to hold the Queriers. */
{
    public interface IUnitOfWork: IDisposable
    {
        Task<int> CommitAsync();
        void Add<T>(T record) where T : class;
        Task AddAsync<T>(T record) where T : class;
        void Delete<T>(T record) where T : class;
    }
}
