using System;
using System.Linq.Expressions;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.CategoryEnums;

namespace Espresso.WebApi.Application.Articles.Queries.GetLatestArticles_1_3
{
    public record GetLatestArticlesCategory_1_3
    {
        #region Properties
        /// <summary>
        /// Category ID
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Category Name
        /// </summary>
        public string Name { get; init; } = "";

        public string Color { get; init; } = "";

        public int? Position { get; init; }

        public CategoryType CategoryType { get; init; }
        #endregion

        #region Methods
        public static Expression<Func<Category, GetLatestArticlesCategory_1_3>> GetProjection()
        {
            return category => new GetLatestArticlesCategory_1_3
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
