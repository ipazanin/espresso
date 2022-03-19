// AmpConfiguration.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Infrastructure;

#pragma warning disable RCS1170

namespace Espresso.Domain.ValueObjects.RssFeedValueObjects;

public class AmpConfiguration : ValueObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AmpConfiguration"/> class.
    /// </summary>
    /// <param name="hasAmpArticles"></param>
    /// <param name="templateUrl"></param>
    public AmpConfiguration(bool hasAmpArticles, string templateUrl)
    {
        HasAmpArticles = hasAmpArticles;
        TemplateUrl = templateUrl;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AmpConfiguration"/> class.
    /// ORM Constructor.
    /// </summary>
    private AmpConfiguration()
    {
    }

    public bool HasAmpArticles { get; private set; }

    /// <summary>
    /// Gets {0} = ArticleId
    /// {1} = Third article segment
    /// {2} = Second Article Segment
    /// {1} = First Article Segment.
    /// </summary>
    public string? TemplateUrl { get; private set; }

    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return HasAmpArticles;
        yield return TemplateUrl;
    }
}
