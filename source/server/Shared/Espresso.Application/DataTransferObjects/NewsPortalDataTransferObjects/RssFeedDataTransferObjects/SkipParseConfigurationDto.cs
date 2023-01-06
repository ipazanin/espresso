// SkipParseConfigurationDto.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Linq.Expressions;
using Espresso.Domain.ValueObjects.RssFeedValueObjects;

namespace Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects.RssFeedDataTransferObjects;

public class SkipParseConfigurationDto
{
    public SkipParseConfigurationDto(int? numberOfSkips)
    {
        NumberOfSkips = numberOfSkips;
    }

    private SkipParseConfigurationDto()
    {
    }

    public static Expression<Func<SkipParseConfiguration, SkipParseConfigurationDto>> Projection
    {
        get => skipParseConfiguration => new SkipParseConfigurationDto
        {
            NumberOfSkips = skipParseConfiguration.NumberOfSkips,
        };
    }

    public int? NumberOfSkips { get; set; }

    public SkipParseConfiguration CreateSkipParseConfiguration()
    {
        return new SkipParseConfiguration(NumberOfSkips, default);
    }
}
