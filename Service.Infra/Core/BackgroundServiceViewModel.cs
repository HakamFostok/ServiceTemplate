using System;

namespace BT_IQM.Domain.BackgroundServices
{
    public class BackgroundServiceViewModel
    {
        public int Id { get; set; }
        public bool Paused { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public int DelayPeriod { get; set; }
        public DateTime? LastExecutedDate { get; set; }
    }
}
