using Microsoft.Extensions.Configuration;

namespace Espresso.WebApi.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class CronJobsConfiguration
    {
        #region Fields
        private readonly IConfigurationSection _configuration;
        #endregion


        #region Properties
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string ApplicationDownloadStatisticsCronExpression =>
            _configuration.GetValue<string>("ApplicationDownloadStatisticsCronExpression");

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string WebApiPerformanceCronExpression =>
            _configuration.GetValue<string>("WebApiPerformanceCronExpression");
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public CronJobsConfiguration(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }
        #endregion
    }
}
