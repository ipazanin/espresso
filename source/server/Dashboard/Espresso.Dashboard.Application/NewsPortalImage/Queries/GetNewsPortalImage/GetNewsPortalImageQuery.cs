// GetNewsPortalImageQuery.cs
//
// © 2022 Espresso News. All rights reserved.

using MediatR;

namespace Espresso.Dashboard.Application.NewsPortalImage.Queries.GetNewsPortalImage;

public class GetNewsPortalImageQuery : IRequest<GetNewsPortalImageQueryResponse>
{
    public GetNewsPortalImageQuery(int newsPortalId)
    {
        NewsPortalId = newsPortalId;
    }

    public int NewsPortalId { get; }
}
