using System;

namespace Espresso.WebApi.RequestData.Body
{
    /// <summary>
    /// 
    /// </summary>
    public record SendPushNotificationRequestBody
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public Guid ArticleId { get; init; }

        /// <summary>
        /// 
        /// </summary>
        public string? InternalName { get; init; }

        /// <summary>
        /// 
        /// </summary>
        public string? Title { get; init; }

        /// <summary>
        /// 
        /// </summary>
        public string? Message { get; init; }

        /// <summary>
        /// 
        /// </summary>
        public string? Topic { get; init; }

        /// <summary>
        /// 
        /// </summary>
        public string? ArticleUrl { get; init; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsSoundEnabled { get; init; }
    }
}
