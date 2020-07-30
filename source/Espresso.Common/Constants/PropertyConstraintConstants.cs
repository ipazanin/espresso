namespace Espresso.Common.Constants
{
    public static class PropertyConstraintConstants
    {
        #region ApplicationDownload
        public const int ApplicationDownloadWebApiVersionHasMaxLenght = 10;
        public const bool ApplicationDownloadWebApiVersionIsRequired = true;

        public const int ApplicationDownloadMobileAppVersionHasMaxLenght = 20;
        public const bool ApplicationDownloadMobileAppVersionIsRequired = true;

        public const bool ApplicationDownloadMobileDeviceTypeisRequired = true;

        public const bool ApplicationDownloadDownloadedTimeIsRequired = true;
        #endregion

        #region Author
        public const int AUTHOR_NAME_HASMAXLENGHT = 100;
        public const int AUTHOR_EMAIL_HASMAXLENGHT = 100;

        public const bool AUTHOR_NAME_ISREQUIRED = false;
        public const bool AUTHOR_EMAIL_ISREQUIRED = false;
        #endregion

        #region Category
        public const int CategoryNameHasMaxLenght = 20;
        public const bool CategoryNameIsRequired = true;

        public const int CategoryColorHasMaxLenght = 6;
        public const bool CategoryColorIsRequired = true;

        public const int CategoryKeyWordsRegexPatterHasMaxLenght = 1000;
        public const bool CategoryKeyWordsRegexPatternIsRequired = false;
        #endregion

        #region NewsPortal
        public const int NEWSPORTAL_NAME_HASMAXLENGHT = 100;
        public const int NEWSPORTAL_BASEURL_HASMAXLENGHT = 100;
        public const int NEWSPORTAL_ICONURL_HASMAXLENGHT = 100;

        public const bool NEWSPORTAL_NAME_ISREQUIRED = true;
        public const bool NEWSPORTAL_BASEURL_ISREQUIRED = true;
        public const bool NEWSPORTAL_ICONURL_ISREQUIRED = true;
        #endregion

        #region RssFeed
        public const int RssFeedContentDescriptionHasMaxLength = 300;
        public const bool RssFeedContentDescriptionIsRequired = false;

        public const int RssFeedUrlHasMaxLength = 300;
        public const bool RssFeedUrlIsRequired = true;

        public const int RssFeedImgElementXPathHasMaxLength = 300;
        public const bool RssFeedImgElementXPathIsRequired = true;

        public const int RssFeedAmpConfigurationTemplateUrlHasMaxLength = 300;
        public const bool RssFeedAmpConfigurationTemplateUrlIsRequired = false;
        #endregion

        #region RssFeedCategory
        public const int RssFeedCategoryUrlRegexHasMaxLength = 100;
        public const bool RssFeedCategoryUrlRegexIsRequired = true;
        #endregion

        #region Region
        public const int RegionNameHasMaxLength = 100;
        public const bool RegionNameIsRequired = true;
        #endregion
    }
}
