using System;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using Espresso.Domain.Entities;

namespace Espresso.Application.DataTransferObjects
{
    public record NewsPortalDto
    {
        #region Properties
        public int Id { get; init; }

        public string Name { get; init; }

        public string IconUrl { get; init; }

        public string BaseUrl { get; init; }

        public bool? IsNewOverride { get; init; }

        public DateTime CreatedAt { get; init; }

        public int CategoryId { get; init; }

        public int RegionId { get; init; }
        #endregion

        #region Constructors
        /// <summary>
        /// Used by JSON serializer
        /// </summary>
        // [JsonConstructor]
        public NewsPortalDto()
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
