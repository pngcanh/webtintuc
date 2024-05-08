using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webtintuc.Models
{
    public class CommentModel
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Bình luận")]
        [Column(TypeName = "nvarchar")]
        [Required(ErrorMessage = "Vui lòng nhập nội dung {0}")]
        public string Content { get; set; }
        public AppUser appUser { get; set; }

    }
}