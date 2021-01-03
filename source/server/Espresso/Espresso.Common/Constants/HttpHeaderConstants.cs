namespace Espresso.Common.Constants
{
    public static class HttpHeaderConstants
    {
        #region Custom Espresso Headers
        public const string ApiKeyHeaderName = "espresso-api-key";
        public const string ApiVersionHeaderName = "espresso-api-version";
        public const string VersionHeaderName = "version";
        public const string DeviceTypeHeaderName = "device-type";
        #endregion

        #region Content Type Options
        /// <summary>
        /// Header value for X-Content-Type-Options
        /// </summary>
        public static readonly string ContentTypeOptionsHeader = "X-Content-Type-Options";

        /// <summary>
        /// Disables content sniffing
        /// </summary>
        public static readonly string ContentTypeOptionsNoSniff = "nosniff";
        #endregion

        #region Frame Options
        /// <summary>
        /// The header value for X-Frame-Options
        /// </summary>
        public static readonly string FrameOptionsHeader = "X-Frame-Options";

        /// <summary>
        /// The page cannot be displayed in a frame, regardless of the site attempting to do so.
        /// </summary>
        public static readonly string FrameOptionsDeny = "DENY";

        /// <summary>
        /// The page can only be displayed in a frame on the same origin as the page itself.
        /// </summary>
        public static readonly string FrameOptionsSameOrigin = "SAMEORIGIN";

        /// <summary>
        /// The page can only be displayed in a frame on the specified origin. {0} specifies the format string
        /// </summary>
        public static readonly string FrameOptionsAllowFromUri = "ALLOW-FROM {0}";
        #endregion

        #region Powered By Constants
        /// <summary>
        /// The header value for X-Powered-By
        /// </summary>
        public static readonly string PoweredByHeader = "Server";
        #endregion

        #region Strict Transport Security
        /// <summary>
        /// Header value for Strict-Transport-Security
        /// </summary>
        public static readonly string StrictTransportSecurityHeader = "Strict-Transport-Security";

        /// <summary>
        /// Tells the user-agent to cache the domain in the STS list for the provided number of seconds {0} 
        /// </summary>
        public static readonly string StrictTransportSecurityMaxAge = "max-age={0}";

        /// <summary>
        /// Tells the user-agent to cache the domain in the STS list for the provided number of seconds {0} and include any sub-domains.
        /// </summary>
        public static readonly string StrictTransportSecurityMaxAgeIncludeSubdomains = "max-age={0}; includeSubDomains";

        /// <summary>
        /// Tells the user-agent to remove, or not cache the host in the STS cache.
        /// </summary>
        public static readonly string StrictTransportSecurityNoCache = "max-age=0";
        #endregion

        #region Xss Protection
        /// <summary>
        /// Header value for X-XSS-Protection
        /// </summary>
        public static readonly string XssProtectionHeader = "X-XSS-Protection";

        /// <summary>
        /// Enables the XSS Protections
        /// </summary>
        public static readonly string XssProtectionEnabled = "1";

        /// <summary>
        /// Disables the XSS Protections offered by the user-agent.
        /// </summary>
        public static readonly string XssProtectionDisabled = "0";

        /// <summary>
        /// Enables XSS protections and instructs the user-agent to block the response in the event that script has been inserted from user input, instead of sanitizing.
        /// </summary>
        public static readonly string XssProtectionBlock = "1; mode=block";

        /// <summary>
        /// A partially supported directive that tells the user-agent to report potential XSS attacks to a single URL. Data will be POST'd to the report URL in JSON format. 
        /// {0} specifies the report url, including protocol
        /// </summary>
        public static readonly string XssProtectionReport = "1; report={0}";
        #endregion
    }
}
