﻿// Region.cs
//
// © 2021 Espresso News. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Espresso.Domain.Enums.RegionEnums;
using Espresso.Domain.Infrastructure;

namespace Espresso.Domain.Entities
{
    public class Region : IEntity<int, Region>
    {
        public const int RegionNameHasMaxLength = 100;
        public const int RegionSubtitleHasMaxLength = 100;

        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Subtitle { get; private set; }

        public IEnumerable<NewsPortal> NewsPortals { get; private set; } = new List<NewsPortal>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Region"/> class.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="subtitle"></param>
        public Region(int id, string name, string subtitle)
        {
            Id = id;
            Name = name;
            Subtitle = subtitle;
        }

        public static Expression<Func<Region, object>> GetOrderByRegionNameExpression()
        {
            return region => region.Name;
        }

        public static Expression<Func<Region, bool>> GetAllRegionsExpectGlobalPredicate()
        {
            return region => !region.Id.Equals((int)RegionId.Global);
        }
    }
}