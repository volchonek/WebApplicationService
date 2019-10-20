using Microsoft.EntityFrameworkCore;
using RESTfullAPIService.Mapers;

namespace RESTfullAPIService.Models
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            new UserMap(modelBuilder.Entity<User>());

            base.OnModelCreating(modelBuilder);     
        }
    }
}

