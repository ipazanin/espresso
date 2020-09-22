using System;
using System.Threading;
using System.Threading.Tasks;
using Cronos;
using Espresso.Common.Enums;
using Espresso.Common.Utilities;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Timer = System.Timers.Timer;

namespace Espresso.WebApi.Application.Infrastructure.CronJobsInfrastructure
{
    public abstract class CronJob<T> : IHostedService, IDisposable where T : CronJob<T>
    {
        #region Fields
        private Timer? _timer;
        private readonly CronExpression _expression;
        private readonly TimeZoneInfo? _timeZoneInfo;
        private readonly ILogger<T> _logger;
        #endregion

        #region Constructors
        protected CronJob(ICronJobConfiguration<T> cronJobConfiguration, ILoggerFactory loggerFactory)
        {
            _expression = CronExpression.Parse(cronJobConfiguration.CronExpression);
            _timeZoneInfo = cronJobConfiguration.TimeZoneInfo;
            _logger = loggerFactory.CreateLogger<T>();
        }
        #endregion

        #region Methods
        public virtual async Task StartAsync(CancellationToken cancellationToken)
        {
            var eventName = $"{typeof(T).Name} initialised";

            _logger.LogInformation(
                eventId: new EventId(
                    id: (int)Event.CronJob,
                    name: eventName
                ),
                message: $"{AnsiUtility.EncodeEventName("{0}")}",
                args: new object[] { eventName }
            );

            await ScheduleJob(cancellationToken);
        }

        protected virtual async Task ScheduleJob(CancellationToken cancellationToken)
        {
            var nexOcurrencet = _expression.GetNextOccurrence(DateTimeOffset.Now, _timeZoneInfo);
            if (nexOcurrencet.HasValue)
            {
                var delay = nexOcurrencet.Value - DateTimeOffset.Now;
                if (delay.TotalMilliseconds <= 0)   // prevent non-positive values from being passed into Timer
                {
                    await ScheduleJob(cancellationToken);
                }
                _timer = new Timer(delay.TotalMilliseconds);
                _timer.Elapsed += async (sender, args) =>
                {
                    _timer.Dispose();  // reset and dispose timer
                    _timer = null;

                    if (!cancellationToken.IsCancellationRequested)
                    {
                        await DoWork(cancellationToken);
                    }

                    if (!cancellationToken.IsCancellationRequested)
                    {
                        await ScheduleJob(cancellationToken);    // reschedule next
                    }
                };
                _timer.Start();
            }
            await Task.CompletedTask;
        }

        public abstract Task DoWork(CancellationToken cancellationToken);

        public virtual Task StopAsync(CancellationToken cancellationToken)
        {
            var eventName = $"{typeof(T).Name} stopped";
            _logger.LogInformation(
                eventId: new EventId(
                    id: (int)Event.CronJob,
                    name: eventName
                ),
                message: $"{AnsiUtility.EncodeEventName("{0}")}",
                args: new object[] { eventName }
            );

            _timer?.Stop();

            return Task.CompletedTask;
        }

        public virtual void Dispose()
        {
            _timer?.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion
    }

}

