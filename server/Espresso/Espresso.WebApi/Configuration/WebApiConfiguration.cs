using Microsoft.Extensions.Configuration;

namespace Espresso.WebApi.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class WebApiConfiguration : IWebApiConfiguration
    {
        #region Properties
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
        public CronJobsConfiguration CronJobsConfiguration { get; }


        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public TrendingScoreConfiguration TrendingScoreConfiguration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public RabbitMqConfiguration RabbitMqConfiguration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public HttpClientConfiguration SlackHttpClientConfiguration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public SystemTextJsonSerializerConfiguration SystemTextJsonSerializerConfiguration { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public WebApiConfiguration(IConfiguration configuration)
        {
            AppConfiguration = new AppConfiguration(configuration.GetSection("AppConfiguration"));
            DatabaseConfiguration = new DatabaseConfiguration(configuration.GetSection("DatabaseConfiguration"));
            ApiKeysConfiguration = new ApiKeysConfiguration(configuration.GetSection("ApiKeysConfiguration"));
            SpaConfiguration = new SpaConfiguration(configuration.GetSection("SpaConfiguration"));
            DateTimeConfiguration = new DateTimeConfiguration(configuration.GetSection("DateTimeConfiguration"));
            CronJobsConfiguration = new CronJobsConfiguration(configuration.GetSection("CronJobsConfiguration"));
            TrendingScoreConfiguration = new TrendingScoreConfiguration(configuration.GetSection("TrendingScoreConfiguration"));
            RabbitMqConfiguration = new RabbitMqConfiguration(configuration.GetSection("RabbitMqConfiguration"));
            SlackHttpClientConfiguration = new HttpClientConfiguration(configuration.GetSection("HttpClientConfiguration:DefaultHttpClientConfiguration"));
            SystemTextJsonSerializerConfiguration = new SystemTextJsonSerializerConfiguration(configuration.GetSection("SystemTextJsonSerializerConfiguration"));
        }
        #endregion
    }
}
