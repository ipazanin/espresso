using System.Collections.Generic;
using System.Linq;

namespace Espresso.WebApi.Application.Categories.Queries.GetCategories
{
    public class GetCategoriesQueryResponse
    {
        #region Properties
        public IEnumerable<GetCategoriesCategory> Categories { get; }
        #endregion

        #region Constructors
        public GetCategoriesQueryResponse(IEnumerable<GetCategoriesCategory> categories)
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
