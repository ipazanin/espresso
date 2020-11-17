namespace Espresso.WebApi.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public interface IWebApiConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public AppConfiguration AppConfiguration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public DatabaseConfiguration DatabaseConfiguration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public ApiKeysConfiguration ApiKeysConfiguration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public SpaConfiguration SpaConfiguration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public DateTimeConfiguration DateTimeConfiguration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public TrendingScoreConfiguration TrendingScoreConfiguration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public CronJobsConfiguration CronJobsConfiguration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public RabbitMqConfiguration RabbitMqConfiguration { get; }
    }
}
