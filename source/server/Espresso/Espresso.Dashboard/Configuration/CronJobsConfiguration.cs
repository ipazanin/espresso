using Microsoft.Extensions.Configuration;

namespace Espresso.Dashboard.Configuration
{
    public class CronJobsConfiguration
    {
        #region Fields
        private readonly IConfigurationSection _configuration;
        #endregion

        #region Properties

        public string ParseArticlesCronExpression =>
            _configuration.GetValue<string>("ParseArticlesCronExpression");

        #endregion

        #region Constructors
        public CronJobsConfiguration(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }
        #endregion
    }
}
