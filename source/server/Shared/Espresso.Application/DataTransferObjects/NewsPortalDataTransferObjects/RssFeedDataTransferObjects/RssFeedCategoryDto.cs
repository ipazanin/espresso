// RssFeedCategoryDto.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects.RssFeedDataTransferObjects;

public class RssFeedCategoryDto
{
    public RssFeedCategoryDto(
        int id,
        string urlRegex,
        int urlSegmentIndex,
        int categoryId,
        int rssFeedId)
    {
        Id = id;
        UrlRegex = urlRegex;
        UrlSegmentIndex = urlSegmentIndex;
        CategoryId = categoryId;
        RssFeedId = rssFeedId;
    }

    private RssFeedCategoryDto()
    {
    }

    public static Expression<Func<RssFeedCategory, RssFeedCategoryDto>> Projection
    {
        get => rssFeedCategory => new RssFeedCategoryDto
        {
            Id = rssFeedCategory.Id,
            UrlRegex = rssFeedCategory.UrlRegex,
            UrlSegmentIndex = rssFeedCategory.UrlSegmentIndex,
            CategoryId = rssFeedCategory.CategoryId,
            RssFeedId = rssFeedCategory.RssFeedId,
        };
    }

    public int Id { get; set; }

    public string UrlRegex { get; set; } = string.Empty;

    public int UrlSegmentIndex { get; set; }

    public int CategoryId { get; set; }

    public int RssFeedId { get; set; }

    public RssFeedCategory CreateRssFeedCategory()
    {
        return new RssFeedCategory(
            id: Id,
            urlRegex: UrlRegex,
            urlSegmentIndex: UrlSegmentIndex,
            categoryId: CategoryId,
            rssFeedId: RssFeedId);
    }
}
