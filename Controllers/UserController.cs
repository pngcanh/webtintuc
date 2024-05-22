using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using webtintuc.Models;
using webtintuc.User.Models;

namespace webtintuc.Controllers
{
    [Authorize(Roles = "admin")]
    public class UserController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly BlogDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public UserController(RoleManager<IdentityRole> roleManager, BlogDbContext context, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _context = context;
            _userManager = userManager;
        }
        [TempData]
        public string StatusMessage { set; get; }
        public async Task<IActionResult> Index([FromQuery(Name = "p")] int currentPage)
        {
            var model = new UserListModel();
            model.currentPage = currentPage;

            var qr = _userManager.Users.OrderBy(u => u.UserName);

            model.totalUsers = await qr.CountAsync();
            model.countPages = (int)Math.Ceiling((double)model.totalUsers / model.ITEMS_PER_PAGE);

            if (model.currentPage < 1)
                model.currentPage = 1;
            if (model.currentPage > model.countPages)
                model.currentPage = model.countPages;

            var qr1 = qr.Skip((model.currentPage - 1) * model.ITEMS_PER_PAGE)
                        .Take(model.ITEMS_PER_PAGE)
                        .Select(u => new UserAndRole()
                        {
                            Id = u.Id,
                            UserName = u.UserName,
                        });

            model.users = await qr1.ToListAsync();

            foreach (var user in model.users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                user.RoleName = string.Join(",", roles);
            }

            return View(model);
        }
        //GET gán role cho user
        [HttpGet]
        public async Task<IActionResult> AddRole(string id)
        {
            var model = new AddRoleForUserModel();
            if (id.IsNullOrEmpty())
            {
                return NotFound("Người dùng không tồn tại");
            }
            model.appUser = await _userManager.FindByIdAsync(id);
            if (model.appUser == null)
            {
                return NotFound("Người dùng không tồn tại");
            }
            model.RoleName = (await _userManager.GetRolesAsync(model.appUser)).ToArray();
            List<string> roleName = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            ViewBag.allRoles = new SelectList(roleName);
            return View(model);
        }
        //POST gán role cho user
        [HttpPost, ActionName("AddRole")]
        public async Task<IActionResult> AddRoleConfirm(string id, [Bind("RoleName")] AddRoleForUserModel model)
        {
            if (id.IsNullOrEmpty()) return NotFound();
            model.appUser = await _userManager.FindByIdAsync(id);
            if (model.appUser == null)
            {
                return NotFound("Không tìm thấy người dùng");
            }
            var oldRoleName = (await _userManager.GetRolesAsync(model.appUser)).ToArray();
            var deleteRoleName = oldRoleName.Where(r => !model.RoleName.Contains(r));
            var addRoleName = model.RoleName.Where(r => !oldRoleName.Contains(r));
            List<string> roleName = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            ViewBag.allRoles = new SelectList(roleName);
            var resultDelete = await _userManager.RemoveFromRolesAsync(model.appUser, deleteRoleName);
            if (!resultDelete.Succeeded)
            {
                foreach (var error in resultDelete.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View();

            }
            var resultAdd = await _userManager.AddToRolesAsync(model.appUser, addRoleName);
            if (!resultAdd.Succeeded)
            {
                foreach (var error in resultAdd.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View();

            }
            StatusMessage = $"Bạn vừa cập nhật role cho {model.appUser.UserName}";
            return RedirectToAction("Index");

        }
    }


}