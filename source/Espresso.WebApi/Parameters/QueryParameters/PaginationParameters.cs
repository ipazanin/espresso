using Microsoft.AspNetCore.Mvc;

namespace Espresso.WebApi.QueryParameters
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

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [FromQuery(Name = "minTimestamp")]
        public long? MinTimestamp { get; set; } = null;
    }
}
