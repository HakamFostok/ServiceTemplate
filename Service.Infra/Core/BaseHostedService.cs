using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BT_IQM.Domain.BackgroundServices
{
    public abstract class BaseHostedService<T> : GeneralHostedService
    {
        private readonly ILogger<T> _logger;

        protected BaseHostedService(int delayPeriod, IServiceProvider serviceProvider, ILogger<T> logger)
            : base(delayPeriod, serviceProvider)
        {
            _logger = logger;
        }

        protected void Log(Exception ex)
        {
            _logger.LogError(ex.ToString());
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await InitialDelay(stoppingToken);
            await MainLogic(stoppingToken);
        }

        protected virtual async Task InitialDelay(CancellationToken stoppingToken)
        {
            // (Hey this is Hakan FISITK talking to you)
            // This method in this class is extremely important
            // Do NOT remove this method, and do NOT remove the following waiting statement
            // The reason of this waiting is to make the startup of the project quickly
            // If we do not make this waiting, then the website will not start quickly
            // In that case the webiste will not start until the ExecuteAsync method of each HostedService finish
            // which could take time to complete, and as consequence it will delay the starat of the website
            await Task.Delay(100);
        }

        protected virtual async Task MainLogic(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    if (Paused)
                        return;

                    await Run(stoppingToken);
                }
                catch (Exception ex)
                {
                    Log(ex);
                }
                finally
                {
                    await UpdateLastExecutionDateAndDelay(stoppingToken);
                }
            }
        }

       
    }
}
