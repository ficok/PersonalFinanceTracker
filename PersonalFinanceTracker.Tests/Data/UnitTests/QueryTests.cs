using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using PersonalFinanceTracker.Models;
using PersonalFinanceTracker.Data.Specifications;

namespace PersonalFinanceTracker.Tests.Data.UnitTests
{
    public class QueryTests: TestBase
    {
        [Fact]
        public void AllTransactions_Query_HasNonNullCondition()
        {
            // Create a new instance of the AllTransactions query:
            var query = new AllTransactions();
            Assert.NotNull(query.Conditions.First());
        }

        [Fact]
        public void AllTransactions_Query_ConditionReturnsTrueForAnyTransaction()
        {
            var query = new AllTransactions();
            var testTransaction = new Transaction();
            var conditionDelegate = query.Conditions.First().Compile();
            bool result = conditionDelegate(testTransaction);
            Assert.True(result);
        }

        [Fact]
        public void AllTransactions_Query_Includes_CountIsTwo()
        {
            var query = new AllTransactions();
            int includesCount = query.Includes.Count;
            Assert.Equal(2, includesCount);
        }

        [Fact]
        public void AllTransactions_Query_Includes_ContainCategoryAndAccount()
        {
            var query = new AllTransactions();
            var includesDescriptions = query.Includes.Select(expr => expr.ToString());

            bool containsCategory = includesDescriptions.Any(desc => desc.Contains("Category"));
            bool containsAccount = includesDescriptions.Any(desc => desc.Contains("Account"));
            Assert.True(containsCategory, "Include should contain a reference to Category.");
            Assert.True(containsAccount, "Include should contain a reference to Account.");
        }
    }
}
