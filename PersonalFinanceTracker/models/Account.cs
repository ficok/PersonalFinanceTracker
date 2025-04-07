using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceTracker.models
{
    [Table("accounts")]
    public class Account
    {
        [Key] [Column("id")] public Guid Id { get; set; }
        [Column("name")] public string Name { get; set; }
        [Column("type")] public string Type {  get; set; }
        [Column("description")] public string Description { get; set; }
        [Column("currency")] public string Currency {  get; set; }
        [Column("created_at")] public DateTime CreatedAt { get; set; }
    }
}
