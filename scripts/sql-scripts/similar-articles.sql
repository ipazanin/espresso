SELECT
    "FirstArticles"."Title", 
    "SecondArticles"."Title", 
    "SimilarArticles"."SimilarityScore",
    "FirstArticlesNewsPortal"."Name",
    "SecondArticlesNewsPortal"."Name"
FROM "SimilarArticles"
    INNER JOIN "Articles" AS "FirstArticles" ON "SimilarArticles"."FirstArticleId" = "FirstArticles"."Id"
    INNER JOIN "Articles" AS "SecondArticles" ON "SimilarArticles"."SecondArticleId" = "SecondArticles"."Id"
    INNER JOIN "NewsPortals" AS "FirstArticlesNewsPortal" ON "FirstArticles"."NewsPortalId" = "FirstArticlesNewsPortal"."Id"
    INNER JOIN "NewsPortals" AS "SecondArticlesNewsPortal" ON "SecondArticles"."NewsPortalId" = "SecondArticlesNewsPortal"."Id"
ORDER BY "SimilarArticles"."SimilarityScore"