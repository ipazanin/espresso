SELECT AVG(CAST(InnerQuery.Count AS FLOAT)) FROM (
    SELECT SimilarArticles.MainArticleId, COUNT(*)+1 AS [Count] FROM SimilarArticles
    GROUP BY SimilarArticles.MainArticleId
) AS InnerQuery
