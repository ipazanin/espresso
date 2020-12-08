namespace Espresso.ParserDeleter.Configuration
{
    public interface IParserDeleterConfiguration
    {
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
    }
}
