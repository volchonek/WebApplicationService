using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RESTfullAPIService.Models;

namespace RESTfullAPIService.Mapers
{
    public class UserMap
    {
        public UserMap(EntityTypeBuilder<User> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.Guid);
            entityTypeBuilder.ToTable("User");

            entityTypeBuilder.Property(x => x.Guid).HasColumnName("Guid");
            entityTypeBuilder.Property(x => x.Name).HasColumnName("Name");
        }
    }
}
