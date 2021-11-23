SELECT "InnerQuery"."Count" as "GroupingCount", COUNT(*) as "Count" FROM (
    SELECT "SimilarArticles"."MainArticleId", COUNT(*)+1 AS "Count" FROM "SimilarArticles"
    GROUP BY "SimilarArticles"."MainArticleId"
) AS "InnerQuery"
GROUP BY "InnerQuery"."Count"
ORDER BY "InnerQuery"."Count" desc
