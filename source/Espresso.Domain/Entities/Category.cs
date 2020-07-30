﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Espresso.Domain.Enums.CategoryEnums;
using Espresso.Domain.Infrastructure;

namespace Espresso.Domain.Entities
{
    public class Category : IEntity<int, Category>
    {
        #region Properties
        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Color { get; private set; }

        public string? KeyWordsRegexPattern { get; private set; }

        public int? SortIndex { get; private set; }

        public int? Position { get; private set; }

        public CategoryType CategoryType { get; private set; }

        public ICollection<ArticleCategory> ArticleCategories { get; private set; } = new List<ArticleCategory>();

        public ICollection<RssFeed> RssFeeds { get; private set; } = new List<RssFeed>();

        public ICollection<NewsPortal> NewsPortals { get; private set; } = new List<NewsPortal>();
        #endregion

        #region Constructors
        /// <summary>
        /// ORM Constructor
        /// </summary>
        private Category()
        {
            Name = null!;
            Color = null!;
        }

        public Category(
            int id,
            string name,
            string color,
            string? keyWordsRegexPattern,
            int? sortIndex,
            int? position,
            CategoryType categoryType
        )
        {
            Id = id;
            Name = name;
            Color = color;
            KeyWordsRegexPattern = keyWordsRegexPattern;
            SortIndex = sortIndex;
            Position = position;
            CategoryType = categoryType;
        }
        #endregion

        #region Methods
        public static Expression<Func<Category, bool>> GetAllCategoriesExceptGeneralExpression()
        {
            return category => !category.Id.Equals((int)CategoryId.General);
        }

        public static Expression<Func<Category, bool>> GetAllCategoriesExceptLocalExpression()
        {
            return category => !category.Id.Equals((int)CategoryId.Local);
        }
        #endregion
    }
}