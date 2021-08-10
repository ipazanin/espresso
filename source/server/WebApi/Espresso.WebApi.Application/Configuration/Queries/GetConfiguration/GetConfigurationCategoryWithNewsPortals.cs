﻿// GetConfigurationCategoryWithNewsPortals.cs
//
// © 2021 Espresso News. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Espresso.Domain.Entities;
using Espresso.Domain.Enums.CategoryEnums;

namespace Espresso.WebApi.Application.Configuration.Queries.GetConfiguration
{
    public record GetConfigurationCategoryWithNewsPortals
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

        public int? Position { get; private set; }

        public CategoryType CategoryType { get; private set; }

        public IEnumerable<GetConfigurationNewsPortal> NewsPortals { get; private set; } = new List<GetConfigurationNewsPortal>();

        private GetConfigurationCategoryWithNewsPortals()
        {
        }

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
                    ),
            };
        }
    }
}