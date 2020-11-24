using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BT_IQM.Domain.BackgroundServices
{
    public abstract class GeneralHostedService : BackgroundService
    {
        protected readonly IServiceProvider _serviceProvider;
        public readonly string Name;
        public readonly string FullName;

        protected bool Paused { get; private set; }

        protected int DelayPeriod { get; private set; } = 60 * 1000;
        protected DateTime? LastExecutedDate { get; private set; }

        protected GeneralHostedService()
        {
            Type serviceRealType = GetType();
            FullName = serviceRealType.FullName;
            Name = serviceRealType.Name;
        }

        protected GeneralHostedService(int delayPeriod, IServiceProvider serviceProvider) : this()
        {
            _serviceProvider = serviceProvider;

            SetDelay(delayPeriod);
        }

        protected async Task UpdateLastExecutionDateAndDelay(CancellationToken stoppingToken)
        {
            if (!Paused)
                UpdateLastExecutedDate();

            await Task.Delay(DelayPeriod, stoppingToken);
        }

        private void UpdateLastExecutedDate() => LastExecutedDate = DateTime.Now;
        public void Pause() => Paused = true;
        public void Resume() => Paused = false;

        public void SetDelay(int delayInSecond)
        {
            if (delayInSecond < 1)
                throw new InvalidOperationException("Delay value can not be zero or negative");

            DelayPeriod = delayInSecond;
        }



        public abstract Task Run(CancellationToken cancellationToken);

        public BackgroundServiceViewModel ToViewModel()
        {
            return new BackgroundServiceViewModel
            {
                Name = Name,
                FullName = FullName,
                DelayPeriod = DelayPeriod,
                LastExecutedDate = LastExecutedDate,
                Paused = Paused
            };
        }
    }
}
