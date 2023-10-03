using LinkShortener.DataLayer.Models;
using LinkShortener.DataLayer.Repositories;
using LinkShortener.Middleware;
using LinkShortener.Utilities;
using LinkShortener.ViewModels;

namespace LinkShortener.Services
{
    public class LinkService
    {
        private readonly LinkRepository _linkRepository;

        public LinkService(LinkRepository linkRepository)
        {
            _linkRepository = linkRepository;
        }

        public async Task<IBaseResponce<Link>> Create(EditorLinkViewModel model)
        {
            try
            {
                var link = new Link()
                {
                    Long_Link = model.Long_Link,
                    CreatedAt = DateTime.Now
                };

                await _linkRepository.Create(link);

                link.Short_Link = UrlShortener.ShortenUrl(link.Link_ID, link.Long_Link);
                
                await _linkRepository.Update(link);

                return new BaseResponse<Link>()
                {
                    Result = link,
                    Description = "Success",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Link>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponce<IEnumerable<LinkViewModel>>> Get()
        {
            try
            {
                var links = _linkRepository.Get();
                List<LinkViewModel> linksList = new List<LinkViewModel>();
                foreach(var item in links)
                {
                    var linkViewModel = new LinkViewModel()
                    {
                        Long_Link = item.Long_Link,
                        Short_Link = item.Short_Link,
                        CreatedAt = item.CreatedAt
                    };
                    linksList.Add(linkViewModel);
                }

                return new BaseResponse<IEnumerable<LinkViewModel>> ()
                {
                    Result = linksList,
                    Description = "Success",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<LinkViewModel>>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponce<Link>> GetShortUrlByPath(string path)
        {
            try
            {
                var shortLink = _linkRepository.Get().Where(x => x.Short_Link == path).FirstOrDefault();              

                return new BaseResponse<Link> ()
                {
                    Result = shortLink,
                    Description = "Success",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Link>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }
    }
}
