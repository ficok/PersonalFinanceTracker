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
        [Key] public Guid AccountId { get; set; }
        public string Name { get; set; }
        public string Type {  get; set; }
        public string Description { get; set; }
        public string Currency {  get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
