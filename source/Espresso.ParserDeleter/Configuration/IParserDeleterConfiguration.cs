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
    }
}
