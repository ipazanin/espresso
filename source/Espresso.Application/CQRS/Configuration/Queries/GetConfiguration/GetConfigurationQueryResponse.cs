using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Espresso.Application.ViewModels.CategoryViewModels;
using Espresso.Application.ViewModels.NewsPortalViewModels;
using Espresso.Application.ViewModels.RegionViewModels;

namespace Espresso.Application.CQRS.Configuration.Queries.GetConfiguration
{
    public class GetConfigurationQueryResponse
    {
        public IEnumerable<CategoryViewModelWithNewsPortals> CategoryViewModelWithNewsPortals { get; }

        public IEnumerable<CategoryViewModel> Categories { get; }

        public IEnumerable<RegionViewModel> Regions { get; set; }

        public GetConfigurationQueryResponse(
            IEnumerable<CategoryViewModelWithNewsPortals> categoryViewModelWithNewsPortals,
            IEnumerable<CategoryViewModel> categories,
            IEnumerable<RegionViewModel> regions
        )
        {
            this.CategoryViewModelWithNewsPortals = categoryViewModelWithNewsPortals;
            Categories = categories;
            Regions = regions;
        }

        public override string ToString()
        {
            return $"{nameof(Categories)}:{Categories.Count()}, " +
                $"{nameof(CategoryViewModelWithNewsPortals)}:{CategoryViewModelWithNewsPortals.Count()}, " +
                $"{nameof(Regions)}:{Regions.Count()}";
        }
    }
}
