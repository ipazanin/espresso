// IWebApiConfiguration.cs
//
// © 2021 Espresso News. All rights reserved.

namespace Espresso.WebApi.Configuration;

/// <summary>
///
/// </summary>
public interface IWebApiConfiguration
{
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
