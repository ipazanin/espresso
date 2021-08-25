// DateTimeUtility.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using System;

namespace Espresso.Domain.Utilities
{
    /// <summary>
    /// <see cref="DateTime"/> utility.
    /// </summary>
    public static class DateTimeUtility
    {
        /// <summary>
        /// Gets returns Current Milliseconds in Unix UTC format.
        /// </summary>
        public static long CurrentMilliseconds => GetMilliseconds(DateTime.UtcNow);

        /// <summary>
        /// Gets yesterdays Date.
        /// </summary>
        public static DateTime YesterdaysDate => DateTime.UtcNow.AddDays(-1).Date;

        /// <summary>
        /// Gets milliseconds in Unix UTC format from <paramref name="dateTime"/>.
        /// </summary>
        /// <param name="dateTime">DateTime.</param>
        /// <returns>Milliseconds in Unix UTC format from <paramref name="dateTime"/>.</returns>
        public static long GetMilliseconds(DateTime dateTime)
        {
            return (long)(dateTime - DateTimeConstants.UnixEpochStartTime).TotalMilliseconds;
        }

        /// <summary>
        /// Gets milliseconds truncated to last day.
        /// </summary>
        /// <param name="milliseconds">Miliseconds.</param>
        /// <returns>Milliseconds truncated to last day.</returns>
        public static long TruncateMillisecondsToDate(long milliseconds)
        {
            return milliseconds - (milliseconds % (long)TimeSpan.FromDays(1).TotalMilliseconds);
        }

        /// <summary>
        /// Gets <see cref="DateTime"/> from Unix UTC milliseconds.
        /// </summary>
        /// <param name="milliseconds">Unix UTC time in milliseconds.</param>
        /// <returns><see cref="DateTime"/> from Unix UTC milliseconds.</returns>
        public static DateTime GetDateTime(long milliseconds)
        {
            return DateTimeConstants.UnixEpochStartTime.AddMilliseconds(milliseconds);
        }
    }
}
