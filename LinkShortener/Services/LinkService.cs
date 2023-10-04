using LinkShortener.DataLayer.Models;
using LinkShortener.DataLayer.Repositories;
using LinkShortener.Middleware;
using LinkShortener.Utilities;
using LinkShortener.ViewModels;
using System.IO;

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
                    CreatedAt = DateTime.Now,
                    RedirectCount = 0
                };

                await _linkRepository.Create(link);
                
                try
                {
                   link.Short_Link = UrlShortener.ShortenUrl(link.Link_ID, link.Long_Link);
                }
                catch (Exception ex)
                {
                    await _linkRepository.Delete(link);
                    return new BaseResponse<Link>()
                    {
                        StatusCode = StatusCode.InternalServerError,
                        Description = $"Некоректная ссылка"
                    };
                }

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

        public async Task<IBaseResponce<Link>> Update(EditorLinkViewModel model)
        {
            try
            {
                var link = _linkRepository.Get().Where(x => x.Link_ID == model.id).FirstOrDefault();

                if(!UrlShortener.ValidateUrl(model.Long_Link))
                    return new BaseResponse<Link>()
                    {
                        StatusCode = StatusCode.InternalServerError,
                        Description = $"Некорректная ссылка"
                    };

                link.Long_Link = model.Long_Link;

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
                        Link_ID = item.Link_ID,
                        Long_Link = item.Long_Link,
                        Short_Link = item.Short_Link,
                        CreatedAt = item.CreatedAt,
                        RedirectCount = item.RedirectCount,
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

        public async Task<IBaseResponce<EditorLinkViewModel>> GetLinkById(int id)
        {
            try
            {
                var link = _linkRepository.Get().Where(x => x.Link_ID == id).FirstOrDefault();
                EditorLinkViewModel linkViewModel = new EditorLinkViewModel() { id = link.Link_ID, Long_Link = link.Long_Link };
                
                return new BaseResponse<EditorLinkViewModel>()
                {
                    Result = linkViewModel,
                    Description = "Success",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<EditorLinkViewModel>()
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
                shortLink.RedirectCount += 1;
                await _linkRepository.Update(shortLink);

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

        public async Task<IBaseResponce<Link>> Delete (int id)
        {
            try
            {
                var link = _linkRepository.Get().Where(x => x.Link_ID == id).FirstOrDefault();
                await _linkRepository.Delete(link);

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
    }
}
