using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using webtintuc.Helper;
using webtintuc.Models;
namespace webtintuc.Blog.Controllers
{
    [AllowAnonymous]
    public class NewsViewController : Controller
    {
        private readonly BlogDbContext _context;

        public NewsViewController(BlogDbContext context)
        {
            _context = context;
        }
        // [Route("/the-loai/{slugCate?}")]
        public async Task<IActionResult> Index(string slugCate, int pageSize, [FromQuery(Name = "p")] int currentPage)
        {
            var categories = _context.category.ToList(); // lay ra tat ca the loai
            CategoryModel category = null;
            if (!string.IsNullOrEmpty(slugCate))
            {
                category = await _context.category.Where(c => c.Slug == slugCate)
                                            .FirstOrDefaultAsync();
                if (category == null)
                {
                    return NotFound();
                }
            }
            ViewBag.category = category;
            var news = _context.news.Include(a => a.AuthorModel)
                                    .Include(c => c.CategoryModel)
                                    .AsQueryable();

            if (category != null)
            {
                news = news.Where(n => n.CategoryID == category.CategoryID);

            }

            //lay bai viet moi nhat
            var newsLately = _context.news.OrderByDescending(n => n.Created).FirstOrDefault();
            ViewBag.newsLately = newsLately;

            //lay bai  viet moi nhat theo the loai
            var newsLatelyByCate = _context.news.OrderByDescending(n => n.Created).Where(n => n.CategoryModel.Slug == slugCate).FirstOrDefault();
            ViewBag.newsLatelyByCate = newsLatelyByCate;

            //lay bai viet nhieu view
            var topView = _context.news.Include(a => a.AuthorModel)
                                        .Include(c => c.CategoryModel)
                                        .OrderByDescending(v => v.ViewNumber)
                                        .Take(3)
                                        .ToList();
            ViewBag.topView = topView;

            //lay bai viet nhieu view theo the loai
            var topviewByCate = _context.news
                                        .Include(a => a.AuthorModel)
                                        .Include(c => c.CategoryModel)
                                        .Where(n => n.CategoryModel.Slug == slugCate)
                                        .OrderByDescending(v => v.ViewNumber)
                                        .Take(3)
                                        .ToList();
            ViewBag.topviewByCate = topviewByCate;

            //lay cac bai cung the loai con lai
            var ortherNews = _context.news.Include(a => a.AuthorModel)
                                            .Include(c => c.CategoryModel)
                                            .Where(n => n.CategoryModel.Slug == slugCate)
                                            .ToList()
                                            .Except(topView)
                                            .Skip(1);
            //doi tac
            var partner = _context.partner.ToList();
            ViewBag.partner = partner;

            //phan trang
            var totalOtherNews = ortherNews.Count();//tong so bai viet

            if (pageSize <= 0) pageSize = 5;
            var countPage = (int)Math.Ceiling((double)totalOtherNews / pageSize);//tong so trang

            if (currentPage > countPage) currentPage = countPage;
            if (currentPage < 1) currentPage = 1;

            var pagingModel = new PagingModel()
            {
                countpages = countPage,
                currentpage = currentPage,
                generateUrl = (pageNumber) => Url.Action("Index", new
                {
                    p = pageNumber,
                    pageSize = pageSize
                })
            };

            var newsInPage = ortherNews.Skip((currentPage - 1) * pageSize)
                                    .Take(pageSize);

            ViewBag.pagingModel = pagingModel;
            ViewBag.totalOtherNews = totalOtherNews;
            ViewBag.newsInPage = newsInPage;

            return View(news.ToList());
        }

        [Route("/bai-viet/{slug}")]
        public async Task<IActionResult> Detail(string slug)
        {
            var news = _context.news.Where(n => n.Slug == slug)
                                    .Include(a => a.AuthorModel)
                                    .Include(c => c.CategoryModel)
                                    .FirstOrDefault();
            if (news == null)
            {
                return NotFound();
            }
            else
            {
                news.ViewNumber++;
                await _context.SaveChangesAsync();
            }

            //lay cac bai viet  khac cung the loai
            var ortherNews = _context.news.Where(n => n.NewsID != news.NewsID && n.CategoryID == news.CategoryID)
                                            .OrderByDescending(n => n.Created).Take(5).AsQueryable();
            ViewBag.ortherNews = ortherNews;
            return View(news);
        }
        public async Task<IActionResult> Search(string searchString)
        {
            var news = await _context.news.Include(a => a.AuthorModel)
                                    .Include(c => c.CategoryModel).ToListAsync();
            if (!string.IsNullOrEmpty(searchString))
            {

                news = news.Where(n => n.Title.ToLowerInvariant().Contains(searchString.ToLowerInvariant())).ToList();
            }
            ViewBag.ResultNumber = news.Count();
            return View(news);
        }

    }
}