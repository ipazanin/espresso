namespace Espresso.Common.Enums
{
    public enum Event
    {
        Undefined = 0,
        CustomExceptionFilterAttribute = 1,
        CreateApplicationDownloadCommand = 2,
        GetApplicationDownloadStatisticsQuery = 3,
        CalculateTrendingScoreCommand = 4,
        DeleteOldArticlesCommand = 5,
        IncrementNumberOfClicksCommand = 6,
        UpdateInMemoryArticlesCommand = 7,
        GetCategoryArticlesQuery = 8,
        GetLatestArticlesQuery = 9,
        GetTrendingArticlesQuery = 10,
        GetCategoriesQuery = 11,
        GetConfigurationQuery = 12,
        GetNewsPortalsQuery = 13,
        SendArticlesNotificationsQuery = 15,
        ParseRssFeedsCommand = 16,
        MemoryCacheInit = 17,
        ParserDeleterWebJob = 18,
        RssFeedLoading = 19,
        ArticleParsing = 20,
        ParserDeleterNewArticlesRequest = 21,
        GetNewNewsPortalsQuery = 22,
    }
}
