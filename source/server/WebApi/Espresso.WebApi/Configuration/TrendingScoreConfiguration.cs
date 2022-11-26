// TrendingScoreConfiguration.cs
//
// © 2022 Espresso News. All rights reserved.

using Microsoft.Extensions.Configuration;

namespace Espresso.WebApi.Configuration;

/// <summary>
///
/// </summary>
public class TrendingScoreConfiguration
{
    private readonly IConfigurationSection _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="TrendingScoreConfiguration"/> class.
    /// </summary>
    /// <param name="configuration"></param>
    public TrendingScoreConfiguration(IConfigurationSection configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    ///
    /// </summary>
    public int HalfOfMaxTrendingScoreValue => _configuration.GetValue<int>("HalfOfMaxTrendingScoreValue");

    /// <summary>
    ///
    /// </summary>
    public decimal AgeWeight => _configuration.GetValue<decimal>("AgeWeight");
}
