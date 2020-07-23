using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.ViewModels.CategoryViewModels;
using Espresso.Application.ViewModels.NewsPortalViewModels;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.Application.CQRS.Configuration.Queries.GetConfiguration_1_3
{
    public class GetConfigurationQueryHandler : IRequestHandler<GetConfigurationQuery_1_3, GetConfigurationQueryResponse_1_3>
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
        public Task<GetConfigurationQueryResponse_1_3> Handle(GetConfigurationQuery_1_3 request, CancellationToken cancellationToken)
        {
            var newsPortals = _memoryCache.Get<IEnumerable<NewsPortal>>(key: MemoryCacheConstants.NewsPortalKey);
            var categories = _memoryCache.Get<IEnumerable<Category>>(key: MemoryCacheConstants.CategoryKey);

            var newsPortalDtos = newsPortals
                .OrderBy(keySelector: newsPortal => newsPortal.Name)
                .Select(selector: GetConfigurationQueryNewsPortalViewModel_1_3.GetProjection().Compile());

            var categoryDtos = categories
                .Where(predicate: Category.GetAllCategoriesExceptGeneralExpression().Compile())
                .Select(selector: GetConfigurationQueryCategoryViewModel_1_3.GetProjection().Compile());

            var response = new GetConfigurationQueryResponse_1_3(
                categories: categoryDtos,
                newsPortals: newsPortalDtos
            );

            return Task.FromResult(result: response);
        }
        #endregion
    }
}
