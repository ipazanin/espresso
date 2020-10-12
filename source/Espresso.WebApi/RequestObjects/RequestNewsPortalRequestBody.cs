namespace Espresso.WebApi.RequestObjects
{
    /// <summary>
    /// 
    /// </summary>
    public record RequestNewsPortalRequestBody
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string? NewsPortalName { get; init; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string? Email { get; init; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string? Url { get; init; }

    }
}
