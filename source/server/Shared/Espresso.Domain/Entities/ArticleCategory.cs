// ArticleCategory.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Infrastructure;
using System;

namespace Espresso.Domain.Entities
{
    public class ArticleCategory :
        IEntity<Guid, ArticleCategory>
    {
        public Guid Id { get; private set; }

        public Guid ArticleId { get; private set; }

        public Article? Article { get; private set; }

        public int CategoryId { get; private set; }

        public Category? Category { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleCategory"/> class.
        /// ORM Constructor.
        /// </summary>
        private ArticleCategory() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleCategory"/> class.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="articleId"></param>
        /// <param name="categoryId"></param>
        /// <param name="article"></param>
        /// <param name="category"></param>
        public ArticleCategory(
            Guid id,
            Guid articleId,
            int categoryId,
            Article? article,
            Category? category)
        {
            Id = id;
            ArticleId = articleId;
            CategoryId = categoryId;
            Article = article;
            Category = category;
        }

        public void SetCategory(Category category)
        {
            Category = category;
        }

        public override bool Equals(object? obj)
        {
            return obj is ArticleCategory category &&
                   ArticleId.Equals(category.ArticleId) &&
                   CategoryId == category.CategoryId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ArticleId, CategoryId);
        }
    }
}
