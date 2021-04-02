using System.Threading.Tasks;

namespace Espresso.Application.Services.Contracts
{
    public interface IGoogleAnalyticsService
    {
        public Task<int> GetNumberOfActiveUsersFromYesterday();

        public Task<decimal> GetTotalRevenueFromYesterday();
    }
}
