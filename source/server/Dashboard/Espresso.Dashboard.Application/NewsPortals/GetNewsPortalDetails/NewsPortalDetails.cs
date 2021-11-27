// NewsPortalDetails.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using Espresso.Domain.Entities;

namespace Espresso.Dashboard.Application.NewsPortals.GetNewsPortalDetails;

/// <summary>
/// <see cref="NewsPortal"/> details.
/// </summary>
public class NewsPortalDetails
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NewsPortalDetails"/> class.
    /// </summary>
    /// <param name="newsPortal">News portal.</param>
    public NewsPortalDetails(NewsPortal newsPortal)
    {
        Id = newsPortal.Id;
        Name = newsPortal.Name;
        BaseUrl = newsPortal.BaseUrl;
        IconUrl = newsPortal.IconUrl;
        IsNewOverride = newsPortal.IsNewOverride switch
        {
            true => true.ToString(),
            false => false.ToString(),
            _ => FormatConstants.Undefined
        };
        CreatedAt = newsPortal.CreatedAt;
        IsEnabled = newsPortal.IsEnabled;
        Category = newsPortal.Category!.Name;
        Region = newsPortal.Region!.Name;
        RssFeeds = newsPortal
            .RssFeeds
            .AsQueryable()
            .Select(RssFeedItem.GetProjection());
    }

    /// <summary>
    /// Gets <see cref="NewsPortal"/> id.
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Gets <see cref="NewsPortal"/> name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets <see cref="NewsPortal"/> name.
    /// </summary>
    public string BaseUrl { get; }

    /// <summary>
    /// Gets <see cref="NewsPortal"/> icon URL.
    /// </summary>
    public string IconUrl { get; }

    /// <summary>
    /// Gets <see cref="NewsPortal"/> is new override.
    /// </summary>
    /// <remarks>
    /// <see cref="IsNewOverride"/> tells applications if news portal is considered new regardless of time when it was added.
    /// </remarks>
    public string IsNewOverride { get; }

    /// <summary>
    /// Gets <see cref="NewsPortal"/> created at.
    /// </summary>
    public DateTimeOffset CreatedAt { get; }

    /// <summary>
    /// Gets a value indicating whether <see cref="NewsPortal"/> is used in application for parsing <see cref="RssFeed"/>.
    /// </summary>
    public bool IsEnabled { get; }

    /// <summary>
    /// Gets <see cref="NewsPortal"/> <see cref="Domain.Entities.Category"/> name.
    /// </summary>
    public string Category { get; }

    /// <summary>
    /// Gets <see cref="NewsPortal"/> <see cref="Domain.Entities.Region"/> name.
    /// </summary>
    public string Region { get; }

    /// <summary>
    /// Gets list of <see cref="RssFeed"/> from this <see cref="NewsPortal"/>.
    /// </summary>
    public IEnumerable<RssFeedItem> RssFeeds { get; }
}
