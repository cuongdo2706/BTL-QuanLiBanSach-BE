using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTL_QuanLiBanSach.Entities;

[Table("tbl_user")]
public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public long Id { get; set; }

    [Column("address")] public string Address { get; set; }
    [Column("code")] [Required] public string Code { get; set; }
    [Column("dob")] public DateOnly? Dob { get; set; }
    [Column("email")] public string? Email { get; set; }
    [Column("gender")] public bool? Gender { get; set; }
    [Column("phone_num")] public string? PhoneNum { get; set; }
    [Column("img_url")] public string ImgUrl { get; set; }
    [Column("public_id")] public string PublicId { get; set; }
    [Column("name")] [Required] public string Name { get; set; }
    [Column("is_active")] public bool IsActive { get; set; }
    [Column("is_deleted")] public bool IsDeleted { get; set; }
    [Column("user_type")] [Required] public bool UserType { get; set; }
    
}