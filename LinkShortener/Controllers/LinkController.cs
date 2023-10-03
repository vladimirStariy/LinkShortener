using LinkShortener.Services;
using LinkShortener.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LinkShortener.Controllers
{
    public class LinkController : Controller
    {
        private readonly LinkService _linkService;

        public LinkController(LinkService linkService)
        {
            _linkService = linkService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var response = _linkService.Get().Result;
            return View(response.Result);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(EditorLinkViewModel model)
        {
            var response = await _linkService.Create(model);
            return RedirectToAction("Index");
        }

        [HttpGet("/{path:required}")]
        public IActionResult RedirectTo(string path)
        {
            var shortUrl = _linkService.GetShortUrlByPath(path).Result;
            return Redirect(shortUrl.Result.Long_Link);
        }
    }
}
