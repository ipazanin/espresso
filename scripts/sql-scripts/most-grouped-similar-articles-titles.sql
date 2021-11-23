SELECT * FROM (
    SELECT "SimilarArticles"."MainArticleId", COUNT(*)+1 AS "Count" FROM "SimilarArticles"
    GROUP BY "SimilarArticles"."MainArticleId"
) AS "InnerQuery"
ORDER BY "InnerQuery"."Count" DESC

SELECT
    "MainArticles"."Title", 
    "SubordinateArticles"."Title", 
    "SimilarArticles"."SimilarityScore"
FROM "SimilarArticles"
    INNER JOIN "Articles" AS "MainArticles" ON "SimilarArticles"."MainArticleId" = "MainArticles"."Id"
    INNER JOIN "Articles" AS "SubordinateArticles" ON "SimilarArticles"."SubordinateArticleId" = "SubordinateArticles"."Id"
WHERE "MainArticles"."Id" = '41ee6386-8ce2-44ef-bdf5-a3257c675cb4' 

-- d87b8893-b55d-47e0-a48a-505b392b3709	66
-- ea833452-45ef-48b0-ae9f-246043deff5d	25
-- 47ccd2d9-d1f1-4609-823f-91bc4b503c9f	20
-- c31ab06d-534e-4168-9e20-1e683c35fbdc	19
-- 41ee6386-8ce2-44ef-bdf5-a3257c675cb4	18
-- b751ab51-e957-4628-a756-325115a1ad60	17
-- e3e97c86-7f9d-4817-8a5d-7c64fd3a4895	17
-- fc118b59-d10e-40e4-bf60-0231b719ae3c	17
-- e6c06d9f-c512-4298-8017-baf8d992ebaf	14
-- 41864534-c701-4e79-b43e-338cf160d334	14