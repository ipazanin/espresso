// GetConfigurationNewsPortal_1_3.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Espresso.WebApi.Application.Configuration.Queries.GetConfiguration_1_3
{
#pragma warning disable S101 // Types should be named in PascalCase
    public record GetConfigurationNewsPortal_1_3
#pragma warning restore S101 // Types should be named in PascalCase
    {
        /// <summary>
        /// Gets news Portal ID.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets news Portal Name.
        /// </summary>
        public string Name { get; private set; } = string.Empty;

        public string IconUrl { get; private set; } = string.Empty;

        private GetConfigurationNewsPortal_1_3()
        {
        }

        public static Expression<Func<NewsPortal, GetConfigurationNewsPortal_1_3>> GetProjection()
        {
            return newsPortal => new GetConfigurationNewsPortal_1_3
            {
                Id = newsPortal.Id,
                Name = newsPortal.Name,
                IconUrl = newsPortal.IconUrl,
            };
        }
    }
}
