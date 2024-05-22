using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webtintuc.Models;

namespace webtintuc.Components
{
    [ViewComponent]
    public class TopNewsSidebar : ViewComponent
    {
        private readonly BlogDbContext _context;
        public TopNewsSidebar(BlogDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var latelyNews = _context.news.Include(a => a.AuthorModel)
                                        .Include(c => c.CategoryModel)
                                        .OrderByDescending(m => m.Created).Take(5).ToList();
            ViewBag.latelyNews = latelyNews;
            var oldNews = _context.news.Include(a => a.AuthorModel)
                                       .Include(c => c.CategoryModel)
                                       .OrderBy(m => m.Created).Take(5).ToList();
            ViewBag.oldNews = oldNews;
            return View();
        }
    }
}