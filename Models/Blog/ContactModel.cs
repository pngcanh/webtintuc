using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webtintuc.Models
{
    public class ContactModel
    {
        [Key]
        public int ID { get; set; }


        [Display(Name = "Họ và tên")]
        [Required(ErrorMessage = "Vui lòng điền {0}")]
        [StringLength(30, MinimumLength = 2)]
        [Column(TypeName = "nvarchar")]

        public string Name { get; set; }

        [Display(Name = "Địa chỉ Email")]
        [Required(ErrorMessage = "Vui lòng điền {0}")]
        [EmailAddress]
        [Column(TypeName = "varchar")]

        public string Email { get; set; }

        [Display(Name = "Tiêu đề")]
        [StringLength(60)]
        [Column(TypeName = "nvarchar")]

        public string Title { get; set; }

        [Display(Name = "Nội dung")]
        [Required(ErrorMessage = "Vui lòng điền {0}")]
        [StringLength(300)]
        [Column(TypeName = "nvarchar")]
        public string Content { get; set; }
    }
}