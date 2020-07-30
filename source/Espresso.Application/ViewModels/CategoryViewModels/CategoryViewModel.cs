using System;
using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.Application.ViewModels.CategoryViewModels
{
    public class CategoryViewModel
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
        #endregion

        #region Constructors
        private CategoryViewModel()
        {
            Name = null!;
            Color = null!;
        }
        #endregion

        #region Methods
        public static Expression<Func<Category, CategoryViewModel>> GetProjection()
        {
            return category => new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                Color = category.Color,
                Position = category.Position,
                CategoryType = category.CategoryType,
            };
        }
        #endregion
    }
}
