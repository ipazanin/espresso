// SecurityHeadersPolicy.cs
//
// © 2022 Espresso News. All rights reserved.

namespace Espresso.Application.Middleware.SecurityHeaders;

/// <summary>
/// Security Headers Policy.
/// </summary>
public class SecurityHeadersPolicy
{
    /// <summary>
    /// Gets headers to set.
    /// </summary>
    public IDictionary<string, string> SetHeaders { get; } = new Dictionary<string, string>();

    /// <summary>
    /// Gets headers to remove.
    /// </summary>
    public ISet<string> RemoveHeaders { get; } = new HashSet<string>();
}
