using System;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Cronos;

namespace VotingSystem.CronJobs
{

    public abstract class CronServiceBase : IHostedService, IDisposable
    {
        private readonly CronExpression _cronExpression;
        private readonly ILogger<CronServiceBase> _logger;
        private System.Timers.Timer _timer;

        public CronServiceBase(string cronExpression, ILogger<CronServiceBase> logger)
        {
            _cronExpression = CronExpression.Parse(cronExpression);
            _logger = logger;
        }
        protected abstract void ExecuteWork();
        

        private DateTime? GetNextOccurenceDate()
        {
            return _cronExpression.GetNextOccurrence(DateTime.UtcNow, TimeZoneInfo.Local);
        }

        private TimeSpan GetDurationUntil(DateTime? dateTime)
        {
            return dateTime.HasValue
                ? dateTime.Value - DateTime.UtcNow
                : TimeSpan.MinValue;

        }
        private void ScheduleJob(CancellationToken cancellationToken)
        {
            var nextDate = GetNextOccurenceDate();
            var durationUntilNextDate = GetDurationUntil(nextDate);

            while(!nextDate.HasValue || durationUntilNextDate.Milliseconds <= 0)
            {
                nextDate = GetNextOccurenceDate();
                durationUntilNextDate = GetDurationUntil(nextDate);
            }    

            _logger.LogInformation(durationUntilNextDate.Seconds.ToString());
            var countdownTimer = new System.Timers.Timer(durationUntilNextDate.TotalMilliseconds);
            countdownTimer.Elapsed += (source, args) => 
            {
                countdownTimer.Dispose();
                countdownTimer = null;

                if (!cancellationToken.IsCancellationRequested)
                {
                    ExecuteWork();
                }
                if (!cancellationToken.IsCancellationRequested)
                {
                    ScheduleJob(cancellationToken);
                }
            };

            countdownTimer.Start();
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started service");
            if (!cancellationToken.IsCancellationRequested)
            {
                ScheduleJob(cancellationToken);
            }
            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopped service");
            _timer?.Stop();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

    }
}