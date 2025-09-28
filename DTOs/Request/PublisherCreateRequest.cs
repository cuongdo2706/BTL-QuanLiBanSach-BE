using System.ComponentModel.DataAnnotations;

namespace BTL_QuanLiBanSach.DTOs.Request
{
    public class PublisherCreateRequest
    {

        [Required(ErrorMessage = "Tên nhà xuất bản là bắt buộc")]
        [StringLength(100, ErrorMessage = "Tên nhà xuất bản không được vượt quá 100 ký tự")]
        public string Name { get; set; } = string.Empty;

    }
}
