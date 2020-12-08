using System.Collections.Generic;

namespace Espresso.WebApi.Application.Categories.Queries.GetCategories
{
    public record GetCategoriesQueryResponse
    {
        #region Properties
        public IEnumerable<GetCategoriesCategory> Categories { get; init; } = new List<GetCategoriesCategory>();
        #endregion
    }
}
