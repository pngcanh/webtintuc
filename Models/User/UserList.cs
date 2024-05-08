using webtintuc.Models;

namespace webtintuc.User.Models
{
    public class UserListModel : AppUser
    {
        public int totalUsers { get; set; }
        public int countPages { get; set; }

        public int ITEMS_PER_PAGE { get; set; } = 10;

        public int currentPage { get; set; }

        public List<UserAndRole> users { get; set; }
    }
    public class UserAndRole : AppUser
    {
        public string? RoleName { get; set; }
    }
}