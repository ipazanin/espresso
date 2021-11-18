// GetNewsPortalsQueryHandler.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.PagingDataTransferObjects;
using Espresso.Persistence.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Dashboard.Application.NewsPortals.GetNewsPortals
{
    public class GetNewsPortalsQueryHandler : IRequestHandler<GetNewsPortalsQuery, GetNewsPortalsQueryResponse>
    {
        private readonly IEspressoDatabaseContext _espressoDatabaseContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetNewsPortalsQueryHandler"/> class.
        /// </summary>
        /// <param name="espressoDatabaseContext"></param>
        public GetNewsPortalsQueryHandler(IEspressoDatabaseContext espressoDatabaseContext)
        {
            _espressoDatabaseContext = espressoDatabaseContext;
        }

        public async Task<GetNewsPortalsQueryResponse> Handle(GetNewsPortalsQuery request, CancellationToken cancellationToken)
        {
            var newsPortals = await _espressoDatabaseContext
                .NewsPortals
                .OrderBy(newsPortal => newsPortal.Name)
                .Skip(request.PagingParameters.Skip())
                .Take(request.PagingParameters.Take())
                .Select(GetNewsPortalsNewsPortal.GetProjection())
                .ToListAsync(cancellationToken: cancellationToken);

            var newsPortalsCount = await _espressoDatabaseContext
                .NewsPortals
                .CountAsync(cancellationToken: cancellationToken);

            var response = new GetNewsPortalsQueryResponse(
                newsPortals: new PagedList<GetNewsPortalsNewsPortal>(
                    items: newsPortals,
                    pagingMetadata: new PagingMetadata(
                        currentPage: request.PagingParameters.CurrentPage,
                        pageSize: request.PagingParameters.PageSize,
                        totalCount: newsPortalsCount)));

            return response;
        }
    }
}
