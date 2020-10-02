using System;
using System.Linq.Expressions;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.CategoryEnums;

namespace Espresso.WebApi.Application.Articles.Queries.GetLatestArticles
{
    public record GetLatestArticlesCategory
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
        public static Expression<Func<Category, GetLatestArticlesCategory>> GetProjection()
        {
            return category => new GetLatestArticlesCategory
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
