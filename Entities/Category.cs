using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTL_QuanLiBanSach.Entities;

[Table("tbl_category")]
public class Category
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public long Id { get; set; }

    [Column("name")] [Required] public string Name { get; set; }
    [Column("is_deleted")] public bool IsDeleted { get; set; }

    public List<Product> Products { get; set; } = new List<Product>();
}