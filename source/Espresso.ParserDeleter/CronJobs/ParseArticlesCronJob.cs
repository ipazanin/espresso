using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.IServices;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using Espresso.ParserDeleter.Configuration;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Espresso.ParserDeleter.ParseRssFeeds;
using Espresso.Application.Infrastructure.CronJobsInfrastructure;

namespace Espresso.ParserDeleter.CronJobs
{
    public class ParseArticlesCronJob : CronJob<ParseArticlesCronJob>
    {
        #region Fields
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IParserDeleterConfiguration _configuration;
        #endregion

        #region Constructors
        public ParseArticlesCronJob(
            ICronJobConfiguration<ParseArticlesCronJob> cronJobConfiguration,
            IServiceScopeFactory serviceScopeFactory,
            IParserDeleterConfiguration configuration
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
