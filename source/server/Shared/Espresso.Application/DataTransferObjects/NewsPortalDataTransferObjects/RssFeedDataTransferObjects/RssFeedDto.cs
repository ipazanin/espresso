// RssFeedDto.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Linq.Expressions;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.RssFeedEnums;

namespace Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects.RssFeedDataTransferObjects;

public class RssFeedDto
{
    public RssFeedDto(
        int id,
        string url,
        RequestType requestType,
        AmpConfigurationDto ampConfiguration,
        CategoryParseConfigurationDto categoryParseConfiguration,
        ImageUrlParseConfigurationDto imageUrlParseConfiguration,
        SkipParseConfigurationDto skipParseConfiguration,
        int newsPortalId,
        int categoryId)
    {
        Id = id;
        Url = url;
        RequestType = requestType;
        AmpConfiguration = ampConfiguration;
        CategoryParseConfiguration = categoryParseConfiguration;
        ImageUrlParseConfiguration = imageUrlParseConfiguration;
        SkipParseConfiguration = skipParseConfiguration;
        NewsPortalId = newsPortalId;
        CategoryId = categoryId;
    }

    private RssFeedDto()
    {
    }

    public int Id { get; set; }

    public string Url { get; set; } = string.Empty;

    public RequestType RequestType { get; set; }

    public AmpConfigurationDto AmpConfiguration { get; set; } = null!;

    public CategoryParseConfigurationDto CategoryParseConfiguration { get; set; } = null!;

    public ImageUrlParseConfigurationDto ImageUrlParseConfiguration { get; set; } = null!;

    public SkipParseConfigurationDto SkipParseConfiguration { get; set; } = null!;

    public int NewsPortalId { get; set; }

    public int CategoryId { get; set; }

    public static Expression<Func<RssFeed, RssFeedDto>> GetProjection()
    {
        var ampConfigurationProjection = AmpConfigurationDto.Projection.Compile();
        var categoryParseConfigurationProjection = CategoryParseConfigurationDto.Projection.Compile();
        var imageUrlParseConfigurationProjection = ImageUrlParseConfigurationDto.Projection.Compile();
        var skipParseConfigurationProjection = SkipParseConfigurationDto.Projection.Compile();

        return rssFeed => new RssFeedDto
        {
            Id = rssFeed.Id,
            Url = rssFeed.Url,
            RequestType = rssFeed.RequestType,
            AmpConfiguration = ampConfigurationProjection.Invoke(rssFeed.AmpConfiguration),
            CategoryParseConfiguration = categoryParseConfigurationProjection.Invoke(rssFeed.CategoryParseConfiguration),
            ImageUrlParseConfiguration = imageUrlParseConfigurationProjection.Invoke(rssFeed.ImageUrlParseConfiguration),
            SkipParseConfiguration = skipParseConfigurationProjection.Invoke(rssFeed.SkipParseConfiguration),
            NewsPortalId = rssFeed.NewsPortalId,
            CategoryId = rssFeed.CategoryId,
        };
    }

    public RssFeed CreateRssFeed()
    {
        return new RssFeed(
            id: Id,
            url: Url,
            requestType: RequestType,
            ampConfiguration: AmpConfiguration.CreateAmpConfiguration(),
            categoryParseConfiguration: CategoryParseConfiguration.CreateCategoryParseConfiguration(),
            imageUrlParseConfiguration: ImageUrlParseConfiguration.CreateImageUrlParseConfiguration(),
            skipParseConfiguration: SkipParseConfiguration.CreateSkipParseConfiguration(),
            newsPortalId: NewsPortalId,
            categoryId: CategoryId);
    }
}
