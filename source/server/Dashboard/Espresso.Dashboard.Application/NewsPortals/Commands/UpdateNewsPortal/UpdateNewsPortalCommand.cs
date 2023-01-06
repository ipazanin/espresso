// UpdateNewsPortalCommand.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects;
using MediatR;

namespace Espresso.Dashboard.Application.NewsPortals.Commands.UpdateNewsPortal;

public class UpdateNewsPortalCommand : IRequest
{
    public UpdateNewsPortalCommand(NewsPortalDto newsPortal)
    {
        NewsPortal = newsPortal;
    }

    public NewsPortalDto NewsPortal { get; }
}
