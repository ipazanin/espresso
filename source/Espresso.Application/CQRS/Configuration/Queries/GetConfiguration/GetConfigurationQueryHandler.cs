using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.CQRS.Categories.Queries.GetCategories;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.Application.CQRS.Configuration.Queries.GetConfiguration
{
    public class GetConfigurationQueryHandler : IRequestHandler<GetConfigurationQuery, GetConfigurationQueryResponse>
    {
        #region Fields
        private readonly IMemoryCache _memoryCache;
        #endregion

        #region Constructors
        public GetConfigurationQueryHandler(
            IMemoryCache memoryCache
        )
        {
            _memoryCache = memoryCache;
        }
        #endregion

        #region Methods
        public Task<GetConfigurationQueryResponse> Handle(GetConfigurationQuery request, CancellationToken cancellationToken)
        {
            var newsPortals = _memoryCache.Get<IEnumerable<NewsPortal>>(key: MemoryCacheConstants.NewsPortalKey);
            var categories = _memoryCache.Get<IEnumerable<Category>>(key: MemoryCacheConstants.CategoryKey);

            var newsPortalDtos = newsPortals
                .OrderBy(keySelector: newsPortal => newsPortal.Name)
                .Select(selector: GetConfigurationQueryNewsPortalViewModel.Projection.Compile());

            var categoryDtos = categories
                .Where(predicate: Category.GetAllCategoriesExceptGeneralExpression().Compile())
                .Select(selector: CategoryViewModel.Projection.Compile());

            var response = new GetConfigurationQueryResponse(
                categories: categoryDtos,
                newsPortals: newsPortalDtos
            );

            return Task.FromResult(result: response);
        }
        #endregion
    }
}
