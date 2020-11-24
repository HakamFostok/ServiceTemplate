using BT_IQM.Domain.BackgroundServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Service.Infra.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BT_IQM.Application.BackgroundServices
{
    public class DemoBelirsizDurusHostedService : BaseHostedService<DemoBelirsizDurusHostedService>
    {
        public DemoBelirsizDurusHostedService(IServiceProvider serviceProvider, ILogger<DemoBelirsizDurusHostedService> logger)
            : base(1000, serviceProvider, logger)
        {
        }

        public async override Task Run(CancellationToken cancellationToken)
        {
            using IServiceScope scope = _serviceProvider.CreateScope();

            using IAppService demoAppServices = scope.ServiceProvider.GetService<IAppService>();

            //await demoAppServices.BelirsizDurusDemo();
        }
    }
}
