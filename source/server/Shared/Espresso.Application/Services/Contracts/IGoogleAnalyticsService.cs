// IGoogleAnalyticsService.cs
//
// © 2022 Espresso News. All rights reserved.

namespace Espresso.Application.Services.Contracts;

/// <summary>
/// Google analytics service.
/// </summary>
public interface IGoogleAnalyticsService
{
    /// <summary>
    /// Gets yesterdays active users.
    /// </summary>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public Task<(int androidUsers, int iosUsers)> GetNumberOfActiveUsersFromYesterday();

    /// <summary>
    /// Gets yesterdays revenue.
    /// </summary>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public Task<(decimal androidRevenue, decimal iosRevenue)> GetTotalRevenueFromYesterday();

    /// <summary>
    /// Gets current month revenue.
    /// </summary>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public Task<(decimal androidRevenue, decimal iosRevenue)> GetTotalRevenueForCurrentMonth();

    /// <summary>
    /// Gets previous month revenue.
    /// </summary>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public Task<(decimal androidRevenue, decimal iosRevenue)> GetTotalRevenueForPreviousMonth();
}
