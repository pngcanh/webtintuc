using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webtintuc.Models
{
    public class CategoryModel
    {
        [Key]
        public int CategoryID { set; get; }

        [Required(ErrorMessage = "Không được bỏ trống {0}!")]
        [Display(Name = "Thể loại")]
        [Column(TypeName = "nvarchar")]
        [StringLength(30)]
        public string CategoryName { set; get; }

        [Display(Name = "URL", Prompt = "Nhập hoặc bỏ trống sẽ tự động phát sinh")]
        [Column(TypeName = "varchar")]
        [StringLength(30)]
        public string? Slug { get; set; }

        [Display(Name = "Mô tả")]
        [Column(TypeName = "ntext")]
        public string? Descriptions { get; set; }
    }
}
//dotnet aspnet-codegenerator controller -name Contact -namespace webtintuc.Areas.Contact.Controllers -m webtintuc.Models.ContactModel -udl -dc webtintuc.Models.BlogDbContext -outDir Areas/Contact/Controllers/