// DashboardConfiguration.cs
//
// © 2022 Espresso News. All rights reserved.

using Microsoft.Extensions.Configuration;

namespace Espresso.Dashboard.Configuration;

public class DashboardConfiguration : IDashboardConfiguration
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DashboardConfiguration"/> class.
    /// </summary>
    /// <param name="configuration"></param>
    public DashboardConfiguration(IConfiguration configuration)
    {
        ApiKeysConfiguration = new ApiKeysConfiguration(configuration.GetSection("ApiKeysConfiguration"));
        AppConfiguration = new AppConfiguration(configuration.GetSection("AppConfiguration"));
        DatabaseConfiguration = new DatabaseConfiguration(configuration.GetSection("DatabaseConfiguration"));
        RabbitMqConfiguration = new RabbitMqConfiguration(configuration.GetSection("RabbitMqConfiguration"));
        SlackHttpClientConfiguration = new HttpClientConfiguration(configuration.GetSection("HttpClientConfiguration:SlackHttpClientConfiguration"));
        SendArticlesHttpClientConfiguration = new HttpClientConfiguration(configuration.GetSection("HttpClientConfiguration:SendArticlesHttpClientConfiguration"));
        LoadRssFeedsHttpClientConfiguration = new HttpClientConfiguration(configuration.GetSection("HttpClientConfiguration:LoadRssFeedsHttpClientConfiguration"));
        ScrapeWebHttpClientConfiguration = new HttpClientConfiguration(configuration.GetSection("HttpClientConfiguration:ScrapeWebHttpClientConfiguration"));
        SystemTextJsonSerializerConfiguration = new SystemTextJsonSerializerConfiguration(configuration.GetSection("SystemTextJsonSerializerConfiguration"));
    }

    public ApiKeysConfiguration ApiKeysConfiguration { get; }

    public AppConfiguration AppConfiguration { get; }

    public DatabaseConfiguration DatabaseConfiguration { get; }

    public RabbitMqConfiguration RabbitMqConfiguration { get; }

    public HttpClientConfiguration SlackHttpClientConfiguration { get; }

    public HttpClientConfiguration SendArticlesHttpClientConfiguration { get; }

    public HttpClientConfiguration LoadRssFeedsHttpClientConfiguration { get; }

    public HttpClientConfiguration ScrapeWebHttpClientConfiguration { get; }

    public SystemTextJsonSerializerConfiguration SystemTextJsonSerializerConfiguration { get; }
}
