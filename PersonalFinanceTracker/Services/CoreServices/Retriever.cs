using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalFinanceTracker.Models;
using PersonalFinanceTracker.Data.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using System.ServiceProcess;
using PersonalFinanceTracker;
using PersonalFinanceTracker.Data.Context;
using PersonalFinanceTracker.Data.Specifications;

namespace PersonalFinanceTracker.Services.CoreServices
{
    public class Retriever: BaseCoreService
    {
        public decimal Sum { get; set; }
        public Retriever(): base()
        {
            Sum = 0;
        }

        public IEnumerable<Transaction> All(Func<IQueryable<Transaction>, IOrderedQueryable<Transaction>> orderby = null)
        {
            var querier = uow_.GetQuerier<Transaction>();
            var result = querier.All(orderby);
            return result.ToList();
        }

        public IEnumerable<Transaction> Query(
            DateTime? startDate = null,
            DateTime? endDate = null,
            Guid? accountId = null,
            Guid? categoryId = null,
            Func<IQueryable<Transaction>, IOrderedQueryable<Transaction>> orderby = null)
        {
            var querier = uow_.GetQuerier<Transaction>();
            var query = new Query<Transaction>();

            if (startDate.HasValue)
            {
                query.AddCondition(t => t.TransactionDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query.AddCondition(t => t.TransactionDate <= endDate.Value);
            }

            if (accountId.HasValue)
            {
                query.AddCondition(t => t.AccountId == accountId.Value);
            }

            if (categoryId.HasValue)
            {
                query.AddCondition(t => t.CategoryId == categoryId.Value);
            }

            query.AddInclude(t => t.Category);
            query.AddInclude(t => t.Account);

            var transactions = querier.Query(query).ToList();

            foreach (var transaction in transactions)
            {
                Sum += transaction.Amount;
            }

            return transactions;
        }
    }
}
