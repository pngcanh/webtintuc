using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using webtintuc.Models;

namespace webtintuc.Blog.Controllers
{
    [Authorize(Roles = "admin")]

    public class PartnerController : Controller
    {
        private readonly IWebHostEnvironment _webHost;
        private readonly BlogDbContext _context;
        public PartnerController(IWebHostEnvironment webHost, BlogDbContext context)
        {
            _webHost = webHost;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var partner = _context.partner.ToList();
            return View(partner);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(PartnerViewModel model)
        {
            if (ModelState.IsValid)
            {
                PartnerModel partner = new PartnerModel()
                {
                    CompanyName = model.CompanyName
                };
                if (model.FileUpload != null && model.FileUpload.Length > 0)
                {
                    var foderPath = Path.Combine(_webHost.WebRootPath, "uploads");// duong dan thu muc luu tru
                    Directory.CreateDirectory(foderPath);// tao duong dan

                    string fileName = Path.GetFileNameWithoutExtension(Path.GetFileName(Path.GetRandomFileName()))
                                    + Path.GetExtension(model.FileUpload.FileName);

                    var filePath = Path.Combine(foderPath, fileName);
                    using (var fileStrem = new FileStream(filePath, FileMode.Create))
                    {
                        await model.FileUpload.CopyToAsync(fileStrem);
                    }

                    partner.Logo = "/uploads/" + fileName;
                    _context.Add(partner);

                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

            }
            return View(model);
        }

    }
}