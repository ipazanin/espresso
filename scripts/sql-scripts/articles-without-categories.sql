SELECT *
FROM "Articles"
WHERE (
    SELECT COUNT(*)
    FROM "ArticleCategories"
    WHERE "ArticleCategories"."ArticleId" = "Articles"."Id"
) = 0
