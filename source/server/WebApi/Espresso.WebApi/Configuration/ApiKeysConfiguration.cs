// ApiKeysConfiguration.cs
//
// © 2021 Espresso News. All rights reserved.

using Microsoft.Extensions.Configuration;

namespace Espresso.WebApi.Configuration;

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

    public string AndroidApiKey => _configuration.GetValue<string>("Android");

    public string IosApiKey => _configuration.GetValue<string>("Ios");

    public string WebApiKey => _configuration.GetValue<string>("Web");

    public string ParserApiKey => _configuration.GetValue<string>("Parser");

    public string DevAndroidApiKey => _configuration.GetValue<string>("DevAndroid");

    public string DevIosApiKey => _configuration.GetValue<string>("DevIos");
}
