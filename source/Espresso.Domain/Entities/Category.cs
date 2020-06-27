using System;
using System.Collections.Generic;
using System.Linq.Expressions;

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

        public ICollection<ArticleCategory> ArticleCategories { get; private set; } = new List<ArticleCategory>();

        public ICollection<RssFeed> RssFeeds { get; private set; } = new List<RssFeed>();

        public static Expression<Func<Category, Category>> Projection => category => category;
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
            string? keyWordsRegexPattern
        )
        {
            Id = id;
            Name = name;
            Color = color;
            KeyWordsRegexPattern = keyWordsRegexPattern;
        }
        #endregion
    }
}
