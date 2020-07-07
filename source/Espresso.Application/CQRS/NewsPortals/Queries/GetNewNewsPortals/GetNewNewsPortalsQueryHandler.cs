using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.CQRS.NewsPortals.Queries.GetNewsPortals;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.Application.CQRS.NewsPortals.Queries.GetNewNewsportals
{
    public class GetNewNewsPortalsQueryHandler : IRequestHandler<GetNewNewsportalsQuery, GetNewNewsPortalsQueryResponse>
    {
        #region Fields
        private readonly IMemoryCache _memoryCache;
        #endregion

        #region Constructors
        public GetNewNewsPortalsQueryHandler(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        #endregion

        #region  Methods
        public Task<GetNewNewsPortalsQueryResponse> Handle(GetNewNewsportalsQuery request, CancellationToken cancellationToken)
        {
            var newsPortals = _memoryCache.Get<IEnumerable<NewsPortal>>(key: MemoryCacheConstants.NewsPortalKey);
            var newsPortalDtos = newsPortals
                .Where(NewsPortal.GetIsNewExpression(request.NewsPortalIds, request.CategoryIds).Compile())
                .OrderBy(keySelector: newsPortal => newsPortal.Name)
                .Select(selector: NewsPortalViewModel.Projection.Compile());

            var response = new GetNewNewsPortalsQueryResponse(newsPortalDtos);
            return Task.FromResult(result: response);
        }
        #endregion
    }
}
