using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace webtintuc.Account.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Vui lòng điền {0}")]
        [Display(Name = "Tên đăng nhập")]

        public string? UserName { set; get; }
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [DataType(DataType.Password)]

        public string? Password { set; get; }
        [Display(Name = "Lưu thông tin đăng nhập")]
        public bool RememberMe { set; get; }
    }
}