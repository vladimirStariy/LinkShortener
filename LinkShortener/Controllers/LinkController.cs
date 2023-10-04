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

        [HttpGet]
        public IActionResult Update(int id)
        {
            var response = _linkService.GetLinkById(id).Result;
            return View(response.Result);
        }

        [HttpPost]
        public async Task<IActionResult> Update(EditorLinkViewModel model)
        {
            var response = await _linkService.Update(model);
            if(response.StatusCode == Utilities.StatusCode.OK)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = "Неккоректная ссылка";
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(EditorLinkViewModel model)
        {
            var response = await _linkService.Create(model);
            if(response.StatusCode == Utilities.StatusCode.OK)
                return RedirectToAction("Index");
            else
            {
                ViewBag.Error = response.Description;
                return View();
            }   
        }

        [HttpGet]
        [Route("{path}", Name = "specific")]
        public IActionResult RedirectTo(string path)
        {
            var shortUrl = _linkService.GetShortUrlByPath(path).Result;
            return Redirect(shortUrl.Result.Long_Link);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _linkService.Delete(id);
            if(response.StatusCode == Utilities.StatusCode.OK)
            {
                return RedirectToAction("Index");
            }
            return Ok();
        }
    }
}
