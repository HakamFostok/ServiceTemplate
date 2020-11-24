using System.Collections.Generic;
using System.Threading;

namespace BT_IQM.Domain.BackgroundServices
{
    public interface IBackgroundServiceManager
    {
        List<BackgroundServiceViewModel> HostedServices { get; }
        void Resume(string serviceName);
        void Pause(string serviceName);
        void SetDelay(string serviceName, int delayInSecond);
        void Run(string serviceName, CancellationToken stoppingToken);
    }
}
