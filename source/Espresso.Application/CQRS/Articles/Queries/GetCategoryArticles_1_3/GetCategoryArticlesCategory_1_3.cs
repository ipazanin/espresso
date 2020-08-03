﻿using System;
using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.Application.CQRS.Articles.Queries.GetCategoryArticles_1_3
{
    public class GetCategoryArticlesCategory_1_3
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
        private GetCategoryArticlesCategory_1_3()
        {
            Name = null!;
            Color = null!;
        }
        #endregion

        #region Methods
        public static Expression<Func<Category, GetCategoryArticlesCategory_1_3>> GetProjection()
        {
            return category => new GetCategoryArticlesCategory_1_3
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
