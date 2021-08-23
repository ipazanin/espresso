// Event.cs
//
// © 2021 Espresso News. All rights reserved.

namespace Espresso.Common.Enums
{
    /// <summary>
    /// Logger event.
    /// </summary>
    public enum Event
    {
#pragma warning disable SA1602 // Enumeration items should be documented
        Undefined = 0,
        CustomExceptionFilterAttribute = 1,
        DashboardEspressoDatabaseInit = 17,
        RssFeedLoading = 19,
        CreateArticle = 20,
        SendNewAndUpdatedArticlesRequest = 21,
        SlackServiceException = 29,
        WebApiInit = 30,
        CronJob = 31,
        DeleteArticlesJob = 32,
        ImageUrlWebScrapingRequest = 33,
        ImageUrlWebScrapingData = 34,
        MediatorRequest = 36,
#pragma warning restore SA1602 // Enumeration items should be documented
    }
}
