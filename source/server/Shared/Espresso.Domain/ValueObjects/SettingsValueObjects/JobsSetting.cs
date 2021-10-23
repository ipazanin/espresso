// JobsSetting.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Infrastructure;
using System.Collections.Generic;

namespace Espresso.Domain.ValueObjects.SettingsValueObjects
{
    /// <summary>
    /// Cron jobs setting.
    /// </summary>
    public class JobsSetting : ValueObject
    {
#pragma warning disable RCS1170 // Use read-only auto-implemented property.

        /// <summary>
        /// Gets analytics cron expression.
        /// </summary>
        public string AnalyticsCronExpression { get; private set; }

        /// <summary>
        /// Gets web api report cron expression.
        /// </summary>
        public string WebApiReportCronExpression { get; private set; }

        /// <summary>
        /// Gets article parsing cron expression.
        /// </summary>
        public string ParseArticlesCronExpression { get; private set; }

#pragma warning restore RCS1170 // Use read-only auto-implemented property.

        /// <summary>
        /// Initializes a new instance of the <see cref="JobsSetting"/> class.
        /// </summary>
        /// <param name="analyticsCronExpression">google analytics cron expression.</param>
        /// <param name="webApiReportCronExpression">web api report cron expression.</param>
        /// <param name="parseArticlesCronExpression">article parsing cron expression.</param>
        public JobsSetting(
            string analyticsCronExpression,
            string webApiReportCronExpression,
            string parseArticlesCronExpression)
        {
            AnalyticsCronExpression = analyticsCronExpression;
            WebApiReportCronExpression = webApiReportCronExpression;
            ParseArticlesCronExpression = parseArticlesCronExpression;
        }

        /// <inheritdoc/>
        protected override IEnumerable<object?> GetAtomicValues()
        {
            yield return AnalyticsCronExpression;
            yield return WebApiReportCronExpression;
            yield return ParseArticlesCronExpression;
        }
    }
}
