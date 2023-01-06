// DeleteNewsPortalCommand.cs
//
// © 2022 Espresso News. All rights reserved.

using MediatR;

namespace Espresso.Dashboard.Application.NewsPortals.Commands.DeleteNewsPortal;

public class DeleteNewsPortalCommand : IRequest
{
    public DeleteNewsPortalCommand(int newsPortalId)
    {
        NewsPortalId = newsPortalId;
    }

    public int NewsPortalId { get; }
}
