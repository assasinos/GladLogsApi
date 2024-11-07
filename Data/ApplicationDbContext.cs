using GladLogsApi.Configuration.DbConfigurations;
using GladLogsApi.Configuration.EntityConfigurations;
using GladLogsApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.IO;

namespace GladLogsApi.Data
{
    public class ApplicationDbContext : DbContext
    {

        /// <summary>
        /// Collection of chats.
        /// </summary>
        public DbSet<Chat> Chats { get; set; }

        /// <summary>
        /// Collection of users who send messages.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Collection of messages sent within chats.
        /// </summary>
        public DbSet<Message> Messages { get; set; }

        /// <summary>
        /// Collection of weeks used to group messages.
        /// </summary>
        public DbSet<Week> Weeks { get; set; }

        /// <summary>
        /// Configuration for the database.
        /// </summary>
        private readonly DbConfig _configuration;

        public ApplicationDbContext(IOptions<DbConfig> configuration)
        {
            _configuration = configuration.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={_configuration.DbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MessageConfiguration());
            modelBuilder.ApplyConfiguration(new ChatConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new WeekConfiguration());
        }
    }
}
