using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webtintuc.Models
{
    public class PartnerModel
    {
        [Key]
        public int ID { set; get; }
        [Required]
        [Display(Name = "Tên đơn vị")]
        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        public string CompanyName { set; get; }
        [Display(Name = "Logo")]
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string Logo { set; get; }
    }

}