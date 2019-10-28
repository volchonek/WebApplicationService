using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RESTfulAPIService.Models;

namespace RESTfulAPIService.DbContext
{
    /// <summary>
    ///     Extension user database context with help method OnModelCreating in UserDbContext
    /// </summary>
    public static class ExtensionUserDbContext
    {
        /// <summary>
        ///     User map for user model
        /// </summary>
        /// <param name="entityTypeBuilder"></param>
        public static void UserMap(this EntityTypeBuilder<User> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.Id);
            entityTypeBuilder.ToTable("User");

            entityTypeBuilder.Property(x => x.Id).HasColumnName("Guid");
            entityTypeBuilder.Property(x => x.Name).HasColumnName("Name");
        }
    }
}