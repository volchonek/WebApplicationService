using Microsoft.EntityFrameworkCore;
using RESTfulAPIService.Mapers;
using RESTfulAPIService.Models;

namespace RESTfulAPIService.DbContext
{
    /// <summary>
    /// Depended database context for user
    /// </summary>
    public class UserDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        /// <summary>
        /// </summary>
        /// <param name="options"></param>
        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Create structure in database 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            modelBuilder.Entity<User>().UserMap();

            base.OnModelCreating(modelBuilder);     
        }
    }
}

