using Microsoft.AspNetCore.Mvc;

namespace LinkShortener.Controllers
{
    public class HomeController : Controller
    {
        

        public HomeController()
        {

        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}