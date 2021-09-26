// GetCategoriesCategory.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using Espresso.Domain.Enums.CategoryEnums;
using System;
using System.Linq.Expressions;

namespace Espresso.WebApi.Application.Categories.Queries.GetCategories
{
    public record GetCategoriesCategory
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

        private GetCategoriesCategory()
        {
        }

        public static Expression<Func<Category, GetCategoriesCategory>> GetProjection()
        {
            return category => new GetCategoriesCategory
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
