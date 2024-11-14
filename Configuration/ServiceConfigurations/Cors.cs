using GladLogsApi.Configuration.ConfigTypes;
using System.Security.Cryptography.X509Certificates;

namespace GladLogsApi.Configuration.ServiceConfigurations
{
    public static class Cors
    {
        private const string CorsPolicyName = "CorsPolicy";
        public static void ConfigCors(this WebApplicationBuilder builder)
        {


            //Get cors configuration
            var corsConfig = builder.Configuration.GetSection("CorsConfig").Get<CorsConfig>();


            if (corsConfig is null)
            {
                builder.Services.AddCors(options =>
                {
                    options.AddPolicy(name: CorsPolicyName,
                                      policy =>
                                      {
                                          policy.AllowAnyOrigin()
                                          .AllowAnyHeader()
                                          .AllowAnyMethod();
                                      });
                });
                return;
            }


            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: CorsPolicyName,
                                  policy =>
                                  {
                                      policy.WithOrigins(corsConfig.AllowedOrigins)
                                      .AllowAnyHeader()
                                      .AllowAnyMethod();
                                  });
            });

        }

        public static void UseCors(this WebApplication app)
        {
            app.UseCors(CorsPolicyName);
        }
    }
}
