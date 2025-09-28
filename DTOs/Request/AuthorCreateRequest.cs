using System.ComponentModel.DataAnnotations;

namespace BTL_QuanLiBanSach.DTOs.Request
{
    public class AuthorCreateRequest
    {

        [Required(ErrorMessage = "Tên tác giả là bắt buộc")]
        [StringLength(100, ErrorMessage = "Tên tác già không được vượt quá 100 ký tự")]
        public string Name { get; set; } = string.Empty;

    }
}
