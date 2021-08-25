// GetLatestArticlesCategory_1_4.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using Espresso.Domain.Enums.CategoryEnums;
using System;
using System.Linq.Expressions;

namespace Espresso.WebApi.Application.Articles.Queries.GetLatestArticles_1_4
{
    public record GetLatestArticlesCategory_1_4
    {
        /// <summary>
        /// Gets category ID.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets category Name.
        /// </summary>
        public string Name { get; private set; } = string.Empty;

        public string Color { get; private set; } = string.Empty;

        public int? Position { get; private set; }

        public CategoryType CategoryType { get; private set; }

        private GetLatestArticlesCategory_1_4()
        {
        }

        public static Expression<Func<Category, GetLatestArticlesCategory_1_4>> GetProjection()
        {
            return category => new GetLatestArticlesCategory_1_4
            {
                Id = category.Id,
                Name = category.Name,
                Color = category.Color,
                Position = category.Position,
                CategoryType = category.CategoryType,
            };
        }
    }
}
