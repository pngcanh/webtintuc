using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using webtintuc.Helper;
using webtintuc.Models;

namespace webtintuc.Blog.Controllers
{
    [Authorize(Roles = "admin")]

    public class NewsController : Controller
    {
        private readonly BlogDbContext _context;
        //lay thong tin ve sever 
        private readonly IWebHostEnvironment _hostingEnvironment;

        public NewsController(BlogDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Articles
        public async Task<IActionResult> Index()
        {
            var blogDbContext = _context.news.Include(a => a.AuthorModel).Include(a => a.CategoryModel).OrderByDescending(n => n.Created);
            return View(await blogDbContext.ToListAsync());
        }

        // GET: Articles/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsModel = await _context.news
                .Include(a => a.AuthorModel)
                .Include(a => a.AuthorModel)
                .FirstOrDefaultAsync(m => m.NewsID == id);
            if (newsModel == null)
            {
                return NotFound();
            }

            return View(newsModel);
        }

        // GET: Articles/Create
        public IActionResult Create()
        {
            ViewData["AuID"] = new SelectList(_context.author, "AuthorID", "AuthorName");
            ViewData["CateID"] = new SelectList(_context.category, "CategoryID", "CategoryName");
            return View();
        }

        // POST: Articles/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewsPhotoViewModel model)
        {
            //tu dong phat sinh chuoi url cho bai viet
            if (model.Slug == null)
            {
                model.Slug = WebUtilities.GenerateSlug(model.Title);
            }

            if (ModelState.IsValid)
            {
                var news = new NewsModel() //gan du lieu
                {
                    Title = model.Title,
                    Slug = model.Slug,
                    Content = model.Content,
                    Created = model.Created,
                    CategoryID = model.CategoryID,
                    AuthorID = model.AuthorID
                };

                if (model.FileUpload != null && model.FileUpload.Length > 0)
                {
                    var uploadFoderPath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads"); // tao duong dan
                    Directory.CreateDirectory(uploadFoderPath); // tao thu muc moi tai duong dan 

                    var file = Path.GetFileNameWithoutExtension(Path.GetFileName(Path.GetRandomFileName()))
                                        + Path.GetExtension(model.FileUpload.FileName);

                    var filePath = Path.Combine(uploadFoderPath, file);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.FileUpload.CopyToAsync(stream);
                    }

                    news.Photo = "/uploads/" + file;
                    _context.Add(news);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["AuID"] = new SelectList(_context.author, "AuthorID", "AuthorName", model.AuthorModel);
            ViewData["CateID"] = new SelectList(_context.category, "CategoryID", "CategoryName", model.CategoryModel);
            return View(model);
        }

        // GET: Articles/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.news.Include(n => n.AuthorModel)
                                            .Include(n => n.CategoryModel)
                                            .Where(n => n.NewsID == id)
                                            .FirstOrDefaultAsync();
            if (news == null)
            {
                return NotFound();
            }
            var viewModel = new NewsPhotoViewModel()
            {
                NewsID = news.NewsID,
                Title = news.Title,
                Content = news.Content,
                AuthorID = news.AuthorID,
                CategoryID = news.CategoryID,
                Created = news.Created
            };

            ViewData["AuID"] = new SelectList(_context.author, "AuthorID", "AuthorName", viewModel.AuthorModel);
            ViewData["CateID"] = new SelectList(_context.category, "CategoryID", "CategoryName", viewModel.AuthorModel);
            return View(viewModel);
        }

        // POST: Articles/Edit

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(NewsPhotoViewModel model)
        {


            if (ModelState.IsValid)
            {
                // tu dong phat sinh url
                if (model.Slug == null)
                {
                    model.Slug = WebUtilities.GenerateSlug(model.Title);
                }
                var news = await _context.news.FindAsync(model.NewsID);
                if (news == null)
                {
                    return NotFound();
                }
                if (model.FileUpload.Length > 0 && model.FileUpload != null)
                {
                    var uploadFoderPath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                    Directory.CreateDirectory(uploadFoderPath);

                    var file = Path.GetFileNameWithoutExtension(Path.GetFileName(Path.GetRandomFileName()))
                                    + Path.GetExtension(model.FileUpload.FileName);

                    var filePath = Path.Combine(uploadFoderPath, file);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.FileUpload.CopyToAsync(fileStream);
                    }
                    news.Photo = "/uploads/" + file;
                    _context.Update(news);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            ViewData["AuID"] = new SelectList(_context.author, "AuthorID", "AuthorName", model.AuthorModel);
            ViewData["CateID"] = new SelectList(_context.category, "CategoryID", "CategoryName", model.CategoryModel);
            return View(model);
        }

        // GET: Articles/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsModel = await _context.news
                .Include(a => a.AuthorModel)
                .Include(a => a.AuthorModel)
                .FirstOrDefaultAsync(m => m.NewsID == id);
            if (newsModel == null)
            {
                return NotFound();
            }

            return View(newsModel);
        }

        // POST: Articles/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var newsModel = await _context.news.FindAsync(id);
            if (newsModel != null)
            {
                _context.news.Remove(newsModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticlesModelExists(int id)
        {
            return _context.news.Any(e => e.NewsID == id);
        }

    }
}