using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.ViewModels.CategoryViewModels;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.Application.CQRS.Categories.Queries.GetCategories
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, GetCategoriesQueryResponse>
    {
        #region Fields
        private readonly IMemoryCache _memoryCache;
        #endregion

        #region Constructors
        public GetCategoriesQueryHandler(
            IMemoryCache memoryCache
        )
        {
            _memoryCache = memoryCache;
        }
        #endregion

        #region Methods
        public Task<GetCategoriesQueryResponse> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = _memoryCache.Get<IEnumerable<Category>>(key: MemoryCacheConstants.CategoryKey);
            var categoryDtos = categories
                .Where(predicate: Category.GetAllCategoriesExceptGeneralExpression().Compile())
                .Select(CategoryViewModel.GetProjection().Compile());

            var response = new GetCategoriesQueryResponse(categoryDtos);

            return Task.FromResult(result: response);
        }
        #endregion
    }
}
