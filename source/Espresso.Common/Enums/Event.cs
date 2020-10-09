namespace Espresso.Common.Enums
{
    public enum Event
    {
        Undefined = 0,
        CustomExceptionFilterAttribute = 1,
        ParserInit = 17,
        RssFeedLoading = 19,
        CreateArticle = 20,
        SendNewAndUpdatedArticlesRequest = 21,
        SlackServiceException = 29,
        WebApiInit = 30,
        CronJob = 31,
        DeleteArticlesJob = 32,
        ImageUrlWebScrapingRequest = 33,
        ImageUrlWebScrapingData = 34,
        MediatorRequest = 36
    }
}
