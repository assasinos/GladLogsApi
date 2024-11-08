using GladLogsApi.Configuration.AuthConfigurations;
using GladLogsApi.Configuration.DbConfigurations;
using GladLogsApi.Data;
using GladLogsApi.Data.Repositories.CrudRepository;
using GladLogsApi.Data.Services.ChatService;

namespace GladLogsApi.Configuration.ServiceConfigurations
{
    public static class DependencyInjection
    {

        public static void AddDependencies(this WebApplicationBuilder builder)
        {
            //Configure the DI
            builder.ConfigureServices();

            builder.Services.AddDbContext<ApplicationDbContext>();


            //Maybe this should be moved into a static class in the Configuration folder
            builder.Services.Configure<AuthConfig>(builder.Configuration.GetSection("AuthConfig"));

            //Add Automapper to make the CRUDRepository easier to maintain and use
            builder.Services.AddAutoMapper(typeof(MappingProfiles));


            //Add the CRUDRepository
            builder.Services.AddScoped(typeof(ICrudRepository<,,,>),typeof(CrudRepository<,,,>));

            //Add the services
            builder.Services.AddServices();
        }



        /// <summary>
        /// Function contains all the custom services that need to be added
        /// </summary>
        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IChatService, ChatService>();
        }



        /// <summary>
        /// Function to configure the DI
        /// </summary>
        private static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<AuthConfig>(builder.Configuration.GetSection("AuthConfig"));
            builder.Services.Configure<DbConfig>(builder.Configuration.GetSection("DbConfig"));

        }

    }
}
