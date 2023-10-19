// NewsPortalDto.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects;

/// <summary>
/// <see cref="NewsPortal"/> details.
/// </summary>
public class NewsPortalDto
{
    public NewsPortalDto(
        int id,
        string name,
        string baseUrl,
        string iconUrl,
        bool? isNewOverride,
        DateTimeOffset createdAt,
        bool isEnabled,
        int categoryId,
        int regionId,
        int countryId)
    {
        Id = id;
        Name = name;
        BaseUrl = baseUrl;
        IconUrl = iconUrl;
        IsNewOverride = isNewOverride;
        CreatedAt = createdAt;
        IsEnabled = isEnabled;
        CategoryId = categoryId;
        RegionId = regionId;
        CountryId = countryId;
    }

    private NewsPortalDto()
    {
    }

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string BaseUrl { get; set; } = string.Empty;

    public string IconUrl { get; set; } = string.Empty;

    public bool? IsNewOverride { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public bool IsEnabled { get; set; }

    public int CategoryId { get; set; }

    public int RegionId { get; set; }

    public int CountryId { get; set; }

    public static Expression<Func<NewsPortal, NewsPortalDto>> GetProjection()
    {
        return newsPortal => new NewsPortalDto
        {
            Id = newsPortal.Id,
            Name = newsPortal.Name,
            BaseUrl = newsPortal.BaseUrl,
            IconUrl = newsPortal.IconUrl,
            IsNewOverride = newsPortal.IsNewOverride,
            CreatedAt = newsPortal.CreatedAt,
            IsEnabled = newsPortal.IsEnabled,
            CategoryId = newsPortal.CategoryId,
            RegionId = newsPortal.RegionId,
            CountryId = newsPortal.CountryId,
        };
    }

    public NewsPortal CreateNewsPortal()
    {
        return new NewsPortal(
            id: Id,
            name: Name,
            baseUrl: BaseUrl,
            iconUrl: IconUrl,
            isNewOverride: IsNewOverride,
            createdAt: CreatedAt,
            categoryId: CategoryId,
            regionId: RegionId,
            isEnabled: IsEnabled,
            countryId: CountryId);
    }
}
