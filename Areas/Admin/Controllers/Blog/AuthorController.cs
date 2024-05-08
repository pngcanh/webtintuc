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
    public class AuthorController : Controller
    {
        private readonly BlogDbContext _context;

        public AuthorController(BlogDbContext context)
        {
            _context = context;
        }

        // GET: Authors
        public async Task<IActionResult> Index()
        {
            return View(await _context.author.ToListAsync());
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorsModle = await _context.author
                .FirstOrDefaultAsync(m => m.AuthorID == id);
            if (authorsModle == null)
            {
                return NotFound();
            }

            return View(authorsModle);
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuthorName,Gender,Email,PhoneNumber")] AuthorModel authorsModle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(authorsModle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(authorsModle);
        }

        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorsModle = await _context.author.FindAsync(id);
            if (authorsModle == null)
            {
                return NotFound();
            }
            return View(authorsModle);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AuthorID,AuthorName,Gender,Email")] AuthorModel authorsModle)
        {
            if (id != authorsModle.AuthorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(authorsModle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorsModleExists(authorsModle.AuthorID))
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
            return View(authorsModle);
        }

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorsModle = await _context.author
                .FirstOrDefaultAsync(m => m.AuthorID == id);
            if (authorsModle == null)
            {
                return NotFound();
            }

            return View(authorsModle);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var authorsModle = await _context.author.FindAsync(id);
            if (authorsModle != null)
            {
                _context.author.Remove(authorsModle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorsModleExists(int id)
        {
            return _context.author.Any(e => e.AuthorID == id);
        }
    }
}