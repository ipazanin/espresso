// GetNewsPortalDetailsQueryResponse.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Domain.Entities;

namespace Espresso.Dashboard.Application.NewsPortals.GetNewsPortalDetails;

/// <summary>
/// Get news portal details query response.
/// </summary>
public class GetNewsPortalDetailsQueryResponse
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetNewsPortalDetailsQueryResponse"/> class.
    /// </summary>
    /// <param name="newsPortal">News portal.</param>
    public GetNewsPortalDetailsQueryResponse(NewsPortal? newsPortal)
    {
        NewsPortalDetails = newsPortal is null ? null : new NewsPortalDetails(newsPortal);
    }

    /// <summary>
    /// Gets <see cref="NewsPortal"/> details.
    /// </summary>
    public NewsPortalDetails? NewsPortalDetails { get; }
}
