// ApiKeysConfiguration.cs
//
// © 2022 Espresso News. All rights reserved.

using Microsoft.Extensions.Configuration;

namespace Espresso.Dashboard.Configuration;

public class ApiKeysConfiguration
{
    private readonly IConfigurationSection _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiKeysConfiguration"/> class.
    /// </summary>
    /// <param name="configuration"></param>
    public ApiKeysConfiguration(IConfigurationSection configuration)
    {
        _configuration = configuration;
    }

    public string ParserApiKey => _configuration.GetValue<string>("Parser")!;
}
