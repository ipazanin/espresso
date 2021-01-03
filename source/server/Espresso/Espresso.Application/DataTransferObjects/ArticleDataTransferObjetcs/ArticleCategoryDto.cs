using System;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using Espresso.Domain.Entities;

namespace Espresso.Application.DataTransferObjects.ArticleDataTransferObjects
{
    public record ArticleCategoryDto
    {
        #region Properties
        public Guid Id { get; private set; }

        public int CategoryId { get; private set; }
        #endregion

        #region Constructors
        [JsonConstructor]
        public ArticleCategoryDto(
            Guid id,
            int categoryId
        )
        {
            Id = id;
            CategoryId = categoryId;
        }

        private ArticleCategoryDto()
        {
        }
        #endregion

        #region Methods
        public static Expression<Func<ArticleCategory, ArticleCategoryDto>> GetProjection()
        {
            return articleCategory => new ArticleCategoryDto
            {
                Id = articleCategory.Id,
                CategoryId = articleCategory.CategoryId
            };
        }
        #endregion
    }
}