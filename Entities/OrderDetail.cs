using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTL_QuanLiBanSach.Entities;
[Table("tbl_order_detail")]
public class OrderDetail
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public long Id { get; set; }
    [Column("price",TypeName = "decimal(19,2)")]
    public decimal Price { get; set; }
    [Column("product_code")]
    public string ProductCode { get; set; }
    [Column("product_name")]
    public string ProductName { get; set; }
    [Column("quantity")]
    public int Quantity { get; set; }
    [Column("total_price",TypeName = "decimal(19,2)")]
    public decimal TotalPrice { get; set; }
    [Column("order_id",TypeName = "decimal(19,2)")]
    [Required]
    public long OrderId { get; set; }
    [ForeignKey(nameof(OrderId))]
    [Required]
    public Order Order { get; set; }
    [Column("product_id")]
    public long ProductId { get; set; }
    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; }
}