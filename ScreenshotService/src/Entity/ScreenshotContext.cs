
using Microsoft.EntityFrameworkCore;

namespace Screenshot.Service.Entity
{
    public class ScreenshotContext : DbContext
    {
        public ScreenshotContext(DbContextOptions<ScreenshotContext> options)
            : base(options)
        {
        }
        public DbSet<RequestEntity> ScreenshotRequests { get; set; }
        public DbSet<ScreenshotEntity> Screenshots { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RequestEntity>()
                .HasKey(c => c.Guid);
            
            modelBuilder.Entity<RequestEntity>()
                .HasMany(e => e.Screenshots)
                .WithOne()
                .HasForeignKey(e => e.RequestGuid);

            modelBuilder.Entity<ScreenshotEntity>()
                .HasKey(c => new { c.RequestGuid, c.Url });
        }
    }
}