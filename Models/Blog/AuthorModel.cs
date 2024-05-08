using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webtintuc.Models
{
    public class AuthorModel
    {
        [Key]
        public int AuthorID { set; get; }

        [Required(ErrorMessage = "Không được bỏ trống {0}!")]
        [Display(Name = "Tên tác giả")]
        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        public string AuthorName { set; get; }

        [Display(Name = "Giới tính")]
        [Column(TypeName = "bit")]
        [Required(ErrorMessage = "Vui lòng chọn giới tính")]
        public bool Gender { set; get; }

        [Display(Name = "Địa chỉ email")]
        [Column(TypeName = "Char")]
        [StringLength(50)]
        [EmailAddress]
        [Required(ErrorMessage = "Vui lòng điền {0}!")]
        public string Email { get; set; }

        [Display(Name = "Số điện thoại")]
        [Column(TypeName = "Char")]
        [StringLength(13)]
        [Phone]
        [Required(ErrorMessage = "Vui lòng điền {0}!")]
        public string PhoneNumber { get; set; }

    }
}