using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceTracker.Models
{
    [Table("recurringtransactions")]
    public class RecurringTransaction
    {
        [Key] [Column("id")] public Guid Id { get; set; }
        [Column("account_id")] public Guid AccountId { get; set; }
        [Column("category_id")] public Guid? Categoryid { get; set; }
        [Column("transaction_id")] public Guid TransactionId { get; set; }
        [Column("amount")] public decimal Amount { get; set; }
        [Column("recurrence_pattern")] public string RecurrencePattern { get; set; }
        [Column("next_occurence")] public DateOnly NextOccurence { get; set; }
        [Column("description")] public string? Description { get; set; }
        [Column("create_at")] public DateTime CreatedAt { get; set; }
        [ForeignKey("AccountId")] public Account Account { get; set; }
        [ForeignKey("CategoryId")] public Category Category { get; set; }
        [ForeignKey("TransactionId")] public Transaction Transaction { get; set; }
    }
}
