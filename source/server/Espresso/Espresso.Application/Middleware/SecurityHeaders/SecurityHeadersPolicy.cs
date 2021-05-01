using System.Collections.Generic;

namespace Espresso.Application.Middleware.SecurityHeaders
{
    /// <summary>
    /// Security Headers Policy.
    /// </summary>
    public class SecurityHeadersPolicy
    {
        /// <summary>
        /// Headers to set.
        /// </summary>
        public IDictionary<string, string> SetHeaders { get; } = new Dictionary<string, string>();

        /// <summary>
        /// Headers to remove.
        /// </summary>
        public ISet<string> RemoveHeaders { get; } = new HashSet<string>();
    }
}
