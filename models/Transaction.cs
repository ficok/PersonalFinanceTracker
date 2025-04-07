using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceTracker.models
{
    [Table("transactions")]
    public class Transaction
    {
        [Key] public Guid TransactionId { get; set; }
        public Guid AccountId { get; set; }
        public Guid CategoryId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public Guid RelatedTransactionId { get; set; }
        public DateTime CreatedAt { get; set; }
        [ForeignKey("AccoundId")] public Account Account { get; set; }
        [ForeignKey("CategoryId")] public Category Category { get; set; }
        [ForeignKey("RelatedTransactionId")] public Transaction RelatedTransaction { get; set; }

    }
}
