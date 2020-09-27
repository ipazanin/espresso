using System;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.IServices;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.Common.Utilities;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using Espresso.Domain.Extensions;
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
            ILoggerFactory loggerFactory
        ) : base(
            cronJobConfiguration: cronJobConfiguration,
            loggerFactory: loggerFactory,
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
                request: new ParseRssFeedsCommand(
                    maxAgeOfArticle: _configuration.AppConfiguration.MaxAgeOfArticles,
                    parserApiKey: _configuration.ApiKeysConfiguration.ParserApiKey,
                    serverUrl: _configuration.AppConfiguration.ServerUrl,
                    currentEspressoWebApiVersion: _configuration.AppConfiguration.Version,
                    targetedEspressoWebApiVersion: _configuration.AppConfiguration.RssFeedParserMajorMinorVersion,
                    consumerVersion: _configuration.AppConfiguration.Version,
                    deviceType: DeviceType.RssFeedParser,
                    appEnvironment: _configuration.AppConfiguration.AppEnvironment
                ),
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
