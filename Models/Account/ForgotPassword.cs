using System.ComponentModel.DataAnnotations;

namespace webtintuc.Account.Models
{
    public class ForgotPasswordModel
    {
        [Display(Name = "Nhập Email bạn đã đăng ký với chúng tôi")]
        [EmailAddress]
        [Required(ErrorMessage = "Nhập địa chỉ Email đã đăng ký để thiết lập lại mật khẩu")]
        public string? Email { set; get; }
    }
}