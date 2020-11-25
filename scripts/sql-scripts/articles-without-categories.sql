USE EspressoDb
GO

SELECT *
FROM Articles
WHERE (
    SELECT
    COUNT(*)
FROM ArticleCategories
WHERE ArticleCategories.ArticleId = ARTICLES.Id
    ) = 0
GO