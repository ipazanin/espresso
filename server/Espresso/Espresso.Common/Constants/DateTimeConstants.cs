using System;

namespace Espresso.Common.Constants
{
    public static class DateTimeConstants
    {
        #region Points in Time
        public static DateTime UnixEpochStartTime => new(
            year: 1970,
            month: 1,
            day: 1,
            hour: 0,
            minute: 0,
            second: 0,
            kind: DateTimeKind.Utc
        );
        #endregion

        #region Formats
        public const string LoggerDateTimeFormat = "HH:mm:ss.ff";
        public const string MobileAppDateTimeFormat = "yyyy-MM-dd'T'HH:mm:ss";
        public const string PushNotificationInternalNameFormat = "yyyy-MM-dd'T'HH:mm:ss";
        #endregion

        #region Timespans
        public const int OneYearInSeconds = 60 * 60 * 24 * 365;
        #endregion
    }
}
