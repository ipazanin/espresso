using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.CategoryEnums;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.WebApi.Application.NewsPortals.Queries.GetNewsPortals_1_3
{
    public class GetNewsPortalsQueryHandler_1_3 : IRequestHandler<GetNewsPortalsQuery_1_3, GetNewsPortalsQueryResponse_1_3>
    {
        #region Fields
        private readonly IMemoryCache _memoryCache;
        #endregion

        #region Constructors
        public GetNewsPortalsQueryHandler_1_3(
            IMemoryCache memoryCache
        )
        {
            _memoryCache = memoryCache;
        }
        #endregion

        #region Methods
        public Task<GetNewsPortalsQueryResponse_1_3> Handle(GetNewsPortalsQuery_1_3 request, CancellationToken cancellationToken)
        {
            var newsPortals = _memoryCache.Get<IEnumerable<NewsPortal>>(key: MemoryCacheConstants.NewsPortalKey);
            var newsPortalDtos = newsPortals
                .OrderBy(keySelector: newsPortal => newsPortal.Name)
                .Where(predicate: newsPortal => !newsPortal.CategoryId.Equals((int)CategoryId.Local))
                .Select(selector: GetNewsPortalsNewsPortal_1_3.GetProjection().Compile());

            var response = new GetNewsPortalsQueryResponse_1_3(newsPortalDtos);

            return Task.FromResult(result: response);
        }
        #endregion
    }
}
