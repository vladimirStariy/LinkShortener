using LinkShortener.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener.DataLayer
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<Link> Links { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Link>(builder =>
            {
                builder.ToTable("Links").HasKey(x => x.Link_ID);
                builder.Property(x => x.Link_ID).ValueGeneratedOnAdd();
            });
        }
    }
}
