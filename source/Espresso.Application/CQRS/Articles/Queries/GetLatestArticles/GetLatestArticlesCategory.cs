using System;
using System.Linq.Expressions;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.CategoryEnums;

namespace Espresso.Application.CQRS.Articles.Queries.GetLatestArticles
{
    public class GetLatestArticlesCategory
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
        private GetLatestArticlesCategory()
        {
            Name = null!;
            Color = null!;
        }
        #endregion

        #region Methods
        public static Expression<Func<Category, GetLatestArticlesCategory>> GetProjection()
        {
            return category => new GetLatestArticlesCategory
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
