using GladLogsApi.Configuration.DbConfigurations;
using GladLogsApi.Data;

namespace GladLogsApi.Configuration.ServiceConfigurations
{
    public static class DependencyInjection
    {

        public static void AddDependencies(this WebApplicationBuilder builder)
        {
            //Configure the database
            builder.Services.Configure<DbConfig>(builder.Configuration.GetSection("DbConfig"));
            builder.Services.AddDbContext<ApplicationDbContext>();



            //Project seems to small to use a auto mapper but if it grows it would be a good idea to add it here
        }
    }
}
