using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTL_QuanLiBanSach.Entities;

[Table("tbl_order")]
public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public long Id { get; set; }

    [Column("code")] [Required] public string Code { get; set; }
    [Column("grand_total",TypeName = "decimal(19,2)")] public decimal? GrandTotal { get; set; }
    [Column("amount_paid",TypeName = "decimal(19,2)")] public decimal? AmountPaid { get; set; }
    [Column("change_amount",TypeName = "decimal(19,2)")] public decimal? ChangeAmount { get; set; }
    [Column("note")] public string? Note { get; set; }
    [Column("sub_total",TypeName = "decimal(19,2)")] public decimal? SubTotal { get; set; }
    [Column("payment_method")] [Required] public bool? PaymentMethod { get; set; }
    [Column("ordered_at")] public DateTime OrderedAt { get; set; }
    [Column("customer_id")] public long CustomerId { get; set; }
    [ForeignKey(nameof(CustomerId))] public virtual User User { get; set; }
    [Column("staff_id")] [Required] public long StaffId { get; set; }

    [ForeignKey(nameof(StaffId))]
    [Required]
    public virtual User Staff { get; set; }
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}