USE EspressoDb
GO

SELECT
    NewsPortals.Name,
    GroupedArticles.NumberOfArticlesWithoutImageUrl
FROM
    (SELECT Articles.NewsPortalId, COUNT(*) AS NumberOfArticlesWithoutImageUrl
    FROM Articles
    WHERE Articles.ImageUrl IS NULL
    GROUP BY Articles.NewsPortalId
) AS GroupedArticles
    INNER JOIN NewsPortals ON NewsPortals.Id = GroupedArticles.NewsPortalId
ORDER BY GroupedArticles.NumberOfArticlesWithoutImageUrl DESC
GO