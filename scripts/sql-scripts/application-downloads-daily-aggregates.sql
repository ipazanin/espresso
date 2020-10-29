USE EspressoDb
GO

SELECT
    DATEPART(month, ApplicationDownload.DownloadedTime) AS Month,
    DATEPART(day, ApplicationDownload.DownloadedTime) AS DayOfMonth,
    COUNT(ApplicationDownload.Id) AS NumberOfDownloads
FROM ApplicationDownload
GROUP BY 
    DATEPART(year, ApplicationDownload.DownloadedTime), 
    DATEPART(month, ApplicationDownload.DownloadedTime),
    DATEPART(day, ApplicationDownload.DownloadedTime)
ORDER BY     
    DATEPART(year, ApplicationDownload.DownloadedTime) DESC, 
    DATEPART(month, ApplicationDownload.DownloadedTime) DESC,
    DATEPART(day, ApplicationDownload.DownloadedTime) DESC
GO