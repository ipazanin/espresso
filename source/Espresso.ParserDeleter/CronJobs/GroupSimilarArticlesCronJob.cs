using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.Infrastructure.CronJobsInfrastructure;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using Espresso.ParserDeleter.Application.GroupSimilarArticles;
using Espresso.ParserDeleter.Configuration;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Espresso.ParserDeleter.CronJobs
{
    /// <summary>
    /// 
    /// </summary>
    public class GroupSimilarArticlesCronJob : CronJob<GroupSimilarArticlesCronJob>
    {
        #region Fields
        private readonly IParserDeleterConfiguration _parserDeleterConfiguration;
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cronJobConfiguration"></param>
        /// <param name="serviceScopeFactory"></param>
        /// <param name="parserDeleterConfiguration"></param>
        /// <returns></returns>
        public GroupSimilarArticlesCronJob(
            ICronJobConfiguration<GroupSimilarArticlesCronJob> cronJobConfiguration,
            IServiceScopeFactory serviceScopeFactory,
            IParserDeleterConfiguration parserDeleterConfiguration
        ) : base(
            cronJobConfiguration,
            serviceScopeFactory
        )
        {
            _parserDeleterConfiguration = parserDeleterConfiguration;
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task DoWork(CancellationToken cancellationToken)
        {
            using var scope = ServiceScopeFactory.CreateScope();
            var sender = scope.ServiceProvider.GetRequiredService<ISender>();

            return sender.Send(
                request: new GroupSimilarArticlesCommand
                {
                    ParserApiKey = _parserDeleterConfiguration.ApiKeysConfiguration.ParserApiKey,
                    ServerUrl = _parserDeleterConfiguration.AppConfiguration.ServerUrl,
                    AppEnvironment = _parserDeleterConfiguration.AppConfiguration.AppEnvironment,
                    TargetedApiVersion = _parserDeleterConfiguration.AppConfiguration.RssFeedParserMajorMinorVersion,
                    ConsumerVersion = _parserDeleterConfiguration.AppConfiguration.Version,
                    CurrentApiVersion = _parserDeleterConfiguration.AppConfiguration.Version,
                    DeviceType = DeviceType.RssFeedParser
                },
                cancellationToken: cancellationToken
            );
        }
        #endregion
    }
}