﻿// GetConfigurationCategory_1_3.cs
//
// © 2021 Espresso News. All rights reserved.

using System;
using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.WebApi.Application.Configuration.Queries.GetConfiguration_1_3
{
    public record GetConfigurationCategory_1_3
    {
        /// <summary>
        /// Gets category ID.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets category Name.
        /// </summary>
        public string Name { get; private set; } = string.Empty;

        public string Color { get; private set; } = string.Empty;

        private GetConfigurationCategory_1_3()
        {
        }

        public static Expression<Func<Category, GetConfigurationCategory_1_3>> GetProjection()
        {
            return category => new GetConfigurationCategory_1_3
            {
                Id = category.Id,
                Name = category.Name,
                Color = category.Color,
            };
        }
    }
}