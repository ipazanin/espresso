// ArticlePaginationParameters.cs
//
// © 2021 Espresso News. All rights reserved.

using Microsoft.AspNetCore.Mvc;
using System;

namespace Espresso.WebApi.RequestData.Query
{
    /// <summary>
    /// 
    /// </summary>
    public class ArticlePaginationParameters : PaginationParameters
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [FromQuery(Name = "firstArticleId")]
        public Guid? FirstArticleId { get; set; } = null;
    }
}
