using System;
using System.Linq.Expressions;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.CategoryEnums;

namespace Espresso.Application.CQRS.Categories.Queries.GetCategories
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

        public static Expression<Func<Category, CategoryViewModel>> Projection => category => new CategoryViewModel
        {
            Id = category.Id,
            Name = category.Name,
            Color = category.Color
        };
        #endregion

        #region Constructors
        private CategoryViewModel()
        {
            Name = null!;
            Color = null!;
        }
        #endregion

    }
}
