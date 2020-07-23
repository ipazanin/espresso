using System;
using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.Application.ViewModels.CategoryViewModels
{
    public class CategoryArticleListItem
    {
        #region Properties
        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Color { get; private set; }
        #endregion

        #region Constructors

        private CategoryArticleListItem()
        {
            Name = null!;
            Color = null!;
        }

        public CategoryArticleListItem(int id, string name, string color)
        {
            Id = id;
            Name = name;
            Color = color;
        }

        #endregion

        #region Projections
        public static Expression<Func<Category, CategoryArticleListItem>> GetProjection()
        {
            return category => new CategoryArticleListItem
            {
                Id = category.Id,
                Name = category.Name,
                Color = category.Color
            };
        }
        #endregion

    }
}
