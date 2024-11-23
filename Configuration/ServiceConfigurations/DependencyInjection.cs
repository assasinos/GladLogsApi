using GladLogsApi.Attributes;
using GladLogsApi.Configuration.ConfigTypes;
using GladLogsApi.Configuration.DbConfigurations;
using GladLogsApi.Data;
using GladLogsApi.Data.Repositories.CrudRepository;
using GladLogsApi.Data.Services.TwitchConnectionService;
using GladLogsApi.Data.Services.ChatService;
using GladLogsApi.Data.Services.MessageService;
using GladLogsApi.Data.Services.UserService;
using GladLogsApi.Data.Services.WeekService;
using GladLogsApi.Data.Services.LifeCycleService;

namespace GladLogsApi.Configuration.ServiceConfigurations
{
    public static class DependencyInjection
    {

        public static void AddDependencies(this WebApplicationBuilder builder)
        {
            //Configure the DI
            builder.ConfigureServices();

            builder.Services.AddDbContext<ApplicationDbContext>();



            //Add Automapper to make the CRUDRepository easier to maintain and use
            builder.Services.AddAutoMapper(typeof(MappingProfiles));


            //Add the CRUDRepository
            builder.Services.AddScoped(typeof(ICrudRepository<,,,>),typeof(CrudRepository<,,,>));

            //Add the services
            builder.Services.AddServices();

            builder.Services.AddScoped<ValidateAuthKeyAttribute>();

            builder.Services.AddBackgroundServices();

        }



        /// <summary>
        /// Function contains all the custom services that need to be added
        /// </summary>
        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IWeekService, WeekService>();
            services.AddScoped<IUserService, UserService>();
        }

        private static void AddBackgroundServices(this IServiceCollection services)
        {
            services.AddHostedService<LifeCycleService>();
            services.AddSingleton<ITwitchConnectionService,TwitchConnectionService>();
        }


        /// <summary>
        /// Function to configure the DI
        /// </summary>
        private static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<AuthConfig>(builder.Configuration.GetSection("AuthConfig"));
            builder.Services.Configure<DbConfig>(builder.Configuration.GetSection("DbConfig"));
            builder.Services.Configure<TwitchAuthConfig>(builder.Configuration.GetSection("TwitchAuth"));
        }

    }
}
