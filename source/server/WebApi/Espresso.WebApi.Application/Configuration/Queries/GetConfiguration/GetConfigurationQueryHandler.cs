// GetConfigurationQueryHandler.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using Espresso.Domain.Infrastructure;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.WebApi.Application.Configuration.Queries.GetConfiguration
{
    public class GetConfigurationQueryHandler : IRequestHandler<GetConfigurationQuery, GetConfigurationQueryResponse>
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ISettingProvider _settingProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetConfigurationQueryHandler"/> class.
        /// </summary>
        /// <param name="memoryCache"></param>
        /// <param name="settingProvider"></param>
        public GetConfigurationQueryHandler(IMemoryCache memoryCache, ISettingProvider settingProvider)
        {
            _memoryCache = memoryCache;
            _settingProvider = settingProvider;
        }

        public Task<GetConfigurationQueryResponse> Handle(GetConfigurationQuery request, CancellationToken cancellationToken)
        {
            var categories = _memoryCache.Get<IEnumerable<Category>>(key: MemoryCacheConstants.CategoryKey);
            var regions = _memoryCache.Get<IEnumerable<Region>>(key: MemoryCacheConstants.RegionKey);

            var categoryDtos = categories
                .Where(predicate: Category.GetAllCategoriesExceptGeneralExpression().Compile())
                .Select(selector: GetConfigurationCategory.GetProjection().Compile());

            var categoriesWithNewsPortals = categories
                .OrderBy(category => category.SortIndex)
                .Where(predicate: Category.GetAllCategoriesExceptLocalExpression().Compile())
                .Select(
                    selector: GetConfigurationCategoryWithNewsPortals
                        .GetProjection(maxAgeOfNewNewsPortal: _settingProvider.LatestSetting.NewsPortalSetting.MaxAgeOfNewNewsPortal)
                        .Compile())
                .Where(grouping => grouping.NewsPortals.Any());

            var regionGroupedNewsPortals = regions
                .OrderBy(keySelector: Region.GetOrderByRegionNameExpression().Compile())
                .Where(predicate: Region.GetAllRegionsExpectGlobalPredicate().Compile())
                .Select(
                    selector: GetConfigurationRegion
                        .GetProjection(
                            maxAgeOfNewNewsPortal: _settingProvider.LatestSetting.NewsPortalSetting.MaxAgeOfNewNewsPortal)
                        .Compile());

            var response = new GetConfigurationQueryResponse
            {
                CategoriesWithNewsPortals = categoriesWithNewsPortals,
                Categories = categoryDtos,
                Regions = regionGroupedNewsPortals,
            };

            return Task.FromResult(result: response);
        }
    }
}
