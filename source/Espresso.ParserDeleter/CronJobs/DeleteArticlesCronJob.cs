using System;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.Infrastructure.CronJobsInfrastructure;
using Espresso.Application.IServices;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.Common.Utilities;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using Espresso.Common.Extensions;
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
        public override async Task DoWork(CancellationToken cancellationToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var slackService = scope.ServiceProvider.GetRequiredService<ISlackService>();

            await mediator.Send(
                request: new DeleteOldArticlesCommand
                {
                    MaxAgeOfOldArticles = _configuration.AppConfiguration.MaxAgeOfArticles,
                    TargetedApiVersion = _configuration.AppConfiguration.RssFeedParserMajorMinorVersion,
                    ConsumerVersion = _configuration.AppConfiguration.Version,
                    DeviceType = DeviceType.RssFeedParser,
                },
                cancellationToken: cancellationToken
            );
        }
        #endregion
    }
}
