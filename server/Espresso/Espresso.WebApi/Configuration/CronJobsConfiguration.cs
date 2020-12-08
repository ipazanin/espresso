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
        public string WebApiReportCronExpression =>
            _configuration.GetValue<string>("WebApiReportCronExpression");
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
