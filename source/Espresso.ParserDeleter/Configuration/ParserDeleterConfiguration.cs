using Microsoft.Extensions.Configuration;

namespace Espresso.ParserDeleter.Configuration
{
    public class ParserDeleterConfiguration : IParserDeleterConfiguration
    {
        #region Properties
        public ApiKeysConfiguration ApiKeysConfiguration { get; }
        public AppConfiguration AppConfiguration { get; }
        public DatabaseConfiguration DatabaseConfiguration { get; }
        public CronJobsConfiguration CronJobsConfiguration { get; }
        #endregion

        #region Constructors
        public ParserDeleterConfiguration(IConfiguration configuration)
        {
            ApiKeysConfiguration = new ApiKeysConfiguration(configuration.GetSection("ApiKeysConfiguration"));
            AppConfiguration = new AppConfiguration(configuration.GetSection("AppConfiguration"));
            DatabaseConfiguration = new DatabaseConfiguration(configuration.GetSection("DatabaseConfiguration"));
            CronJobsConfiguration = new CronJobsConfiguration(configuration.GetSection("CronJobsConfiguration"));
        }
        #endregion
    }
}
