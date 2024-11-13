using GladLogsApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GladLogsApi.Configuration.EntityConfigurations
{
    public class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            // Set primary key
            builder.HasKey(c => c.Id);

            // Set properties
            builder.Property(c => c.CreatedAt).IsRequired();

            // Set relationships

            // Each Chat can have many Messages, and each Message is associated with one Chat.
            builder.HasMany(c => c.Messages)
                   .WithOne(m => m.Chat)
                   .HasForeignKey(m => m.ChatId);
        }
    }
}
