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

        public static Expression<Func<Category, CategoryDto>> Projection => category => new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Color = category.Color,
            KeyWordsRegexPattern = category.KeyWordsRegexPattern
        };
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

        public CategoryDto(int id, string name, string color, string keyWordsRegexPattern)
        {
            Id = id;
            Name = name;
            Color = color;
            KeyWordsRegexPattern = keyWordsRegexPattern;
        }
        #endregion

        #region Methods
        #endregion
    }
}
