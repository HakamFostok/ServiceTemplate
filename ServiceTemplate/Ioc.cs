using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http;

namespace TemplateService
{
    public static class Ioc
    {
        public static void Register(this IServiceCollection services, HostBuilderContext hostContext)
        {
            RegisterAppSettings(hostContext.Configuration, services);
            RegisterCommonServices(services);
        }

        private static void RegisterAppSettings(IConfiguration configuration, IServiceCollection services)
        {
            //IAppSettingsConfigurations appSettings = new AppSettingsConfigurations();
            //configuration.Bind("AppSettings", appSettings);
            //services.AddSingleton(appSettings);

            //IConnectionStrings connectionStringsConfiguration = new ConnectionStrings();
            //configuration.Bind("ConnectionStrings", connectionStringsConfiguration);
            //services.AddSingleton(connectionStringsConfiguration);
        }

        private static void RegisterCommonServices(IServiceCollection services)
        {
            services.AddTransient<HttpClient>();

            //services.AddDbContext<BTContext>((provider, optionsBuilder) =>
            //{
            //    optionsBuilder.EnableSensitiveDataLogging();
            //    var connectionString = provider.GetService<IConnectionStrings>();

            //    optionsBuilder.UseSqlServer(connectionString.DefaultConnection);
            //}, ServiceLifetime.Scoped, ServiceLifetime.Scoped);
        }

        private static void RegisterWorkersVeriToplama(IServiceCollection services)
        {
            //services.AddHostedService<OrcaVeriToplamaWorker>();
        }
    }
}