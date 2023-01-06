// DeleteRegionCommand.cs
//
// © 2022 Espresso News. All rights reserved.

using MediatR;

namespace Espresso.Dashboard.Application.Regions.Commands.DeleteRegion;

public class DeleteRegionCommand : IRequest
{
    public DeleteRegionCommand(int regionId)
    {
        RegionId = regionId;
    }

    public int RegionId { get; }
}
