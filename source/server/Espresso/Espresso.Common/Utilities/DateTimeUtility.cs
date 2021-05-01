using System;

using Espresso.Common.Constants;

namespace Espresso.Domain.Utilities
{
    public static class DateTimeUtility
    {

        #region Properties
        /// <summary>
        /// Returns Current Milliseconds in Unix UTC format
        /// </summary>
        public static long CurrentMilliseconds => GetMilliseconds(DateTime.UtcNow);

        /// <summary>
        /// Yesterdays Date
        /// </summary>
        /// <returns></returns>
        public static DateTime YesterdaysDate => DateTime.UtcNow.AddDays(-1).Date;
        #endregion

        #region Methods
        /// <summary>
        /// Gets milliseconds in Unix UTC format from DateTime
        /// </summary>
        /// <param name="dateTime">DateTime</param>
        /// <returns></returns>
        public static long GetMilliseconds(DateTime dateTime)
        {
            return (long)(dateTime - DateTimeConstants.UnixEpochStartTime).TotalMilliseconds;
        }

        /// <summary>
        /// Gets milliseconds truncated to last day
        /// </summary>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public static long TruncateMillisecondsToDate(long milliseconds)
        {
            return milliseconds - (milliseconds % (long)TimeSpan.FromDays(1).TotalMilliseconds);
        }

        /// <summary>
        /// Gets DateTime from Unix UTC milliseconds
        /// </summary>
        /// <param name="milliseconds">Unix UTC time in milliseconds</param>
        /// <returns></returns>
        public static DateTime GetDateTime(long milliseconds)
        {
            return DateTimeConstants.UnixEpochStartTime.AddMilliseconds(milliseconds);
        }
        #endregion
    }
}
