// ISlackService.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.SlackDataTransferObjects;
using Espresso.Domain.Entities;

namespace Espresso.Application.Services.Contracts;

/// <summary>
/// Used to send messages to slack.
/// </summary>
public interface ISlackService
{
    /// <summary>
    /// Send error message to slack.
    /// </summary>
    /// <param name="eventName">EventName.</param>
    /// <param name="message">Message.</param>
    /// <param name="exception">Exception.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    public Task LogError(
        string eventName,
        string message,
        Exception exception,
        CancellationToken cancellationToken);

    /// <summary>
    /// Send application downloads statistics to slack.
    /// </summary>
    /// <param name="yesterdayAndroidCount">Yesterday android downloads count.</param>
    /// <param name="yesterdayIosCount">Yesterday iOS downloads count.</param>
    /// <param name="totalAndroidCount">Total android downloads count.</param>
    /// <param name="totalIosCount">Total iOS downloads count.</param>
    /// <param name="activeUsers">Number of active users.</param>
    /// <param name="revenue">Revenue.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    public Task LogAppDownloadStatistics(
        int yesterdayAndroidCount,
        int yesterdayIosCount,
        int totalAndroidCount,
        int totalIosCount,
        (int activeUsersOnAndroid, int activeUsersOnIos) activeUsers,
        (decimal androidRevenue, decimal iosRevenue) revenue,
        CancellationToken cancellationToken);

    /// <summary>
    /// Send missing categories to slack.
    /// </summary>
    /// <param name="rssFeedUrl">Rss feed url.</param>
    /// <param name="articleUrl">Article url.</param>
    /// <param name="urlCategories">Url categories.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    public Task LogMissingCategoriesError(
        string rssFeedUrl,
        string articleUrl,
        string urlCategories,
        CancellationToken cancellationToken);

    /// <summary>
    /// Send news portal request slack.
    /// </summary>
    /// <param name="newsPortalName">News portal name.</param>
    /// <param name="email">User email.</param>
    /// <param name="url">Url.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    public Task LogNewNewsPortalRequest(
        string newsPortalName,
        string email,
        string? url,
        CancellationToken cancellationToken);

    /// <summary>
    /// Send yesterdays statistics to slack.
    /// </summary>
    /// <param name="topArticles">Top articles.</param>
    /// <param name="totalNumberOfClicks">Total clicks on articles yesterday.</param>
    /// <param name="topNewsPortals">Top news portals.</param>
    /// <param name="categoriesWithNumberOfClicks">Categories with clicks.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    public Task LogYesterdaysStatistics(
        IEnumerable<Article> topArticles,
        int totalNumberOfClicks,
        IEnumerable<(NewsPortal newsPortal, int numberOfClicks, IEnumerable<Article> articles)> topNewsPortals,
        IEnumerable<(Category category, int numberOfClicks, IEnumerable<Article> articles)> categoriesWithNumberOfClicks,
        CancellationToken cancellationToken);

    /// <summary>
    /// Send push notification to slack.
    /// </summary>
    /// <param name="pushNotification">Push notification.</param>
    /// <param name="article">Article.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    public Task LogPushNotification(
        PushNotification pushNotification,
        Article article,
        CancellationToken cancellationToken);

    /// <summary>
    /// Sends message to slack.
    /// </summary>
    /// <param name="data">Message data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    public Task SendToSlack(
        SlackWebHookRequestBodyDto data,
        CancellationToken cancellationToken);
}
