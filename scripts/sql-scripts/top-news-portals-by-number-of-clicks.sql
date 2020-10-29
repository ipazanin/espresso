USE EspressoDb
GO

SELECT
    NewsPortals.Name,
    COUNT(Articles.Id) AS NumberOfArticles,
    SUM(Articles.NumberOfClicks) AS TotalNumberOfClicks,
    CAST(SUM(Articles.NumberOfClicks) AS FLOAT) / CAST(COUNT(Articles.Id) AS FLOAT) AS AverageNumberOfClicks
FROM Articles
    INNER JOIN NewsPortals ON NewsPortals.Id = Articles.NewsPortalId
WHERE Articles.PublishDateTime > DATEADD(day, -1, GETUTCDATE())
GROUP BY NewsPortals.Name
-- ORDER BY CAST(SUM(Articles.NumberOfClicks) AS FLOAT) / CAST(COUNT(Articles.Id) AS FLOAT) DESC
ORDER BY SUM(Articles.NumberOfClicks) DESC
GO