using LinkShortener.DataLayer.Models;

namespace LinkShortener.DataLayer.Repositories
{
    public class LinkRepository
    {
        private readonly ApplicationDbContext _db;

        public LinkRepository(ApplicationDbContext db) 
        {
            _db = db;
        }

        public async Task Create(Link entity)
        {
            await _db.Links.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<Link> Update(Link entity)
        {
            _db.Links.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(Link entity)
        {
            _db.Links.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IEnumerable<Link> Get()
        {
            return _db.Links;
        }
    }
}
