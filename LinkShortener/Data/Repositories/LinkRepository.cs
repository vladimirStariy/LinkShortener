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
    }
}
