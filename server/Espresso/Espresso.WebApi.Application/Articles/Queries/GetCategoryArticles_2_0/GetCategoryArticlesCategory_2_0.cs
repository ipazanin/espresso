using System;
using System.Linq.Expressions;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.CategoryEnums;

namespace Espresso.WebApi.Application.Articles.Queries.GetCategoryArticles_2_0
{
    public record GetCategoryArticlesCategory_2_0
    {
        #region Properties
        /// <summary>
        /// Category ID
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Category Name
        /// </summary>
        public string Name { get; private set; } = "";

        public string Color { get; private set; } = "";

        public int? Position { get; private set; }

        public CategoryType CategoryType { get; private set; }
        #endregion

        #region Constructors
        private GetCategoryArticlesCategory_2_0()
        {
        }
        #endregion

        #region Methods
        public static Expression<Func<Category, GetCategoryArticlesCategory_2_0>> GetProjection()
        {
            return category => new GetCategoryArticlesCategory_2_0
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
