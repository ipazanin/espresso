// UpdateRegionCommand.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects;
using MediatR;

namespace Espresso.Dashboard.Application.Regions.Commands.UpdateRegion;

public class UpdateRegionCommand : IRequest
{
    public UpdateRegionCommand(RegionDto region)
    {
        Region = region;
    }

    public RegionDto Region { get; }
}
