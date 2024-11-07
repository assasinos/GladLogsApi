using GladLogsApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GladLogsApi.Configuration.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Set primary key
            builder.HasKey(u => u.UserId);

            // Set properties
            builder.Property(u => u.Name).IsRequired();
            builder.Property(u => u.CreatedAt).IsRequired();

            // Set relationships

            //Each User can have many Messages, and each Message is associated with one User.
            builder.HasMany(u => u.Messages)
                   .WithOne(m => m.User)
                   .HasForeignKey(m => m.UserId);


        }
    }
}
