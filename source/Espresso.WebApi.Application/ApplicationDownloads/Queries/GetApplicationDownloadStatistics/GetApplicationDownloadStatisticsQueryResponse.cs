namespace Espresso.WebApi.Application.ApplicationDownloads.Queries.GetApplicationDownloadStatistics
{
    public class GetApplicationDownloadStatisticsQueryResponse
    {
        public int AndroidDownloadsCount { get; }

        public int IosDownloadsCount { get; }

        public int TotalDownloadCount { get; }

        public GetApplicationDownloadStatisticsQueryResponse(int androidDownloadsCount, int iosDownloadsCount, int totalDownloadCount)
        {
            AndroidDownloadsCount = androidDownloadsCount;
            IosDownloadsCount = iosDownloadsCount;
            TotalDownloadCount = totalDownloadCount;
        }

        public override string ToString()
        {
            return $"{nameof(AndroidDownloadsCount)}:{AndroidDownloadsCount}, " +
                $"{nameof(IosDownloadsCount)}:{IosDownloadsCount}, " +
                $"{nameof(TotalDownloadCount)}:{TotalDownloadCount}";
        }
    }
}
