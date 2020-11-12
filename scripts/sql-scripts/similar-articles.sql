USE EspressoDb
GO

SELECT MainArticles.Title, SubordinateArticles.Title FROM SimilarArticles
INNER JOIN Articles AS MainArticles ON SimilarArticles.MainArticleId = MainArticles.Id
INNER JOIN Articles AS SubordinateArticles ON SimilarArticles.SubordinateArticleId = SubordinateArticles.Id
ORDER BY MainArticles.Title