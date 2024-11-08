using GladLogsApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GladLogsApi.Configuration.EntityConfigurations
{
    public class WeekConfiguration : IEntityTypeConfiguration<Week>
    {
        public void Configure(EntityTypeBuilder<Week> builder)
        {
            // Set primary key
            builder.HasKey(w => w.Id);

            // Set properties
            builder.Property(w => w.StartDate).IsRequired();
            builder.Property(w => w.EndDate).IsRequired();

            // Set relationships

            // Each Week can have many Messages, and each Message is associated with one Week.
            builder.HasMany(w => w.Messages)
                   .WithOne(m => m.Week)
                   .HasForeignKey(m => m.WeekId);
        }
    }
}
