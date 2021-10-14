using Likr.Likes.Entities;
using Microsoft.EntityFrameworkCore;

namespace Likr.Likes.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Like> Likes { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Like>(x =>
            {
                x.HasKey(l => new { l.ObserverId, l.TargetId });
            });
        }
    }
}