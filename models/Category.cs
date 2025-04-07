using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceTracker.models
{
    [Table("categories")]
    public class Category
    {
        [Key] public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public DateTime CreatedAt { get; set; }
        [ForeignKey("ParentCategoryId")] public Category ParentCategory { get; set; }
    }
}
