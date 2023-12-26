using Microsoft.EntityFrameworkCore;
using MockR.Entities;

namespace MockR
{
    public class CacheDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "CacheMock");
        }

        public DbSet<Page> Pages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Page>().HasKey(p => p.Id);
        }
    }
}
