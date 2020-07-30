using System;
using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.Application.DataTransferObjects
{
    public class CategoryDto
    {
        #region Properties
        public int Id { get; set; }

        public string Name { get; set; }

        public string Color { get; set; }

        public string? KeyWordsRegexPattern { get; set; }

        public int? SortIndex { get; set; }

        public int? Position { get; private set; }

        public CategoryType CategoryType { get; private set; }
        #endregion

        #region Contructors
        /// <summary>
        /// Used by JSON serializer
        /// </summary>
        private CategoryDto()
        {
            Name = null!;
            Color = null!;
            KeyWordsRegexPattern = null!;
        }
        #endregion

        #region Methods
        public static Expression<Func<Category, CategoryDto>> GetProjection()
        {
            return category => new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Color = category.Color,
                KeyWordsRegexPattern = category.KeyWordsRegexPattern,
                SortIndex = category.SortIndex,
                Position = category.Position,
                CategoryType = category.CategoryType
            };
        }
        #endregion
    }
}
