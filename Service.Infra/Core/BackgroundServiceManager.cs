using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BT_IQM.Domain.BackgroundServices
{
    public class BackgroundServiceManager : IBackgroundServiceManager
    {
        private readonly List<GeneralHostedService> _hostedServices;

        public List<BackgroundServiceViewModel> HostedServices
        {
            get
            {
                var list = _hostedServices.Select(x => x.ToViewModel()).ToList();
                int counter = 1;
                list.ForEach(x => x.Id = counter++);
                return list;
            }
        }

        public BackgroundServiceManager(IServiceProvider serviceProvider)
        {
            _hostedServices = serviceProvider.GetServices<IHostedService>()
                .OfType<GeneralHostedService>()
                .OrderBy(x => x.Name)
                .ToList();
        }

        public void Resume(string serviceName)
        {
            GeneralHostedService service = GetByName(serviceName);
            service?.Resume();
        }

        public void Pause(string serviceName)
        {
            GeneralHostedService service = GetByName(serviceName);
            service?.Pause();
        }

        public void SetDelay(string serviceName, int delayInSecond)
        {
            GeneralHostedService service = GetByName(serviceName);
            service?.SetDelay(delayInSecond);
        }

        public void Run(string serviceName, CancellationToken stoppingToken)
        {
            GeneralHostedService service = GetByName(serviceName);
            service?.Run(stoppingToken);
        }

        private GeneralHostedService GetByName(string serviceName)
        {
            return _hostedServices.FirstOrDefault(x => x.Name == serviceName);
        }
    }
}
