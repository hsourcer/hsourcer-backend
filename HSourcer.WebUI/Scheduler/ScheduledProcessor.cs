using HSourcer.WebUI.BackgroundService;
using Microsoft.Extensions.DependencyInjection;
using NCrontab;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HSourcer.WebUI.Scheduler
{
    //code from https://github.com/pgroene/ASPNETCoreScheduler/tree/master/ASPNETCoreScheduler
    public abstract class ScheduledProcessor : ScopedProcessor
    {
        private CrontabSchedule _schedule;
        private DateTime _nextRun;
        protected abstract string Schedule { get; }
        public ScheduledProcessor(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
            _schedule = CrontabSchedule.Parse(Schedule);
            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                var now = DateTime.Now;
                if (now > _nextRun)
                {
                    await Process();
                    _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
                }
                await Task.Delay(5000, stoppingToken); //5 seconds delay
            }
            while (!stoppingToken.IsCancellationRequested);
        }
    }
}
