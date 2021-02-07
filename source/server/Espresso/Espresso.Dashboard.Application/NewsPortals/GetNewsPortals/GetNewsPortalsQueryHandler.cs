using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.DataTransferObjects.PagingDataTransferObjects;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using Espresso.Persistence.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.Dashboard.Application.NewsPortals.GetNewsPortals
{
    public class GetNewsPortalsQueryHandler : IRequestHandler<GetNewsPortalsQuery, GetNewsPortalsQueryResponse>
    {
        #region Fields
        private readonly IEspressoDatabaseContext _espressoDatabaseContext;
        #endregion Fields

        #region Constructors
        public GetNewsPortalsQueryHandler(IEspressoDatabaseContext espressoDatabaseContext)
        {
            _espressoDatabaseContext = espressoDatabaseContext;
        }
        #endregion Constructors


        #region Methods
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
                        totalCount: newsPortalsCount
                    )
                )
            );

            return response;
        }
        #endregion Methods
    }
}
