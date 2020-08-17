using System;

namespace Espresso.Common.Constants
{
    public static class DateTimeConstants
    {
        #region Points in Time
        public static DateTime UnixEpochStartTime => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        #endregion

        #region Formats
        public const string LoggerDateTimeFormat = "HH:mm:ss.ff";
        public const string MobileAppDateTimeFormat = "yyyy-MM-dd'T'HH:mm:ss";
        #endregion

        #region Timespans
        public static TimeSpan MaxAgeOfArticle => TimeSpan.FromDays(7);
        public static TimeSpan MaxAgeOfTrendingArticle => TimeSpan.FromDays(2);
        public static TimeSpan MaxAgeOfNewNewsPortal => TimeSpan.FromDays(30);
        public const int OneYearInSeconds = 60 * 60 * 24 * 365;
        #endregion
    }
}
