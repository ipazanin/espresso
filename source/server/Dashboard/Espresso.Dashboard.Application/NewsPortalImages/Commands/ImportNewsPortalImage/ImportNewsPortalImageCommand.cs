// ImportNewsPortalImageCommand.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects;
using MediatR;

namespace Espresso.Dashboard.Application.NewsPortalImages.Commands.ImportNewsPortalImage;

public class ImportNewsPortalImageCommand : IRequest
{
    public ImportNewsPortalImageCommand(NewsPortalImageDto? newsPortalImage)
    {
        NewsPortalImage = newsPortalImage;
    }

    public NewsPortalImageDto? NewsPortalImage { get; }
}
