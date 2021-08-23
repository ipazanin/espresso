// IGoogleAnalyticsService.cs
//
// © 2021 Espresso News. All rights reserved.

using System.Threading.Tasks;

namespace Espresso.Application.Services.Contracts
{
    /// <summary>
    /// Google analytics service.
    /// </summary>
    public interface IGoogleAnalyticsService
    {
        /// <summary>
        /// Gets yesterdays active users.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public Task<int> GetNumberOfActiveUsersFromYesterday();

        /// <summary>
        /// Gets yesterdays revenue.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public Task<decimal> GetTotalRevenueFromYesterday();
    }
}
