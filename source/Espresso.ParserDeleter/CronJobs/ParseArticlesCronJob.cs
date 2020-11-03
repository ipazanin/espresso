using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.IServices;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using Espresso.ParserDeleter.Configuration;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
            IParserDeleterConfiguration configuration,
            ILoggerService<CronJob<ParseArticlesCronJob>> loggerService
        ) : base(
            cronJobConfiguration: cronJobConfiguration,
            loggerService: loggerService,
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

            var parseArticlesCancellationToken = GetParseArticlesCancellationToken();

            var parseRssFeedsCommandResponse = await mediator.Send(
                request: new ParseRssFeedsCommand
                {
                    MaxAgeOfArticle = _configuration.AppConfiguration.MaxAgeOfArticles,
                    ParserApiKey = _configuration.ApiKeysConfiguration.ParserApiKey,
                    ServerUrl = _configuration.AppConfiguration.ServerUrl,
                    CurrentApiVersion = _configuration.AppConfiguration.Version,
                    TargetedApiVersion = _configuration.AppConfiguration.RssFeedParserMajorMinorVersion,
                    ConsumerVersion = _configuration.AppConfiguration.Version,
                    DeviceType = DeviceType.RssFeedParser,
                    AppEnvironment = _configuration.AppConfiguration.AppEnvironment
                },
                cancellationToken: parseArticlesCancellationToken
            );
        }

        private CancellationToken GetParseArticlesCancellationToken()
        {
            var cancellationTokeSource = new CancellationTokenSource(
                delay: _configuration.CronJobsConfiguration.ParseArticlesCancellation
            );
            return cancellationTokeSource.Token;
        }
        #endregion
    }
}
