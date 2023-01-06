// GetRssFeedsQuery.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.PagingDataTransferObjects;
using MediatR;

namespace Espresso.Dashboard.Application.RssFeeds.Queries.GetRssFeeds;

public class GetRssFeedsQuery : IRequest<GetRssFeedsQueryResponse>
{
    public GetRssFeedsQuery(PagingParameters pagingParameters)
    {
        PagingParameters = pagingParameters;
    }

    public PagingParameters PagingParameters { get; }
}
