USE EspressoDb
GO

SELECT
    DATEPART(weekday, ApplicationDownload.DownloadedTime) AS DayOfWeek,
    COUNT(ApplicationDownload.Id) AS NumberOfDownloads
FROM ApplicationDownload
GROUP BY DATEPART(weekday, ApplicationDownload.DownloadedTime)
ORDER BY COUNT(ApplicationDownload.Id) DESC
GO