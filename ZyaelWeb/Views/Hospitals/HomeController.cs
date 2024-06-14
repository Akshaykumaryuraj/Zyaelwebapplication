using Microsoft.AspNetCore.Mvc;

namespace ZyaelWeb.Views.Hospitals
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
