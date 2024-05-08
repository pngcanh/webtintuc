using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webtintuc.Models
{
    public class NewsModel
    {
        [Key]
        public int NewsID { set; get; }

        [Required(ErrorMessage = "Không được bỏ trống {0}!")]
        [Display(Name = "Tiêu đề")]
        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        public string Title { set; get; }

        [Display(Name = "Nội dung")]
        [Column(TypeName = "ntext")]
        [Required(ErrorMessage = "Không được bỏ trống {0}!")]
        public string Content { set; get; }

        [Display(Name = "URL")]
        [Column(TypeName = "varchar")]
        // [Required(ErrorMessage = "Không được bỏ trống {0}!")]
        public string Slug { set; get; }

        [Display(Name = "Ngày tạo")]
        [Column(TypeName = "DateTime")]
        // [Required(ErrorMessage = "Không được bỏ trống {0}!")]
        public DateTime Created { get; set; }

        // [Display(Name = "Thể loại")]
        public int? CategoryID { set; get; }
        [ForeignKey("CategoryID")]
        [Display(Name = "Thể loại")]
        public CategoryModel? CategoryModel { set; get; }

        // [Display(Name = "Tác giả")]
        public int? AuthorID { set; get; }
        [ForeignKey("AuthorID")]
        [Display(Name = "Tác giả")]
        public AuthorModel? AuthorModel { set; get; }
    }

}