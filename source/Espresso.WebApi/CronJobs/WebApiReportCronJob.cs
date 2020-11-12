using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.Infrastructure.CronJobsInfrastructure;
using Espresso.Application.IServices;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using Espresso.Domain.Utilities;
using Espresso.WebApi.Configuration;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Espresso.WebApi.CronJobs
{
    /// <summary>
    /// 
    /// </summary>
    public class WebApiReportCronJob : CronJob<WebApiReportCronJob>
    {
        #region Fields
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IWebApiConfiguration _webApiConfiguration;
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceScopeFactory"></param>
        /// <param name="cronJobConfiguration"></param>
        /// <param name="webApiConfiguration"></param>
        /// <returns></returns>
        public WebApiReportCronJob(
            IServiceScopeFactory serviceScopeFactory,
            ICronJobConfiguration<WebApiReportCronJob> cronJobConfiguration,
            IWebApiConfiguration webApiConfiguration
        ) : base(
            cronJobConfiguration: cronJobConfiguration,
            serviceScopeFactory: serviceScopeFactory
        )
        {
            _serviceScopeFactory = serviceScopeFactory;
            _webApiConfiguration = webApiConfiguration;
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await base.StartAsync(cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        public override async Task DoWork(CancellationToken stoppingToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var slackService = scope.ServiceProvider.GetRequiredService<ISlackService>();
            var memoryCache = scope.ServiceProvider.GetRequiredService<IMemoryCache>();

            var data = new List<(string name, int count, TimeSpan duration)>();

            var articles = memoryCache.Get<IEnumerable<Article>>(MemoryCacheConstants.ArticleKey);
            var newsPortals = memoryCache.Get<IEnumerable<NewsPortal>>(MemoryCacheConstants.NewsPortalKey);
            var topArticles = GetTopArticles(articles);
            var totalNumberOfClicks = GetTotalClicksForYesterday(articles);
            var topNewsPortals = GetTop10NewsPortalsByNumberOfClicks(
                articles,
                newsPortals
            );
            var categoriesWithNumberOfClicks = GetCategoriesWithNumberOfClicks(articles);

            await slackService.LogYesterdaysStatistics(
                topArticles: topArticles,
                totalNumberOfClicks: totalNumberOfClicks,
                topNewsPortals: topNewsPortals,
                categoriesWithNumberOfClicks: categoriesWithNumberOfClicks,
                appEnvironment: _webApiConfiguration.AppConfiguration.AppEnvironment,
                cancellationToken: stoppingToken
            );
        }

        private static IEnumerable<Article> GetTopArticles(
            IEnumerable<Article> articles
        )
        {
            var topArticles = articles
                .Where(article => article.PublishDateTime.Date == DateTimeUtility.YesterdaysDate)
                .OrderByDescending(article => article.NumberOfClicks)
                .ThenByDescending(article => article.PublishDateTime)
                .Take(5);

            return topArticles;
        }

        private static int GetTotalClicksForYesterday(
            IEnumerable<Article> articles
        )
        {
            var totalClicks = articles
                .Where(article => article.PublishDateTime.Date == DateTimeUtility.YesterdaysDate)
                .Sum(article => article.NumberOfClicks);

            return totalClicks;
        }

        private static IEnumerable<(NewsPortal newsPortal, int numberOfClicks, IEnumerable<Article> articles)> GetTop10NewsPortalsByNumberOfClicks(
            IEnumerable<Article> articles,
            IEnumerable<NewsPortal> newsPortals
        )
        {
            var topNewsPortals = articles
                .Where(article => article.PublishDateTime.Date == DateTimeUtility.YesterdaysDate)
                .GroupBy(article => article.NewsPortal!.Id)
                .Select(articlesGroupedByNewsPortal =>
                    (
                        newsPortal: newsPortals.FirstOrDefault(newsPortal => newsPortal.Id == articlesGroupedByNewsPortal.Key),
                        numberOfClicks: articlesGroupedByNewsPortal.Sum(article => article.NumberOfClicks),
                        articles = articlesGroupedByNewsPortal.ToList().AsEnumerable()
                    )
                )
                .Where(articleClicksGroupedByNewsPortal => articleClicksGroupedByNewsPortal.newsPortal is not null)
                .OrderByDescending(articleClicksGroupedByNewsPortal => articleClicksGroupedByNewsPortal.numberOfClicks)
                .Take(10);

            return topNewsPortals!;
        }

        private static IEnumerable<(Category category, int numberOfClicks, IEnumerable<Article> articles)> GetCategoriesWithNumberOfClicks(
            IEnumerable<Article> articles
        )
        {
            var categoriesOrderedByNumberOfClicks = articles
                .Where(article => article.PublishDateTime.Date == DateTimeUtility.YesterdaysDate)
                .GroupBy(article => article.ArticleCategories.First().Category!.Id)
                .Select(articlesGroupedByCategory =>
                    (
                        category: articlesGroupedByCategory.FirstOrDefault()?.ArticleCategories.FirstOrDefault()?.Category,
                        numberOfClicks: articlesGroupedByCategory.Sum(article => article.NumberOfClicks),
                        articles = articlesGroupedByCategory.ToList().AsEnumerable()
                    )
                )
                .Where(articlesGroupedByCategory => articlesGroupedByCategory.category is not null)
                .OrderByDescending(articleClicksGroupedByCategory => articleClicksGroupedByCategory.numberOfClicks)
                .Take(10);

            return categoriesOrderedByNumberOfClicks!;
        }
        #endregion
    }
}
