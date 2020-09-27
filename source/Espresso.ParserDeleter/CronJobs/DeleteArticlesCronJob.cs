using System;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.Infrastructure.CronJobsInfrastructure;
using Espresso.Application.IServices;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.Common.Utilities;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using Espresso.Domain.Extensions;
using Espresso.ParserDeleter.Application.DeleteOldArticles;
using Espresso.ParserDeleter.Configuration;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Espresso.ParserDeleter.CronJobs
{
    public class DeleteArticlesCronJob : CronJob<DeleteArticlesCronJob>
    {
        #region Fields
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IParserDeleterConfiguration _configuration;
        private readonly ILogger<DeleteArticlesCronJob> _logger;
        #endregion

        #region Constructors
        public DeleteArticlesCronJob(
            IServiceScopeFactory serviceScopeFactory,
            IParserDeleterConfiguration parserDeleterConfiguration,
            ILoggerFactory loggerFactory,
            ICronJobConfiguration<DeleteArticlesCronJob> cronJobConfiguration
        ) : base(
            cronJobConfiguration: cronJobConfiguration,
            loggerFactory: loggerFactory,
            serviceScopeFactory: serviceScopeFactory
        )
        {
            _serviceScopeFactory = serviceScopeFactory;
            _configuration = parserDeleterConfiguration;
            _logger = loggerFactory.CreateLogger<DeleteArticlesCronJob>();
        }
        #endregion

        #region Methods
        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            // await Task.Delay(
            //     delay: TimeSpan.FromSeconds(60),
            //     cancellationToken: cancellationToken
            // );

            await base.StartAsync(cancellationToken);
        }

        public override async Task DoWork(CancellationToken stoppingToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var slackService = scope.ServiceProvider.GetRequiredService<ISlackService>();

            var cancellationToken = GetCancellationToken();

            try
            {

                await mediator.Send(
                    request: new DeleteOldArticlesCommand(
                        maxAgeOfOldArticles: _configuration.AppConfiguration.MaxAgeOfArticles,
                        currentEspressoWebApiVersion: _configuration.AppConfiguration.Version,
                        targetedEspressoWebApiVersion: _configuration.AppConfiguration.RssFeedParserMajorMinorVersion,
                        consumerVersion: _configuration.AppConfiguration.Version,
                        deviceType: DeviceType.RssFeedParser,
                        appEnvironment: _configuration.AppConfiguration.AppEnvironment
                    ),
                    cancellationToken: cancellationToken
                );
            }
            catch (Exception exception)
            {
                var eventName = Event.DeleteArticlesJob.GetDisplayName();
                var eventId = (int)Event.DeleteArticlesJob;
                var version = _configuration.AppConfiguration.Version;
                var exceptionMessage = exception.Message;
                var innerExceptionMessage = exception.InnerException?.Message ?? FormatConstants.EmptyValue;

                _logger.LogError(
                    eventId: new EventId(
                        id: eventId,
                        name: eventName
                    ),
                    exception: exception,
                    message: $"{AnsiUtility.EncodeEventName("{0}")}\n\t" +
                        $"{AnsiUtility.EncodeParameterName(nameof(version))}: " +
                        $"{AnsiUtility.EncodeVersion("{1}")}\n\t" +
                        $"{AnsiUtility.EncodeParameterName(nameof(exceptionMessage))}: " +
                        $"{AnsiUtility.EncodeErrorMessage("{2}")}\n\t" +
                        $"{AnsiUtility.EncodeParameterName(nameof(innerExceptionMessage))}: " +
                        $"{AnsiUtility.EncodeErrorMessage("{3}")}",
                    args: new object[]
                    {
                            eventName,
                            version,
                            exceptionMessage,
                            innerExceptionMessage,
                    }
                );

                await slackService.LogError(
                        eventName: eventName,
                        version: _configuration.AppConfiguration.Version,
                        message: exception.Message,
                        exception: exception,
                        appEnvironment: _configuration.AppConfiguration.AppEnvironment,
                        cancellationToken: default
                );
            }
        }

        private CancellationToken GetCancellationToken()
        {
            var cancellationTokeSource = new CancellationTokenSource(
                delay: _configuration.CronJobsConfiguration.DeleteArticlesCancellation
            );
            return cancellationTokeSource.Token;
        }
        #endregion
    }
}
