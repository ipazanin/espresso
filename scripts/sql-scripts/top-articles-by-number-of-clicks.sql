USE EspressoDb
GO

SELECT TOP(50)
    Articles.NumberOfClicks,
    Articles.Title,
    NewsPortals.Name,
    Articles.PublishDateTime
FROM Articles
    INNER JOIN NewsPortals ON NewsPortals.Id = Articles.NewsPortalId
ORDER BY Articles.NumberOfClicks DESC
GO