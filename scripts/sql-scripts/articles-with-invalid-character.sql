select 
    DISTINCT("NewsPortals"."Name")
from "Articles"
inner join "NewsPortals" on "NewsPortals"."Id" = "Articles"."NewsPortalId"
where "Articles"."Title" like '%�%'