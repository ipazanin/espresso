// WebApiConfiguration.cs
//
// © 2021 Espresso News. All rights reserved.

using Microsoft.Extensions.Configuration;

namespace Espresso.WebApi.Configuration;

/// <summary>
///
/// </summary>
public class WebApiConfiguration : IWebApiConfiguration
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WebApiConfiguration"/> class.
    /// </summary>
    /// <param name="configuration"></param>
    public WebApiConfiguration(IConfiguration configuration)
    {
        AppConfiguration = new AppConfiguration(configuration.GetSection("AppConfiguration"));
        DatabaseConfiguration = new DatabaseConfiguration(configuration.GetSection("DatabaseConfiguration"));
        ApiKeysConfiguration = new ApiKeysConfiguration(configuration.GetSection("ApiKeysConfiguration"));
        SpaConfiguration = new SpaConfiguration(configuration.GetSection("SpaConfiguration"));
        TrendingScoreConfiguration = new TrendingScoreConfiguration(configuration.GetSection("TrendingScoreConfiguration"));
        RabbitMqConfiguration = new RabbitMqConfiguration(configuration.GetSection("RabbitMqConfiguration"));
        SlackHttpClientConfiguration = new HttpClientConfiguration(configuration.GetSection("HttpClientConfiguration:SlackHttpClientConfiguration"));
        SystemTextJsonSerializerConfiguration = new SystemTextJsonSerializerConfiguration(configuration.GetSection("SystemTextJsonSerializerConfiguration"));
    }

    /// <summary>
    ///
    /// </summary>
    public AppConfiguration AppConfiguration { get; }

    /// <summary>
    ///
    /// </summary>
    public DatabaseConfiguration DatabaseConfiguration { get; }

    /// <summary>
    ///
    /// </summary>
    public ApiKeysConfiguration ApiKeysConfiguration { get; }

    /// <summary>
    ///
    /// </summary>
    public SpaConfiguration SpaConfiguration { get; }

    /// <summary>
    ///
    /// </summary>
    public TrendingScoreConfiguration TrendingScoreConfiguration { get; }

    /// <summary>
    ///
    /// </summary>
    public RabbitMqConfiguration RabbitMqConfiguration { get; }

    /// <summary>
    ///
    /// </summary>
    public HttpClientConfiguration SlackHttpClientConfiguration { get; }

    /// <summary>
    ///
    /// </summary>
    public SystemTextJsonSerializerConfiguration SystemTextJsonSerializerConfiguration { get; }
}
