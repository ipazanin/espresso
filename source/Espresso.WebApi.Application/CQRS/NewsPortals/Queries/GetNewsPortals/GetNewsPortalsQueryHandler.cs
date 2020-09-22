using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.CategoryEnums;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.WebApi.Application.CQRS.NewsPortals.Queries.GetNewsPortals
{
    public class GetNewsPortalsQueryHandler : IRequestHandler<GetNewsPortalsQuery, GetNewsPortalsQueryResponse>
    {
        #region Fields
        private readonly IMemoryCache _memoryCache;
        #endregion

        #region Constructors
        public GetNewsPortalsQueryHandler(
            IMemoryCache memoryCache
        )
        {
            _memoryCache = memoryCache;
        }
        #endregion

        #region Methods
        public Task<GetNewsPortalsQueryResponse> Handle(GetNewsPortalsQuery request, CancellationToken cancellationToken)
        {
            var newsPortals = _memoryCache.Get<IEnumerable<NewsPortal>>(key: MemoryCacheConstants.NewsPortalKey);
            var newsPortalDtos = newsPortals
                .OrderBy(keySelector: newsPortal => newsPortal.Name)
                .Where(predicate: newsPortal => !newsPortal.CategoryId.Equals((int)CategoryId.Local))
                .Select(selector: GetNewsPortalsNewsPortal.GetProjection().Compile());

            var response = new GetNewsPortalsQueryResponse(newsPortalDtos);

            return Task.FromResult(result: response);
        }
        #endregion
    }
}
