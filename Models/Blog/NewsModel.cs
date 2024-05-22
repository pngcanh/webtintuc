using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace webtintuc.Models
{
    public class NewsModel
    {
        [Key]
        public int NewsID { set; get; }

        [Required(ErrorMessage = "Không được bỏ trống {0}!")]
        [Display(Name = "Tiêu đề")]
        [Column(TypeName = "ntext")]
        public string Title { set; get; }

        [Display(Name = "Ảnh đại diện cho bài viết")]
        [StringLength(200)]
        public string Photo { set; get; }

        [Display(Name = "Nội dung")]
        [Column(TypeName = "ntext")]
        [Required(ErrorMessage = "Không được bỏ trống {0}!")]
        public string Content { set; get; }

        [Display(Name = "URL", Prompt = "Có thể nhập hoặc tự phát sinh theo tiêu đề")]
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string? Slug { set; get; }


        [Display(Name = "Ngày tạo")]
        [Column(TypeName = "DateTime")]
        public DateTime Created { get; set; }


        [Column(TypeName = "int")]
        [Display(Name = "Lượt xem")]
        private int num = 0;
        public int ViewNumber { set { num = value; } get { return num; } }

        [Display(Name = "Thể loại")]
        public int? CategoryID { set; get; }
        [ForeignKey("CategoryID")]
        public CategoryModel? CategoryModel { set; get; }

        [Display(Name = "Tác giả")]
        public int? AuthorID { set; get; }
        [ForeignKey("AuthorID")]
        public AuthorModel? AuthorModel { set; get; }

    }

}