using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Espresso.Application.ViewModels.CategoryViewModels;
using Espresso.Domain.Entities;

namespace Espresso.Application.CQRS.Configuration.Queries.GetConfiguration
{
    public class GetConfigurationQueryNewsPortalCategoryGroupingViewModel
    {
        #region Properties
        public CategoryViewModel Category { get; private set; }

        public IEnumerable<GetConfigurationQueryNewsPortalViewModel> NewsPortals { get; private set; } = new List<GetConfigurationQueryNewsPortalViewModel>();
        #endregion

        #region Constructors
        private GetConfigurationQueryNewsPortalCategoryGroupingViewModel()
        {
            Category = null!;
        }
        #endregion

        #region Methods
        public static Expression<Func<Category, GetConfigurationQueryNewsPortalCategoryGroupingViewModel>> GetProjection(
            IEnumerable<GetConfigurationQueryNewsPortalViewModel> newsPortals
        )
        {
            return category => new GetConfigurationQueryNewsPortalCategoryGroupingViewModel
            {
                Category = CategoryViewModel.GetProjection().Compile().Invoke(category),
                NewsPortals = newsPortals.Where(newsPortal => newsPortal.CategoryId.Equals(category.Id))
            };
        }
        #endregion
    }
}
