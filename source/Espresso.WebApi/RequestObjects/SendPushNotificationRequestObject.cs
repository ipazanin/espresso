namespace Espresso.WebApi.RequestObject
{
    /// <summary>
    /// 
    /// </summary>
    public class SendPushNotificationRequestObject
    {
        /// <summary>
        /// 
        /// </summary>
        public string? InternalName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? Topic { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? ArticleUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsSoundEnabled { get; set; }

    }
}
