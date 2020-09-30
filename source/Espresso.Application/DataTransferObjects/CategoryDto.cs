using System;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.CategoryEnums;

namespace Espresso.Application.DataTransferObjects
{
    public record CategoryDto
    {
        #region Properties
        public int Id { get; init; }

        public string Name { get; init; }

        public string Color { get; init; }

        public string? KeyWordsRegexPattern { get; init; }

        public int? SortIndex { get; init; }

        public int? Position { get; init; }

        public CategoryType CategoryType { get; init; }

        public string Url { get; init; }
        #endregion

        #region Contructors
        /// <summary>
        /// Used by JSON serializer
        /// </summary>
        // [JsonConstructor]
        public CategoryDto()
        {
            Name = null!;
            Color = null!;
            KeyWordsRegexPattern = null!;
            Url = null!;
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
                CategoryType = category.CategoryType,
                Url = category.Url
            };
        }
        #endregion
    }
}
