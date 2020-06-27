using System;

using Espresso.Domain.Infrastructure;

namespace Espresso.Domain.Entities
{
    public class ArticleCategory :
        IEntity<Guid, ArticleCategory>
    {
        #region Properties
        public Guid Id { get; private set; }

        public Guid ArticleId { get; private set; }

        public Article? Article { get; private set; }

        public int CategoryId { get; private set; }

        public Category? Category { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// ORM Constructor
        /// </summary>
        private ArticleCategory() { }

        public ArticleCategory(
            Guid id,
            Guid articleId,
            int categoryId,
            Article? article,
            Category? category
        )
        {
            Id = id;
            ArticleId = articleId;
            CategoryId = categoryId;
            Article = article;
            Category = category;
        }
        #endregion

        #region Methods
        public void SetCategory(Category category)
        {
            Category = category;
        }

        #region Object Overrides
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
        #endregion

        #endregion
    }
}
