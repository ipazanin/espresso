﻿// IWebApiConfiguration.cs
//
// © 2021 Espresso News. All rights reserved.

namespace Espresso.WebApi.Configuration
{
    /// <summary>
    ///
    /// </summary>
    public interface IWebApiConfiguration
    {
        /// <summary>
        ///
        /// </summary>
        /// <value></value>
        public AppConfiguration AppConfiguration { get; }

        /// <summary>
        ///
        /// </summary>
        /// <value></value>
        public DatabaseConfiguration DatabaseConfiguration { get; }

        /// <summary>
        ///
        /// </summary>
        /// <value></value>
        public ApiKeysConfiguration ApiKeysConfiguration { get; }

        /// <summary>
        ///
        /// </summary>
        /// <value></value>
        public SpaConfiguration SpaConfiguration { get; }

        /// <summary>
        ///
        /// </summary>
        /// <value></value>
        public TrendingScoreConfiguration TrendingScoreConfiguration { get; }

        /// <summary>
        ///
        /// </summary>
        /// <value></value>
        public RabbitMqConfiguration RabbitMqConfiguration { get; }

        /// <summary>
        ///
        /// </summary>
        /// <value></value>
        public HttpClientConfiguration SlackHttpClientConfiguration { get; }

        /// <summary>
        ///
        /// </summary>
        /// <value></value>
        public SystemTextJsonSerializerConfiguration SystemTextJsonSerializerConfiguration { get; }
    }
}
