// CronJobsConfiguration.cs
//
// © 2021 Espresso News. All rights reserved.

using Microsoft.Extensions.Configuration;

namespace Espresso.WebApi.Configuration
{
    /// <summary>
    ///
    /// </summary>
    public class CronJobsConfiguration
    {
        private readonly IConfigurationSection _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="CronJobsConfiguration"/> class.
        /// </summary>
        /// <param name="configuration"></param>
        public CronJobsConfiguration(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }

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
    }
}
