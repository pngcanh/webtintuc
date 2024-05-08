using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using webtintuc.Models;

namespace webtintuc.Blog.Controllers
{
    public class NewsController : Controller
    {
        private readonly BlogDbContext _context;

        public NewsController(BlogDbContext context)
        {
            _context = context;
        }

        // GET: Articles
        public async Task<IActionResult> Index()
        {
            var blogDbContext = _context.news.Include(a => a.AuthorModel).Include(a => a.CategoryModel);
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
        public async Task<IActionResult> Create([Bind("Title,Content,Created,Slug,CategoryID,AuthorID")] NewsModel newsModel)
        {

            if (ModelState.IsValid)
            {
                _context.Add(newsModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuID"] = new SelectList(_context.author, "AuthorID", "AuthorName", newsModel.AuthorModel);
            ViewData["CateID"] = new SelectList(_context.category, "CategoryID", "CategoryName", newsModel.CategoryModel);
            return View(newsModel);
        }

        // GET: Articles/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsModel = await _context.news.FindAsync(id);
            if (newsModel == null)
            {
                return NotFound();
            }
            ViewData["AuID"] = new SelectList(_context.author, "AuthorID", "AuthorName", newsModel.AuthorModel);
            ViewData["CateID"] = new SelectList(_context.category, "CategoryID", "CategoryName", newsModel.AuthorModel);
            return View(newsModel);
        }

        // POST: Articles/Edit

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,Content,Created,Slug,CategoryID,AuthorID")] NewsModel newsModel)
        {
            if (id != newsModel.NewsID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(newsModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticlesModelExists(newsModel.NewsID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuID"] = new SelectList(_context.author, "AuthorID", "AuthorName", newsModel.AuthorModel);
            ViewData["CateID"] = new SelectList(_context.category, "CategoryID", "CategoryName", newsModel.CategoryModel);
            return View(newsModel);
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