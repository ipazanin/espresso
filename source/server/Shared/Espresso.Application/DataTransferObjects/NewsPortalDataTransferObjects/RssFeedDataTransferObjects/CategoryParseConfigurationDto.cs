// CategoryParseConfigurationDto.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Linq.Expressions;
using Espresso.Domain.Enums.RssFeedEnums;
using Espresso.Domain.ValueObjects.RssFeedValueObjects;

namespace Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects.RssFeedDataTransferObjects;

public class CategoryParseConfigurationDto
{
    public CategoryParseConfigurationDto(CategoryParseStrategy categoryParseStrategy)
    {
        CategoryParseStrategy = categoryParseStrategy;
    }

    private CategoryParseConfigurationDto()
    {
    }

    public static Expression<Func<CategoryParseConfiguration, CategoryParseConfigurationDto>> Projection
    {
        get => categoryParseConfiguration => new CategoryParseConfigurationDto
        {
            CategoryParseStrategy = categoryParseConfiguration.CategoryParseStrategy,
        };
    }

    public CategoryParseStrategy CategoryParseStrategy { get; set; }

    public CategoryParseConfiguration CreateCategoryParseConfiguration()
    {
        return new CategoryParseConfiguration(CategoryParseStrategy);
    }
}
