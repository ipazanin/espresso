using System;
using System.Collections.Generic;

namespace Espresso.Common.Constants
{
    public static class MemoryCacheConstants
    {
        #region NewsPortals

        public const string NewsPortalKey = "NewsPortal";
        public static TimeSpan NewsPortalDuration => TimeSpan.FromHours(8);

        #endregion

        #region Categories
        public const string CategoryKey = "Category";
        public static TimeSpan CategoryDuration => TimeSpan.FromHours(8);
        #endregion

        #region RssFeeds
        public const string RssFeedKey = "RssFeed";
        public static TimeSpan RssFeedDuration => TimeSpan.FromHours(8);
        #endregion

        #region ApplicationDownloads

        public const string ApplicationDownloadKey = "ApplicationDownload";
        public static TimeSpan ApplicationDownloadDuration => TimeSpan.FromMinutes(1);

        #endregion

        #region Articles

        public const string ArticleKey = "Article";
        public static TimeSpan ArticleDuration => TimeSpan.FromHours(24);

        #endregion

        #region ArticleCategory

        public const string ArticleCategoryKey = "ArticleCategory";
        public static TimeSpan ArticleCategoryDuration => TimeSpan.FromHours(8);

        #endregion

        #region RssFeedCategory

        public const string RssFeedCategoryKey = "RssFeedCategory";
        public static TimeSpan RssFeedCategoryDuration => TimeSpan.FromHours(24);

        #endregion

        #region Common

        public static IEnumerable<object> GetAllMemoryCacheKey() => new List<object>
        {
            NewsPortalKey,
            CategoryKey,
            RssFeedKey,
            ApplicationDownloadKey,
            ArticleKey,
            ArticleCategoryKey,
            RssFeedCategoryKey,
        };

        public const string DeadLockLogKey = "DeadLockLogKey";

        public const string ApiKeysKey = "ApiKeysKey";

        #endregion
    }
}
