using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.CategoryEnums;

namespace Espresso.Application.CQRS.Configuration.Queries.GetConfiguration
{
    public class GetConfigurationCategoryWithNewsPortals
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

        public IEnumerable<GetConfigurationNewsPortal> NewsPortals { get; private set; } = new List<GetConfigurationNewsPortal>();
        #endregion

        #region Constructors
        private GetConfigurationCategoryWithNewsPortals()
        {
            Name = null!;
            Color = null!;
        }
        #endregion

        #region Methods
        public static Expression<Func<Category, GetConfigurationCategoryWithNewsPortals>> GetProjection(TimeSpan maxAgeOfNewNewsPortal)
        {
            return category => new GetConfigurationCategoryWithNewsPortals
            {
                Id = category.Id,
                Name = category.Name,
                Color = category.Color,
                Position = category.Position,
                CategoryType = category.CategoryType,
                NewsPortals = category.NewsPortals
                    .Select(
                        GetConfigurationNewsPortal
                            .GetProjection(maxAgeOfNewNewsPortal)
                            .Compile()
                    )
            };
        }
        #endregion
    }
}
