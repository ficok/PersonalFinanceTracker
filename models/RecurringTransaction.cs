using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceTracker.models
{
    [Table("recurringtransactions")]
    public class RecurringTransaction
    {
        [Key] public Guid RecurringTransactionId { get; set; }
        public Guid AccountId { get; set; }
        public Guid Categoryid { get; set; }
        public Guid TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string RecurrencePattern { get; set; }
        public DateOnly NextOccurence { get; set; }
        public string Description { get; set; }
        public DateTime CreateAt { get; set; }
        [ForeignKey("AccountId")] public Account Account { get; set; }
        [ForeignKey("CategoryId")] public Category Category { get; set; }
        [ForeignKey("TransactionId")] public Transaction Transaction { get; set; }
    }
}
