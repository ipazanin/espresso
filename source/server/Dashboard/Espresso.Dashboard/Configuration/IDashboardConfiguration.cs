// IDashboardConfiguration.cs
//
// © 2022 Espresso News. All rights reserved.

namespace Espresso.Dashboard.Configuration;

public interface IDashboardConfiguration
{
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
