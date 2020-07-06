using System;

using Espresso.Common.Constants;

namespace Espresso.Domain.Utilities
{
    public static class DateTimeUtility
    {
        /// <summary>
        /// Gets DateTime from Unix UTC miliseconds
        /// </summary>
        /// <param name="miliseconds">Unix UTC time in miliseconds</param>
        /// <returns></returns>
        public static DateTime GetDateTime(long miliseconds)
        {
            return DateTimeConstants.UnixEpochStartTime.AddMilliseconds(miliseconds);
        }

        /// <summary>
        /// Returns Current Miliseconds in Unix UTC format
        /// </summary>
        public static long CurrentMiliseconds => GetMiliseconds(DateTime.UtcNow);

        /// <summary>
        /// Gets miliseconds in Unix UTC format from DateTime
        /// </summary>
        /// <param name="dateTime">DateTime</param>
        /// <returns></returns>
        public static long GetMiliseconds(DateTime dateTime)
        {
            return (long)(dateTime - DateTimeConstants.UnixEpochStartTime).TotalMilliseconds;
        }

        /// <summary>
        /// Gets miliseconds truncated to last day
        /// </summary>
        /// <param name="miliseconds"></param>
        /// <returns></returns>
        public static long TruncateMilisecondsToDate(long miliseconds)
        {
            return miliseconds - (miliseconds % (long)TimeSpan.FromDays(1).TotalMilliseconds);
        }
    }
}
