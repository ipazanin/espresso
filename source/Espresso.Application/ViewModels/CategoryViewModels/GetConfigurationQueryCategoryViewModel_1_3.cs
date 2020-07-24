using System;
using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.Application.ViewModels.CategoryViewModels
{
    public class GetConfigurationQueryCategoryViewModel_1_3
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
        #endregion

        #region Constructors
        private GetConfigurationQueryCategoryViewModel_1_3()
        {
            Name = null!;
            Color = null!;
        }
        #endregion

        #region Methods
        public static Expression<Func<Category, GetConfigurationQueryCategoryViewModel_1_3>> GetProjection()
        {
            return category => new GetConfigurationQueryCategoryViewModel_1_3
            {
                Id = category.Id,
                Name = category.Name,
                Color = category.Color,
            };
        }
        #endregion
    }
}
