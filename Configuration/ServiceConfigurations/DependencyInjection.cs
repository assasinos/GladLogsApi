using GladLogsApi.Configuration.DbConfigurations;
using GladLogsApi.Data;
using GladLogsApi.Data.Repositories.CrudRepository;

namespace GladLogsApi.Configuration.ServiceConfigurations
{
    public static class DependencyInjection
    {

        public static void AddDependencies(this WebApplicationBuilder builder)
        {
            //Configure the database
            builder.Services.Configure<DbConfig>(builder.Configuration.GetSection("DbConfig"));
            builder.Services.AddDbContext<ApplicationDbContext>();

            //Add Automapper to make the CRUDRepository easier to maintain and use
            builder.Services.AddAutoMapper(typeof(MappingProfiles));


            //Add the CRUDRepository
            builder.Services.AddScoped(typeof(ICrudRepository<,,,>),typeof(CrudRepository<,,,>));
        }
    }
}
