namespace Espresso.Common.Constants
{
    public static class PropertyConstraintConstants
    {
        #region ApplicationDownload
        public const int ApplicationDownloadWebApiVersionHasMaxLenght = 10;
        public const int ApplicationDownloadMobileAppVersionHasMaxLenght = 20;
        #endregion

        #region Category
        public const int CategoryNameHasMaxLenght = 20;

        public const int CategoryColorHasMaxLenght = 20;

        public const int CategoryKeyWordsRegexPatterHasMaxLenght = 1000;
        #endregion

        #region NewsPortal
        public const int NEWSPORTAL_NAME_HASMAXLENGHT = 100;
        public const int NEWSPORTAL_BASEURL_HASMAXLENGHT = 100;
        public const int NEWSPORTAL_ICONURL_HASMAXLENGHT = 100;
        #endregion

        #region RssFeed
        public const int RssFeedUrlHasMaxLength = 300;

        public const int RssFeedImgElementXPathHasMaxLength = 300;

        public const int RssFeedAmpConfigurationTemplateUrlHasMaxLength = 300;
        #endregion

        #region RssFeedCategory
        public const int RssFeedCategoryUrlRegexHasMaxLength = 100;
        #endregion

        #region Region
        public const int RegionNameHasMaxLength = 100;
        #endregion
    }
}
