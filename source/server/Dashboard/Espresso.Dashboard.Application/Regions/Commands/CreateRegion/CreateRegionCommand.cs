// CreateRegionCommand.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects;
using MediatR;

namespace Espresso.Dashboard.Application.Regions.Commands.CreateRegion;

public class CreateRegionCommand : IRequest
{
    public CreateRegionCommand(RegionDto region)
    {
        Region = region;
    }

    public RegionDto Region { get; }
}
