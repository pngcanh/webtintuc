using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using webtintuc.Models;
using webtintuc.Role.Models;

namespace webtintuc.Controllers
{
    [Authorize(Roles = "admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly BlogDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public RoleController(RoleManager<IdentityRole> roleManager, BlogDbContext context, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _context = context;
            _userManager = userManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        //GET: hien thi danh sach role
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var role = await _roleManager.Roles.ToListAsync();
            return View(role);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleModel createRoleModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var newRole = new IdentityRole(createRoleModel.Name!);
            var result = await _roleManager.CreateAsync(newRole);
            if (result.Succeeded)
            {
                StatusMessage = $"Bạn vừa tạo mới role: {newRole.Name}";
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var erro in result.Errors)
                {
                    ModelState.AddModelError("", erro.Description);
                }
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return NotFound("Không tìm thấy role");
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound("Không tìm thấy role");
            }
            return View(role);
        }
        //POST : Xóa role
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(string id)
        {
            if (id == null) return NotFound("role không tồn tại");
            var role = await _roleManager.FindByIdAsync(id);
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                StatusMessage = $"Bạn vừa xóa {role.Name}";
                return View("Index");
            }
            else
            {
                foreach (var r in result.Errors)
                {
                    ModelState.AddModelError("", r.Description);
                }
            }
            return View(role);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id, [Bind("Name")] EditRoleModel editRoleModel)
        {
            if (id == null) return NotFound("Không tìm thấy role");
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound("Role không tồn tại");
            }
            editRoleModel.Name = role.Name;
            return View(editRoleModel);
        }
        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> EditConfirm(string id, [Bind("Name")] EditRoleModel editRoleModel)
        {
            if (id == null) return NotFound();
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            role.Name = editRoleModel.Name;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                StatusMessage = $"Bạn vừa cập nhật role {editRoleModel.Name}";
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }
            return View(editRoleModel);
        }
    }
}