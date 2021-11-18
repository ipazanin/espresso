// GetNewsPortalDetailsQueryHandler.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using Espresso.Persistence.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Dashboard.Application.NewsPortals.GetNewsPortalDetails
{
    /// <summary>
    /// Get <see cref="NewsPortal"/> details query handler.
    /// </summary>
    public class GetNewsPortalDetailsQueryHandler : IRequestHandler<GetNewsPortalDetailsQuery, GetNewsPortalDetailsQueryResponse>
    {
        private readonly IEspressoDatabaseContext _espressoDatabaseContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetNewsPortalDetailsQueryHandler"/> class.
        /// </summary>
        /// <param name="espressoDatabaseContext">Espresso database context.</param>
        public GetNewsPortalDetailsQueryHandler(IEspressoDatabaseContext espressoDatabaseContext)
        {
            _espressoDatabaseContext = espressoDatabaseContext;
        }

        /// <inheritdoc/>
        public async Task<GetNewsPortalDetailsQueryResponse> Handle(GetNewsPortalDetailsQuery request, CancellationToken cancellationToken)
        {
            var newsPortal = await _espressoDatabaseContext
                .NewsPortals
                .Include(newsPortal => newsPortal.Category)
                .Include(newsPortal => newsPortal.Region)
                .Include(newsPortal => newsPortal.RssFeeds)
                .ThenInclude(rssFeed => rssFeed.Category)
                .Include(newsPortal => newsPortal.RssFeeds)
                .ThenInclude(rssFeed => rssFeed.RssFeedCategories)
                .ThenInclude(rssFeedCategory => rssFeedCategory.Category)
                .FirstOrDefaultAsync(
                    predicate: newsPortal => newsPortal.Id == request.NewsPortalId,
                    cancellationToken: cancellationToken);

            return new GetNewsPortalDetailsQueryResponse(newsPortal);
        }
    }
}
