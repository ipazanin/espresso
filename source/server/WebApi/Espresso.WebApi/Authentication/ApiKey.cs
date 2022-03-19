// ApiKey.cs
//
// © 2021 Espresso News. All rights reserved.

namespace Espresso.WebApi.Authentication;

public class ApiKey
{
    public const string MobileAppRole = nameof(MobileAppRole);

    public const string WebAppRole = nameof(WebAppRole);

    public const string ParserRole = nameof(ParserRole);

    public const string DevMobileAppRole = nameof(DevMobileAppRole);

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiKey"/> class.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="role"></param>
    /// <param name="key"></param>
    public ApiKey(
        int id,
        string role,
        string key)
    {
        Id = id;
        Role = role ?? throw new ArgumentNullException(nameof(role));
        Key = key ?? throw new ArgumentNullException(nameof(key));
    }

    public int Id { get; }

    public string Role { get; }

    public string Key { get; }
}
