using System;
using System.Linq.Expressions;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.CategoryEnums;

namespace Espresso.Application.NewsPortals
{
    public record GetNewsPortalsCategory
    {
        #region Properties
        public int Id { get; private set; }

        public string Name { get; private set; } = "";

        public string Color { get; private set; } = "";

        public string? KeyWordsRegexPattern { get; private set; }

        public int? SortIndex { get; private set; }

        public int? Position { get; private set; }

        public CategoryType CategoryType { get; private set; }
        #endregion

        #region Constructors
        private GetNewsPortalsCategory()
        {
        }
        #endregion

        #region Methods
        public static Expression<Func<Category, GetNewsPortalsCategory>> GetProjection()
        {
            return category => new GetNewsPortalsCategory
            {
                Id = category.Id,
                Name = category.Name,
                Color = category.Color,
                KeyWordsRegexPattern = category.KeyWordsRegexPattern,
                SortIndex = category.SortIndex,
                Position = category.Position,
                CategoryType = category.CategoryType,
            };
        }
        #endregion
    }
}