// WebApiReportCronJob.cs
//
// © 2021 Espresso News. All rights reserved.

using Cronos;
using Espresso.Application.Infrastructure.CronJobsInfrastructure;
using Espresso.Application.Services.Contracts;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using Espresso.Domain.Infrastructure;
using Espresso.Domain.Utilities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Espresso.WebApi.Jobs.CronJobs
{
    /// <summary>
    ///
    /// </summary>
    public class WebApiReportCronJob : CronJob<WebApiReportCronJob>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ISettingProvider _settingProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebApiReportCronJob"/> class.
        /// </summary>
        /// <param name="serviceScopeFactory"></param>
        /// <param name="cronJobConfiguration"></param>
        /// <param name="settingProvider"></param>
        public WebApiReportCronJob(
            IServiceScopeFactory serviceScopeFactory,
            ICronJobConfiguration<WebApiReportCronJob> cronJobConfiguration,
            ISettingProvider settingProvider)
            : base(
            cronJobConfiguration: cronJobConfiguration,
            serviceScopeFactory: serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _settingProvider = settingProvider;
        }

        protected override CronExpression CronExpression => CronExpression.Parse(_settingProvider.LatestSetting.JobsSetting.WebApiReportCronExpression);

        /// <summary>
        ///
        /// </summary>
        /// <param name="cancellationToken"></param>
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            return base.StartAsync(cancellationToken);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cancellationToken"></param>
        public override async Task DoWork(CancellationToken cancellationToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var slackService = scope.ServiceProvider.GetRequiredService<ISlackService>();
            var memoryCache = scope.ServiceProvider.GetRequiredService<IMemoryCache>();

            var articles = memoryCache.Get<IEnumerable<Article>>(MemoryCacheConstants.ArticleKey);
            var newsPortals = memoryCache.Get<IEnumerable<NewsPortal>>(MemoryCacheConstants.NewsPortalKey);
            var topArticles = GetTopArticles(articles);
            var totalNumberOfClicks = GetTotalClicksForYesterday(articles);
            var topNewsPortals = GetTop10NewsPortalsByNumberOfClicks(
                articles,
                newsPortals);
            var categoriesWithNumberOfClicks = GetCategoriesWithNumberOfClicks(articles);

            await slackService.LogYesterdaysStatistics(
                topArticles: topArticles,
                totalNumberOfClicks: totalNumberOfClicks,
                topNewsPortals: topNewsPortals,
                categoriesWithNumberOfClicks: categoriesWithNumberOfClicks,
                cancellationToken: cancellationToken);
        }

        private static IEnumerable<Article> GetTopArticles(
            IEnumerable<Article> articles)
        {
            var topArticles = articles
                .Where(article => article.PublishDateTime.Date == DateTimeUtility.YesterdaysDate)
                .OrderByDescending(article => article.NumberOfClicks)
                .ThenByDescending(article => article.PublishDateTime)
                .Take(5);

            return topArticles;
        }

        private static int GetTotalClicksForYesterday(
            IEnumerable<Article> articles)
        {
            var totalClicks = articles
                .Where(article => article.PublishDateTime.Date == DateTimeUtility.YesterdaysDate)
                .Sum(article => article.NumberOfClicks);

            return totalClicks;
        }

        private static IEnumerable<(NewsPortal newsPortal, int numberOfClicks, IEnumerable<Article> articles)> GetTop10NewsPortalsByNumberOfClicks(
            IEnumerable<Article> articles,
            IEnumerable<NewsPortal> newsPortals)
        {
            var topNewsPortals = articles
                .Where(article => article.PublishDateTime.Date == DateTimeUtility.YesterdaysDate)
                .GroupBy(article => article.NewsPortal!.Id)
                .Select(articlesGroupedByNewsPortal =>
                    (
                        newsPortal: newsPortals.FirstOrDefault(newsPortal => newsPortal.Id == articlesGroupedByNewsPortal.Key),
                        numberOfClicks: articlesGroupedByNewsPortal.Sum(article => article.NumberOfClicks),
                        articles: articlesGroupedByNewsPortal.ToList().AsEnumerable()))
                .Where(articleClicksGroupedByNewsPortal => articleClicksGroupedByNewsPortal.newsPortal is not null)
                .OrderByDescending(articleClicksGroupedByNewsPortal => articleClicksGroupedByNewsPortal.numberOfClicks)
                .Take(10);

            return topNewsPortals!;
        }

        private static IEnumerable<(Category category, int numberOfClicks, IEnumerable<Article> articles)> GetCategoriesWithNumberOfClicks(
            IEnumerable<Article> articles)
        {
            var categoriesOrderedByNumberOfClicks = articles
                .Where(article => article.PublishDateTime.Date == DateTimeUtility.YesterdaysDate)
                .GroupBy(article => article.ArticleCategories.First().Category!.Id)
                .Select(articlesGroupedByCategory =>
                    (
                        category: articlesGroupedByCategory.FirstOrDefault()?.ArticleCategories.FirstOrDefault()?.Category,
                        numberOfClicks: articlesGroupedByCategory.Sum(article => article.NumberOfClicks),
                        articles: articlesGroupedByCategory.ToList().AsEnumerable()))
                .Where(articlesGroupedByCategory => articlesGroupedByCategory.category is not null)
                .OrderByDescending(articleClicksGroupedByCategory => articleClicksGroupedByCategory.numberOfClicks)
                .Take(10);

            return categoriesOrderedByNumberOfClicks!;
        }
    }
}
