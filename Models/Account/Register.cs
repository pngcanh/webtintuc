using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace webtintuc.Account.Models
{
    public class RegisterModel
    {
        [Display(Name = "Tên đăng nhập", Prompt = "Tên đăng nhập")]
        [Required(ErrorMessage = "Vui lòng nhập {0}")]
        public string? Name { set; get; }

        [Display(Name = "Địa chỉ Email", Prompt = "Địa chỉ Email")]
        [Required(ErrorMessage = "Vui lòng nhập {0}")]
        [DataType(DataType.EmailAddress)]
        public string? Email { set; get; }

        [Display(Name = "Mật khẩu", Prompt = "Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập {0}")]
        [DataType(DataType.Password)]
        public string? Password { set; get; }

        [Display(Name = "Nhập lại mật khẩu", Prompt = "Nhập lại mật khẩu")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Xác nhận mật khẩu mới chưa chính xác")]
        public string? ConfirmPassword { set; get; }

    }
}