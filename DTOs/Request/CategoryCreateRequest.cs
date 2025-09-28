using System.ComponentModel.DataAnnotations;

namespace BTL_QuanLiBanSach.DTOs.Request
{
    public class CategoryCreateRequest
    {
        [Required(ErrorMessage = "Tên loại sách là bắt buộc")]
        [StringLength(100, ErrorMessage = "Tên loại sách không được vượt quá 100 ký tự")]
        public string Name { get; set; } = string.Empty;
    }
}
