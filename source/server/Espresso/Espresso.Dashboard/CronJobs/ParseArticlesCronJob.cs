using System.Threading;
using System.Threading.Tasks;
using Espresso.Common.Enums;
using Espresso.Dashboard.Configuration;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Espresso.Dashboard.ParseRssFeeds;
using Espresso.Application.Infrastructure.CronJobsInfrastructure;
using Espresso.Application.Services.Contracts;

namespace Espresso.Dashboard.CronJobs
{
    public class ParseArticlesCronJob : CronJob<ParseArticlesCronJob>
    {
        #region Fields
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IDashboardConfiguration _configuration;
        #endregion

        #region Constructors
        public ParseArticlesCronJob(
            ICronJobConfiguration<ParseArticlesCronJob> cronJobConfiguration,
            IServiceScopeFactory serviceScopeFactory,
            IDashboardConfiguration configuration
        ) : base(
            cronJobConfiguration: cronJobConfiguration,
            serviceScopeFactory: serviceScopeFactory
        )
        {
            _serviceScopeFactory = serviceScopeFactory;
            _configuration = configuration;
        }
        #endregion

        #region Methods
        public override async Task DoWork(CancellationToken cancellationToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var slackService = scope.ServiceProvider.GetRequiredService<ISlackService>();

            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            var parseRssFeedsCommandResponse = await mediator.Send(
                request: new ParseRssFeedsCommand
                {
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
