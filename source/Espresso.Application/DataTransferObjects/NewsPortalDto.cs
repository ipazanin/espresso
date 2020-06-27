using System;
using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.Application.DataTransferObjects
{
    public class NewsPortalDto
    {
        #region Properties
        public int Id { get; set; }

        public string Name { get; set; }

        public string IconUrl { get; set; }

        public static Expression<Func<NewsPortal, NewsPortalDto>> Projection => newsPortal => new NewsPortalDto
        {
            Id = newsPortal.Id,
            Name = newsPortal.Name,
            IconUrl = newsPortal.IconUrl
        };
        #endregion

        #region Constructors
        /// <summary>
        /// Used by JSON serializer
        /// </summary>
        public NewsPortalDto()
        {
            Name = null!;
            IconUrl = null!;
        }
        #endregion

        #region Methods
        #endregion
    }
}
