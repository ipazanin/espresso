// GetRssFeedDetailsQuery.cs
//
// © 2022 Espresso News. All rights reserved.

using MediatR;

namespace Espresso.Dashboard.Application.RssFeeds.Queries.GetRssFeedsDetails;

public class GetRssFeedDetailsQuery : IRequest<GetRssFeedDetailsQueryResponse>
{
    public GetRssFeedDetailsQuery(int rssFeedId)
    {
        RssFeedId = rssFeedId;
    }

    public int RssFeedId { get; }
}
