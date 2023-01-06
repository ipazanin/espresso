// CreateNewsPortalCommand.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects;
using MediatR;

namespace Espresso.Dashboard.Application.NewsPortals.Commands.CreateNewsPortal;

public class CreateNewsPortalCommand : IRequest
{
    public CreateNewsPortalCommand(NewsPortalDto newsPortal, NewsPortalImageDto? newsPortalImage)
    {
        NewsPortal = newsPortal;
        NewsPortalImage = newsPortalImage;
    }

    public NewsPortalDto NewsPortal { get; }

    public NewsPortalImageDto? NewsPortalImage { get; }
}
