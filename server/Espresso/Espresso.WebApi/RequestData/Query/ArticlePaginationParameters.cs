using System;
using Microsoft.AspNetCore.Mvc;

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
