using System;
using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.Application.CQRS.Configuration.Queries.GetConfiguration
{
    public class GetConfigurationCategory
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

        public string Url { get; private set; }
        #endregion

        #region Constructors
        private GetConfigurationCategory()
        {
            Name = null!;
            Color = null!;
            Url = null!;
        }

        public GetConfigurationCategory(
            int id,
            string name,
            string color,
            int? position,
            CategoryType categoryType,
            string url
        )
        {
            Id = id;
            Name = name;
            Color = color;
            Position = position;
            CategoryType = categoryType;
            Url = url;
        }
        #endregion

        #region Methods
        public static Expression<Func<Category, GetConfigurationCategory>> GetProjection()
        {
            return category => new GetConfigurationCategory
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
