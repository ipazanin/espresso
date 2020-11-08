using System;
using Microsoft.Extensions.Configuration;

namespace Espresso.ParserDeleter.Configuration
{
    public class CronJobsConfiguration
    {
        #region Fields
        private readonly IConfigurationSection _configuration;
        #endregion

        #region Properties
        public string DeleteArticlesCronExpression =>
            _configuration.GetValue<string>("DeleteArticlesCronExpression");

        public string ParserDeleterReportCronExpression =>
            _configuration.GetValue<string>("ParserDeleterReportCronExpression");

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
