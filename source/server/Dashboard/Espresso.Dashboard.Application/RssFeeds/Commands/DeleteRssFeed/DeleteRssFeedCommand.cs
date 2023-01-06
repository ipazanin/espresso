// DeleteRssFeedCommand.cs
//
// © 2022 Espresso News. All rights reserved.

using MediatR;

namespace Espresso.Dashboard.Application.RssFeeds.Commands.DeleteRssFeed;

public class DeleteRssFeedCommand : IRequest
{
    public DeleteRssFeedCommand(int rssFeedId)
    {
        RssFeedId = rssFeedId;
    }

    public int RssFeedId { get; }
}
