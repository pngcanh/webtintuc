using System.ComponentModel;
using Microsoft.Build.Framework;
using webtintuc.Models;

namespace webtintuc.User.Models
{
    public class AddRoleForUserModel
    {
        public AppUser appUser { set; get; }
        [DisplayName("Chọn role muốn gán")]
        public string[] RoleName { set; get; }

    }

}