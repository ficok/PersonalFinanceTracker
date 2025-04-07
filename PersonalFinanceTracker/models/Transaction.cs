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
        [Key] [Column("id")] public Guid Id { get; set; }
        [Column("account_id")] public Guid AccountId { get; set; }
        [Column("category_id")] public Guid? CategoryId { get; set; }
        [Column("amount")] public decimal Amount { get; set; }
        [Column("description")] public string? Description { get; set; }
        [Column("transaction_date")] public DateTime TransactionDate { get; set; }
        [Column("transacrion_type")] public string TransactionType { get; set; }
        [Column("related_transaction_id")] public Guid? RelatedTransactionId { get; set; }
        [Column("created_at")] public DateTime CreatedAt { get; set; }
        [ForeignKey("AccoundId")] public Account Account { get; set; }
        [ForeignKey("CategoryId")] public Category Category { get; set; }
        [ForeignKey("RelatedTransactionId")] public Transaction RelatedTransaction { get; set; }

    }
}
