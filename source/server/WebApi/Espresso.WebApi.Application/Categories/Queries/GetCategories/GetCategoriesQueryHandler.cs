// GetCategoriesQueryHandler.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.WebApi.Application.Categories.Queries.GetCategories
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, GetCategoriesQueryResponse>
    {
        private readonly IMemoryCache _memoryCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetCategoriesQueryHandler"/> class.
        /// </summary>
        /// <param name="memoryCache"></param>
        public GetCategoriesQueryHandler(
            IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public Task<GetCategoriesQueryResponse> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = _memoryCache.Get<IEnumerable<Category>>(key: MemoryCacheConstants.CategoryKey);
            var categoryDtos = categories
                .Where(predicate: Category.GetAllCategoriesExceptGeneralExpression().Compile())
                .Select(GetCategoriesCategory.GetProjection().Compile());

            var response = new GetCategoriesQueryResponse
            {
                Categories = categoryDtos,
            };

            return Task.FromResult(result: response);
        }
    }
}
