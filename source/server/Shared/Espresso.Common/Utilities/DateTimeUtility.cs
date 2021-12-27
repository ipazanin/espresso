// DateTimeUtility.cs
//
// © 2021 Espresso News. All rights reserved.

namespace Espresso.Domain.Utilities;

/// <summary>
/// <see cref="DateTime"/> utility.
/// </summary>
public static class DateTimeUtility
{
    /// <summary>
    /// Gets yesterdays Date.
    /// </summary>
    public static DateTimeOffset YesterdaysDate => DateTimeOffset.UtcNow.AddDays(-1).Date;

    /// <summary>
    /// Gets milliseconds truncated to last day.
    /// </summary>
    /// <param name="milliseconds">Miliseconds.</param>
    /// <returns>Milliseconds truncated to last day.</returns>
    public static long TruncateMillisecondsToDate(long milliseconds)
    {
        return milliseconds - (milliseconds % (long)TimeSpan.FromDays(1).TotalMilliseconds);
    }
}
