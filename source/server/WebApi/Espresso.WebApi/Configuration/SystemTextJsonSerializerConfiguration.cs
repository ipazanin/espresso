﻿// SystemTextJsonSerializerConfiguration.cs
//
// © 2021 Espresso News. All rights reserved.

using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace Espresso.WebApi.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class SystemTextJsonSerializerConfiguration
    {
        private readonly IConfigurationSection _configuration;

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public JsonSerializerOptions JsonSerializerOptions => new()
        {
            AllowTrailingCommas = _configuration.GetValue<bool>("AllowTrailingCommas"),
            MaxDepth = _configuration.GetValue<int>("MaxDepth"),
            PropertyNameCaseInsensitive = _configuration.GetValue<bool>("PropertyNameCaseInsensitive"),
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            ReadCommentHandling = _configuration.GetValue<JsonCommentHandling>("ReadCommentHandling"),
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemTextJsonSerializerConfiguration"/> class.
        /// </summary>
        /// <param name="configuration"></param>
        public SystemTextJsonSerializerConfiguration(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonSerializerOptions"></param>
        public void MapJsonSerializerOptionsToDefaultOptions(
            JsonSerializerOptions jsonSerializerOptions)
        {
            jsonSerializerOptions.PropertyNameCaseInsensitive = JsonSerializerOptions.PropertyNameCaseInsensitive;
            jsonSerializerOptions.PropertyNamingPolicy = JsonSerializerOptions.PropertyNamingPolicy;
            jsonSerializerOptions.AllowTrailingCommas = JsonSerializerOptions.AllowTrailingCommas;
            jsonSerializerOptions.MaxDepth = JsonSerializerOptions.MaxDepth;
            jsonSerializerOptions.ReadCommentHandling = JsonSerializerOptions.ReadCommentHandling;
        }
    }
}
