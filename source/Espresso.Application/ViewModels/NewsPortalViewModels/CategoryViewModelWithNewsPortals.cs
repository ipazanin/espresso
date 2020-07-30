using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Espresso.Application.CQRS.Configuration.Queries.GetConfiguration;
using Espresso.Domain.Entities;

namespace Espresso.Application.ViewModels.NewsPortalViewModels
{
    public class CategoryViewModelWithNewsPortals
    {
        #region Properties
        /// <summary>
        /// Category ID
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Category Name
        /// </summary>
        public string Name { get; private set; }

        public string Color { get; private set; }

        public int? Position { get; private set; }

        public CategoryType CategoryType { get; private set; }

        public IEnumerable<GetConfigurationQueryNewsPortalViewModel> NewsPortals { get; private set; } = new List<GetConfigurationQueryNewsPortalViewModel>();
        #endregion

        #region Constructors
        private CategoryViewModelWithNewsPortals()
        {
            Name = null!;
            Color = null!;
        }
        #endregion

        #region Methods
        public static Expression<Func<Category, CategoryViewModelWithNewsPortals>> GetProjection()
        {
            return category => new CategoryViewModelWithNewsPortals
            {
                Id = category.Id,
                Name = category.Name,
                Color = category.Color,
                Position = category.Position,
                CategoryType = category.CategoryType,
                NewsPortals = category.NewsPortals.Select(GetConfigurationQueryNewsPortalViewModel.GetProjection().Compile())
            };
        }
        #endregion
    }
}
