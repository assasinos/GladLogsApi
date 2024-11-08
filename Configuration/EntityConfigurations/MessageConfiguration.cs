using GladLogsApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GladLogsApi.Configuration.EntityConfigurations
{
    // This class configures the Message entity for the Entity Framework Core.
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            // Set primary key
            builder.HasKey(m => m.Id);

            // Set properties
            builder.Property(m => m.Content).IsRequired(); // Content is required
            builder.Property(m => m.Timestamp).IsRequired(); // Timestamp is required

            // Set relationships

            // Each Message is associated with one Week, and each Week can have many Messages.
            builder.HasOne(m => m.Week)
                   .WithMany(w => w.Messages)
                   .HasForeignKey(m => m.WeekId);

            // Each Message is associated with one User, and each User can have many Messages.
            builder.HasOne(m => m.User)
                   .WithMany(u => u.Messages)
                   .HasForeignKey(m => m.UserId);

            // Each Message is associated with one Chat, and each Chat can have many Messages.
            builder.HasOne(m => m.Chat)
                   .WithMany(c => c.Messages)
                   .HasForeignKey(m => m.ChatId);
        }
    }
}
