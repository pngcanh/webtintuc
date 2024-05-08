using System.ComponentModel.DataAnnotations;

namespace webtintuc.Account.Models
{
    public class ResetPasswordModel
    {
        [Display(Name = "Nhập địa chỉ Email")]
        [Required(ErrorMessage = "Phải nhập {0}")]
        [EmailAddress(ErrorMessage = "Phải đúng định dạng email")]
        public string? Email { get; set; }

        [Display(Name = "Mật khẩu mới")]
        [Required(ErrorMessage = "Vui lòng nhập {0}")]
        [DataType(DataType.Password)]
        public string? Password { set; get; }

        [Display(Name = "Nhập lại mật khẩu")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Xác nhận mật khẩu mới chưa chính xác")]
        public string? ConfirmPassword { set; get; }

        public string? Code { get; set; }

    }
}