SELECT
    DATE_PART('dow', "ApplicationDownload"."DownloadedTime") AS "DayOfWeek",
    COUNT("ApplicationDownload"."Id") AS "NumberOfDownloads"
FROM "ApplicationDownload"
GROUP BY DATE_PART('dow', "ApplicationDownload"."DownloadedTime")
ORDER BY COUNT("ApplicationDownload"."Id") DESC