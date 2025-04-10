using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceTracker.Models
{
    [Table("categories")]
    public class Category
    {
        [Key] [Column("id")] public Guid Id { get; set; }
        [Column("name")] public string Name { get; set; }
        [Column("type")] public string Type { get; set; }
        [Column("parent_category_id")] public Guid? ParentCategoryId { get; set; }
        [Column("created_at")] public DateTime CreatedAt { get; set; }
        [ForeignKey("ParentCategoryId")] public Category ParentCategory { get; set; }
    }
}
