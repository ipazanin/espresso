using System;
using Microsoft.AspNetCore.Mvc;

namespace Espresso.WebApi.RequestData.Query
{
    /// <summary>
    /// 
    /// </summary>
    public class PaginationParameters
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [FromQuery(Name = "take")]
        public int Take { get; set; } = 20;

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [FromQuery(Name = "skip")]
        public int Skip { get; set; } = 0;
    }
}
