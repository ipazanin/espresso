﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using Espresso.Domain.Enums.CategoryEnums;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.WebApi.Application.Configuration.Queries.GetWebConfiguration
{
    public class GetWebConfigurationQueryHandler : IRequestHandler<GetWebConfigurationQuery, GetWebConfigurationQueryResponse>
    {
        #region Fields
        private readonly IMemoryCache _memoryCache;
        #endregion

        #region Constructors
        public GetWebConfigurationQueryHandler(
            IMemoryCache memoryCache
        )
        {
            _memoryCache = memoryCache;
        }
        #endregion

        #region Methods
        public Task<GetWebConfigurationQueryResponse> Handle(GetWebConfigurationQuery request, CancellationToken cancellationToken)
        {
            var newsPortals = _memoryCache.Get<IEnumerable<NewsPortal>>(key: MemoryCacheConstants.NewsPortalKey);
            var categories = _memoryCache.Get<IEnumerable<Category>>(key: MemoryCacheConstants.CategoryKey);
            var regions = _memoryCache.Get<IEnumerable<Region>>(key: MemoryCacheConstants.RegionKey);

            var categoryDtos = categories
                .Where(predicate: Category.GetAllCategoriesExceptGeneralExpression().Compile())
                .Select(selector: GetWebConfigurationCategory.GetProjection().Compile());

            if (request.DeviceType.Equals(DeviceType.WebApp))
            {
                var categoryDtosList = categoryDtos.ToList();
                var sveVijesticategoryDto = new GetWebConfigurationCategory(
                    id: -1,
                    name: "Sve Vijesti",
                    color: "",
                    position: null,
                    categoryType: Domain.Enums.CategoryEnums.CategoryType.General,
                    url: "/"
                );

                categoryDtosList.Insert(0, sveVijesticategoryDto);
                categoryDtos = categoryDtosList;
            }

            var newsPortalIds = newsPortals
                .Where(newsPortal => !newsPortal.CategoryId.Equals((int)CategoryId.Local))
                .Select(newsPortal => newsPortal.Id);

            var response = new GetWebConfigurationQueryResponse(
                categories: categoryDtos,
                newsPortalIds: newsPortalIds
            );

            return Task.FromResult(result: response);
        }
        #endregion
    }
}