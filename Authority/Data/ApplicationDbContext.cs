using Authority.Models;
using Microsoft.EntityFrameworkCore;

namespace Authority.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(new User { UserId = 1, Username = "admin", Password = "admin", Email = "admin@pslib.cz", Admin = 1 });
            modelBuilder.Entity<User>().HasData(new User { UserId = 2, Username = "user", Password = "user", Email = "user@pslib.cz", Admin = 0 });
        }
    }
}
