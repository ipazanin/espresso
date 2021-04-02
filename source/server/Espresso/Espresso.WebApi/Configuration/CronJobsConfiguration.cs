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
        public string AnalyticsCronExpression =>
            _configuration.GetValue<string>(nameof(AnalyticsCronExpression));

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string WebApiReportCronExpression =>
            _configuration.GetValue<string>(nameof(WebApiReportCronExpression));
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
