using System.Collections.Generic;

namespace Espresso.WebApi.Middleware
{
    /// <summary>
    /// 
    /// </summary>
    public class SecurityHeadersPolicy
    {
        /// <summary>
        /// 
        /// </summary>
        public IDictionary<string, string> SetHeaders { get; } = new Dictionary<string, string>();

        /// <summary>
        /// 
        /// </summary>
        public ISet<string> RemoveHeaders { get; } = new HashSet<string>();
    }
}
