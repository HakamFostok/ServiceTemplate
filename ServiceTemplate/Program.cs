using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using System;
using TemplateService;

var logger = LogManager.LoadConfiguration("nlog.config").GetCurrentClassLogger();

try
{
    logger.Info("Starting up service.");
    CreateHostBuilder(args).Build().Run();
}
catch (Exception exception)
{
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddConsole();
            logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
        })
        .UseNLog()
        .UseWindowsService()
        .ConfigureServices((hostContext, services) =>
        {
            services.Register(hostContext);
        });
