// DateTimeConstants.cs
//
// © 2022 Espresso News. All rights reserved.

namespace Espresso.Common.Constants;

/// <summary>
/// <see cref="DateTime"/> constants.
/// </summary>
public static class DateTimeConstants
{
    /// <summary>
    /// Logger <see cref="DateTime"/> format.
    /// </summary>
    public const string LoggerDateTimeFormat = "HH:mm:ss.ff";

    /// <summary>
    /// Mobile app <see cref="DateTime"/> format.
    /// </summary>
    public const string MobileAppDateTimeFormat = "yyyy-MM-dd'T'HH:mm:ss";

    /// <summary>
    /// Push notification internal name <see cref="DateTime"/> format.
    /// </summary>
    public const string PushNotificationInternalNameFormat = "yyyy-MM-dd'T'HH:mm:ss";

    /// <summary>
    /// One year in seconds.
    /// </summary>
    public const int OneYearInSeconds = 60 * 60 * 24 * 365;

    /// <summary>
    /// Gets unix epoch start time.
    /// </summary>
    /// <remarks>
    /// https://en.wikipedia.org/wiki/Unix_time.
    /// </remarks>
    public static DateTimeOffset UnixEpochStart => new(
        dateTime: UnixEpochStartDateTime,
        offset: TimeSpan.Zero);

    public static DateTime UnixEpochStartDateTime => new(
        year: 1970,
        month: 1,
        day: 1,
        hour: 0,
        minute: 0,
        second: 0,
        kind: DateTimeKind.Utc);
}
