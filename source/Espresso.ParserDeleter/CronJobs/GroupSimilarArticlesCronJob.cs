using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.Infrastructure.CronJobsInfrastructure;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using Espresso.Domain.IServices;
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
        private readonly ISender _sender;
        private readonly IParserDeleterConfiguration _parserDeleterConfiguration;
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cronJobConfiguration"></param>
        /// <param name="serviceScopeFactory"></param>
        /// <param name="loggerService"></param>
        /// <param name="sender"></param>
        /// <param name="parserDeleterConfiguration"></param>
        /// <returns></returns>
        public GroupSimilarArticlesCronJob(
            ICronJobConfiguration<GroupSimilarArticlesCronJob> cronJobConfiguration,
            IServiceScopeFactory serviceScopeFactory,
            ILoggerService<CronJob<GroupSimilarArticlesCronJob>> loggerService,
            ISender sender,
            IParserDeleterConfiguration parserDeleterConfiguration
        ) : base(
            cronJobConfiguration,
            serviceScopeFactory,
            loggerService
        )
        {
            _sender = sender;
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
            return _sender.Send(
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