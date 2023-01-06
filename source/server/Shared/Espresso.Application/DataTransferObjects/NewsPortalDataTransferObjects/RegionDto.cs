// RegionDto.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects;

public class RegionDto
{
    public RegionDto(int id, string name, string subtitle)
    {
        Id = id;
        Name = name;
        Subtitle = subtitle;
    }

    private RegionDto()
    {
    }

    public static Expression<Func<Region, RegionDto>> Projection
    {
        get => region => new RegionDto
        {
            Id = region.Id,
            Name = region.Name,
            Subtitle = region.Subtitle,
        };
    }

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Subtitle { get; set; } = string.Empty;

    public Region CreateRegion()
    {
        return new Region(Id, Name, Subtitle);
    }
}
