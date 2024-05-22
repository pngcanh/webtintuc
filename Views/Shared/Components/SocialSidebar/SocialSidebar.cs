using Microsoft.AspNetCore.Mvc;

namespace webtintuc.Components
{
    [ViewComponent]
    public class SocialSidebar : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}