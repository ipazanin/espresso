// GetNewsPortalImageQueryResponse.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects;

namespace Espresso.Dashboard.Application.NewsPortalImage.Queries.GetNewsPortalImage;

public class GetNewsPortalImageQueryResponse
{
    public GetNewsPortalImageQueryResponse(NewsPortalImageDto? newsPortalImage)
    {
        NewsPortalImage = newsPortalImage;
    }

    public NewsPortalImageDto? NewsPortalImage { get; set; }
}
