using DevEncurtaUrl.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevEncurtaUrl.API.Persistence
{
  public class DevShortenedLinkDbContext : DbContext
  {
    public DbSet<ShortenedCustomLink> Links { get; set; }

    public DevShortenedLinkDbContext(DbContextOptions<DevShortenedLinkDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ShortenedCustomLink>(e =>{
            e.HasKey(s => s.Id);
        });
    }
  }
}