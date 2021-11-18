// GetConfigurationQueryHandler_1_3.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.CategoryEnums;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.WebApi.Application.Configuration.Queries.GetConfiguration_1_3
{
#pragma warning disable S101 // Types should be named in PascalCase
    public class GetConfigurationQueryHandler_1_3 : IRequestHandler<GetConfigurationQuery_1_3, GetConfigurationQueryResponse_1_3>
#pragma warning restore S101 // Types should be named in PascalCase
    {
        private readonly IMemoryCache _memoryCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetConfigurationQueryHandler_1_3"/> class.
        /// </summary>
        /// <param name="memoryCache"></param>
        public GetConfigurationQueryHandler_1_3(
            IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public Task<GetConfigurationQueryResponse_1_3> Handle(GetConfigurationQuery_1_3 request, CancellationToken cancellationToken)
        {
            var newsPortals = _memoryCache.Get<IEnumerable<NewsPortal>>(key: MemoryCacheConstants.NewsPortalKey);
            var categories = _memoryCache.Get<IEnumerable<Category>>(key: MemoryCacheConstants.CategoryKey);

            var newsPortalDtos = newsPortals
                .OrderBy(keySelector: newsPortal => newsPortal.Name)
                .Where(predicate: newsPortal => !newsPortal.CategoryId.Equals((int)CategoryId.Local) && newsPortal.IsEnabled)
                .Select(selector: GetConfigurationNewsPortal_1_3.GetProjection().Compile());

            var categoryDtos = categories
                .Where(predicate: Category.GetAllCategoriesExceptGeneralExpression().Compile())
                .Where(predicate: category => !category.Id.Equals((int)CategoryId.Local))
                .Select(selector: GetConfigurationCategory_1_3.GetProjection().Compile());

            var response = new GetConfigurationQueryResponse_1_3
            {
                Categories = categoryDtos,
                NewsPortals = newsPortalDtos,
            };

            return Task.FromResult(result: response);
        }
    }
}
