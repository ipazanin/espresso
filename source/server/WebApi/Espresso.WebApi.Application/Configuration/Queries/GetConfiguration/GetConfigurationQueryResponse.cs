﻿// GetConfigurationQueryResponse.cs
//
// © 2022 Espresso News. All rights reserved.

namespace Espresso.WebApi.Application.Configuration.Queries.GetConfiguration;

public record GetConfigurationQueryResponse
{
    public IEnumerable<GetConfigurationCategoryWithNewsPortals> CategoriesWithNewsPortals { get; init; } = new List<GetConfigurationCategoryWithNewsPortals>();

    public IEnumerable<GetConfigurationCategory> Categories { get; init; } = new List<GetConfigurationCategory>();

    public IEnumerable<GetConfigurationRegion> Regions { get; init; } = new List<GetConfigurationRegion>();
}
