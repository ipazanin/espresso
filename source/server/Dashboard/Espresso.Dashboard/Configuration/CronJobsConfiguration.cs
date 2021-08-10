// CronJobsConfiguration.cs
//
// © 2021 Espresso News. All rights reserved.

using Microsoft.Extensions.Configuration;

namespace Espresso.Dashboard.Configuration
{
    public class CronJobsConfiguration
    {
        private readonly IConfigurationSection _configuration;

        public string ParseArticlesCronExpression =>
            _configuration.GetValue<string>("ParseArticlesCronExpression");

        /// <summary>
        /// Initializes a new instance of the <see cref="CronJobsConfiguration"/> class.
        /// </summary>
        /// <param name="configuration"></param>
        public CronJobsConfiguration(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }
    }
}
