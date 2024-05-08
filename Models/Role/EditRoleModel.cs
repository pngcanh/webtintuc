using System.ComponentModel.DataAnnotations;

namespace webtintuc.Role.Models
{
    public class EditRoleModel
    {
        [Display(Name = "Tên role")]
        [StringLength(255, MinimumLength = 2)]
        [Required(ErrorMessage = "Vui lòng nhập {0}")]
        public string? Name { set; get; }
    }
}