using Microsoft.AspNetCore.Mvc;
using webtintuc.Models;

namespace webtintuc.Components
{
    [ViewComponent]
    public class CategorySidebar : ViewComponent
    {
        private readonly BlogDbContext _context;
        public CategorySidebar(BlogDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var model = _context.category.Where(c => c.CategoryName != "Quảng cáo").ToList();
            return View(model);
        }
    }
}