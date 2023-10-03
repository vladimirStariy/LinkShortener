using LinkShortener.DataLayer.Models;
using NHibernate;

namespace LinkShortener.DataLayer.Repositories
{
    public class LinkRepository : IMapperSession<Link>
    {
        private readonly ISession _session;

        public LinkRepository(ISession session) 
        {
            _session = session;
        }

        public IQueryable<Link> Links => _session.Query<Link>();

        public async Task Save(Link model)
        {
            await _session.SaveOrUpdateAsync(model);
        }

        public async Task Delete(Link model)
        {
            await _session.DeleteAsync(model);
        }
    }
}
