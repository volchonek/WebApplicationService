using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RESTfullAPIService.Models;

namespace RESTfullAPIService.Mapers
{
    public class UserMap
    {
        public UserMap(EntityTypeBuilder<User> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.Id);
            entityTypeBuilder.ToTable("User");

            entityTypeBuilder.Property(x => x.Id).HasColumnName("Id");
            entityTypeBuilder.Property(x => x.Name).HasColumnName("Name");
        }
    }
}
