using System.Collections.Generic;
using System.Linq;
using Espresso.Application.ViewModels.CategoryViewModels;

namespace Espresso.Application.CQRS.Configuration.Queries.GetConfiguration
{
    public class GetConfigurationQueryResponse
    {
        public IEnumerable<GetConfigurationQueryNewsPortalCategoryGroupingViewModel> GroupedNewsPortals { get; }

        public IEnumerable<CategoryViewModel> Categories { get; }

        public GetConfigurationQueryResponse(
            IEnumerable<GetConfigurationQueryNewsPortalCategoryGroupingViewModel> groupedNewsPortals,
            IEnumerable<CategoryViewModel> categories
        )
        {
            GroupedNewsPortals = groupedNewsPortals;
            Categories = categories;
        }

        public override string ToString()
        {
            return $"{nameof(Categories)}:{Categories.Count()}";
        }
    }
}
