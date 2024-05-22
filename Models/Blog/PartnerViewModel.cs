using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace webtintuc.Models
{
    public class PartnerViewModel
    {
        [Required]
        [Display(Name = "Tên đơn vị")]
        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        public string CompanyName { set; get; }

        [Display(Name = "Logo")]
        [DataType(DataType.Upload)]
        [Required]
        public IFormFile FileUpload { set; get; }
    }
}