// AmpConfigurationDto.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Linq.Expressions;
using Espresso.Domain.ValueObjects.RssFeedValueObjects;

namespace Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects.RssFeedDataTransferObjects;

public class AmpConfigurationDto
{
    public AmpConfigurationDto(bool? hasAmpArticles, string? templateUrl)
    {
        HasAmpArticles = hasAmpArticles;
        TemplateUrl = templateUrl;
    }

    private AmpConfigurationDto()
    {
    }

    public static Expression<Func<AmpConfiguration, AmpConfigurationDto>> Projection
    {
        get => ampConfiguration => new AmpConfigurationDto
        {
            HasAmpArticles = ampConfiguration.HasAmpArticles,
            TemplateUrl = ampConfiguration.TemplateUrl,
        };
    }

    public bool? HasAmpArticles { get; set; }

    /// <summary>
    /// Gets {0} = ArticleId
    /// {1} = Third article segment
    /// {2} = Second Article Segment
    /// {1} = First Article Segment.
    /// </summary>
    public string? TemplateUrl { get; set; }

    public AmpConfiguration CreateAmpConfiguration()
    {
        return new AmpConfiguration(HasAmpArticles, TemplateUrl);
    }
}
