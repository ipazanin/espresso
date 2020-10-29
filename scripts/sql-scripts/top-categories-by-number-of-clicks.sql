USE EspressoDb
GO

SELECT
    Categories.Name,
    COUNT(Articles.Id) AS NumberOfArticles,
    SUM(Articles.NumberOfClicks) AS TotalNumberOfClicks,
    CAST(SUM(Articles.NumberOfClicks) AS FLOAT) / CAST(COUNT(Articles.Id) AS FLOAT) AS AverageNumberOfClicks
FROM Articles
    INNER JOIN ArticleCategories ON ArticleCategories.ArticleId = Articles.Id
    INNER JOIN Categories ON Categories.Id = ArticleCategories.CategoryId
WHERE Articles.PublishDateTime > DATEADD(day, -1, GETUTCDATE())
GROUP BY Categories.Name
-- ORDER BY CAST(SUM(Articles.NumberOfClicks) AS FLOAT) / CAST(COUNT(Articles.Id) AS FLOAT) DESC
ORDER BY SUM(Articles.NumberOfClicks) DESC
GO