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
using Espresso.Domain.IServices;
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
        #endregion

        #region Constructors
        public DeleteArticlesCronJob(
            IServiceScopeFactory serviceScopeFactory,
            IParserDeleterConfiguration parserDeleterConfiguration,
            ICronJobConfiguration<DeleteArticlesCronJob> cronJobConfiguration
        ) : base(
            cronJobConfiguration: cronJobConfiguration,
            serviceScopeFactory: serviceScopeFactory
        )
        {
            _serviceScopeFactory = serviceScopeFactory;
            _configuration = parserDeleterConfiguration;
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

            await mediator.Send(
                request: new DeleteOldArticlesCommand
                {
                    MaxAgeOfOldArticles = _configuration.AppConfiguration.MaxAgeOfArticles,
                    CurrentApiVersion = _configuration.AppConfiguration.Version,
                    TargetedApiVersion = _configuration.AppConfiguration.RssFeedParserMajorMinorVersion,
                    ConsumerVersion = _configuration.AppConfiguration.Version,
                    DeviceType = DeviceType.RssFeedParser,
                    AppEnvironment = _configuration.AppConfiguration.AppEnvironment
                },
                cancellationToken: cancellationToken
            );
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
