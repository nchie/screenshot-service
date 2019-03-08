
using Microsoft.EntityFrameworkCore;

namespace Screenshot.Service 
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }
        public DbSet<ScreenshotRequestModel> ScreenshotRequests { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ScreenshotRequestModel>()
                .HasKey(c => c.Guid);
        }
    }
}