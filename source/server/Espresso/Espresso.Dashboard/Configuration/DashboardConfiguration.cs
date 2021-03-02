using Microsoft.Extensions.Configuration;

namespace Espresso.Dashboard.Configuration
{
    public class DashboardConfiguration : IDashboardConfiguration
    {
        #region Properties

        public ApiKeysConfiguration ApiKeysConfiguration { get; }
        public AppConfiguration AppConfiguration { get; }
        public DatabaseConfiguration DatabaseConfiguration { get; }
        public CronJobsConfiguration CronJobsConfiguration { get; }
        public ArticleSimilarityConfiguration ArticleSimilarityConfiguration { get; }
        public RabbitMqConfiguration RabbitMqConfiguration { get; }
        public HttpClientConfiguration SlackHttpClientConfiguration { get; }
        public HttpClientConfiguration SendArticlesHttpClientConfiguration { get; }
        public HttpClientConfiguration LoadRssFeedsHttpClientConfiguration { get; }
        public HttpClientConfiguration ScrapeWebHttpClientConfiguration { get; }
        public SystemTextJsonSerializerConfiguration SystemTextJsonSerializerConfiguration { get; }

        #endregion

        #region Constructors

        public DashboardConfiguration(IConfiguration configuration)
        {
            ApiKeysConfiguration = new ApiKeysConfiguration(configuration.GetSection("ApiKeysConfiguration"));
            AppConfiguration = new AppConfiguration(configuration.GetSection("AppConfiguration"));
            DatabaseConfiguration = new DatabaseConfiguration(configuration.GetSection("DatabaseConfiguration"));
            CronJobsConfiguration = new CronJobsConfiguration(configuration.GetSection("CronJobsConfiguration"));
            ArticleSimilarityConfiguration = new ArticleSimilarityConfiguration(configuration.GetSection("ArticleSimilarityConfiguration"));
            RabbitMqConfiguration = new RabbitMqConfiguration(configuration.GetSection("RabbitMqConfiguration"));
            SlackHttpClientConfiguration = new HttpClientConfiguration(configuration.GetSection("HttpClientConfiguration:SlackHttpClientConfiguration"));
            SendArticlesHttpClientConfiguration = new HttpClientConfiguration(configuration.GetSection("HttpClientConfiguration:SendArticlesHttpClientConfiguration"));
            LoadRssFeedsHttpClientConfiguration = new HttpClientConfiguration(configuration.GetSection("HttpClientConfiguration:LoadRssFeedsHttpClientConfiguration"));
            ScrapeWebHttpClientConfiguration = new HttpClientConfiguration(configuration.GetSection("HttpClientConfiguration:ScrapeWebHttpClientConfiguration"));
            SystemTextJsonSerializerConfiguration = new SystemTextJsonSerializerConfiguration(configuration.GetSection("SystemTextJsonSerializerConfiguration"));
        }

        #endregion
    }
}
