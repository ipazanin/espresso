using System;
using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.Dashboard.Application.NewsPortals.GetNewsPortals
{
    public class GetNewsPortalsCategory
    {
        #region Properties
        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Color { get; private set; }
        #endregion Properties

        #region Constructors
        private GetNewsPortalsCategory()
        {
            Name = null!;
            Color = null!;
        }
        #endregion Constructors

        #region Methods
        public static Expression<Func<Category, GetNewsPortalsCategory>> GetProjection()
        {
            return category => new GetNewsPortalsCategory
            {
                Id = category.Id,
                Name = category.Name,
                Color = category.Color,
            };
        }
        #endregion Methods
    }
}
