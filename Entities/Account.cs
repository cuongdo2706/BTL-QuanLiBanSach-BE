using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTL_QuanLiBanSach.Entities;

[Table("tbl_account")]
public class Account
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public long Id { get; set; }

    [Column("password")] [Required] public string Password { get; set; }
    [Column("is_active")] public bool IsActive { get; set; }
    [Column("role")] public string Role { get; set; }
    [Column("username")] public string Username { get; set; }
    [Column("is_deleted")] public bool IsDeleted { get; set; }
    [Column("staff_id")] public long StaffId { get; set; }
    [ForeignKey(nameof(StaffId))] public User Staff { get; set; }
}