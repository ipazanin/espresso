using System;
using System.Linq.Expressions;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.CategoryEnums;

namespace Espresso.WebApi.Application.Configuration.Queries.GetWebConfiguration
{
    public record GetWebConfigurationCategory
    {
        #region Properties
        /// <summary>
        /// Category ID
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Category Name
        /// </summary>
        public string Name { get; init; } = "";

        public string Color { get; init; } = "";

        public int? Position { get; init; }

        public CategoryType CategoryType { get; init; }

        public string Url { get; init; } = "";
        #endregion

        #region Methods
        public static Expression<Func<Category, GetWebConfigurationCategory>> GetProjection()
        {
            return category => new GetWebConfigurationCategory
            {
                Id = category.Id,
                Name = category.Name,
                Color = category.Color,
                Position = category.Position,
                CategoryType = category.CategoryType,
                Url = category.Url
            };
        }
        #endregion
    }
}
