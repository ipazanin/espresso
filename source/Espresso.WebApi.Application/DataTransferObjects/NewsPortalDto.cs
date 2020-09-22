using System;
using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.WebApi.Application.DataTransferObjects
{
    public class NewsPortalDto
    {
        #region Properties
        public int Id { get; set; }

        public string Name { get; set; }

        public string IconUrl { get; set; }

        public string BaseUrl { get; set; }

        public bool? IsNewOverride { get; set; }

        public DateTime CreatedAt { get; set; }

        public int CategoryId { get; set; }

        public int RegionId { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Used by JSON serializer
        /// </summary>
        private NewsPortalDto()
        {
            Name = null!;
            IconUrl = null!;
            BaseUrl = null!;
        }
        #endregion

        #region Methods

        public static Expression<Func<NewsPortal, NewsPortalDto>> GetProjection()
        {
            return newsPortal => new NewsPortalDto
            {
                Id = newsPortal.Id,
                Name = newsPortal.Name,
                IconUrl = newsPortal.IconUrl,
                BaseUrl = newsPortal.BaseUrl,
                IsNewOverride = newsPortal.IsNewOverride,
                CreatedAt = newsPortal.CreatedAt,
                CategoryId = newsPortal.CategoryId,
                RegionId = newsPortal.RegionId
            };
        }
        #endregion
    }
}
