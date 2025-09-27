using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTL_QuanLiBanSach.Entities;

[Table("tbl_product")]
public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public long Id { get; set; }

    [Column("code")] public string Code { get; set; }
    [Column("description")] public string? Description { get; set; }
    [Column("img_url")] public string? ImgUrl { get; set; }
    [Column("is_deleted")] public bool IsDeleted { get; set; }
    [Column("is_active")] public bool IsActive { get; set; }
    [Column("name")] [Required] public string Name { get; set; }
    [Column("num_of_pages")] public int? NumOfPages { get; set; }
    [Column("price",TypeName = "decimal(19,2)")] [Required] public decimal Price { get; set; }
    [Column("public_id")] public string? PublicId { get; set; }
    [Column("published_year")] public int? PublishedYear { get; set; }
    [Column("quantity")] [Required] public int Quantity { get; set; }
    [Column("translator")] public string? Translator { get; set; }
    [Column("publisher_id")] public long PublisherId { get; set; }

    [ForeignKey(nameof(PublisherId))]
    [Required]
    public virtual Publisher? Publisher { get; set; }

    public List<Category> Categories { get; set; } = new List<Category>();
    public List<Author> Authors { get; set; } = new List<Author>();
}