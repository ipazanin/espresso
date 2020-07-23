using System.Collections.Generic;
using System.Linq;
using Espresso.Application.ViewModels.CategoryViewModels;

namespace Espresso.Application.CQRS.Categories.Queries.GetCategories
{
    public class GetCategoriesQueryResponse
    {
        #region Properties
        public IEnumerable<CategoryViewModel> Categories { get; }
        #endregion

        #region Constructors
        public GetCategoriesQueryResponse(IEnumerable<CategoryViewModel> categories)
        {
            Categories = categories;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{nameof(Categories)}:{Categories.Count()}";
        }
        #endregion
    }
}
