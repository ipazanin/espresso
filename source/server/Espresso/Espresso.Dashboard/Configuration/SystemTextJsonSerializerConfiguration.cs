// SystemTextJsonSerializerConfiguration.cs
//
// © 2021 Espresso News. All rights reserved.

using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace Espresso.Dashboard.Configuration
{
    /// <summary>
    /// <see cref="JsonSerializer"/> Configuration.
    /// </summary>
    public class SystemTextJsonSerializerConfiguration
    {
        private readonly IConfigurationSection _configuration;

        /// <summary>
        /// Gets json Serializer Options.
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
        /// Creates new instance of SystemTextJsonSerializerConfiguration with provided <paramref name="configuration"/>.
        /// </summary>
        /// <param name="configuration"></param>
        public SystemTextJsonSerializerConfiguration(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Maps <paramref name="jsonSerializerOptions"/> to defalt <see cref="JsonSerializerOptions"/>.
        /// </summary>
        /// <param name="jsonSerializerOptions"></param>
        public void MapJsonSerializerOptionsToDefaultOptions(
            JsonSerializerOptions jsonSerializerOptions
        )
        {
            jsonSerializerOptions.PropertyNameCaseInsensitive = JsonSerializerOptions.PropertyNameCaseInsensitive;
            jsonSerializerOptions.PropertyNamingPolicy = JsonSerializerOptions.PropertyNamingPolicy;
            jsonSerializerOptions.AllowTrailingCommas = JsonSerializerOptions.AllowTrailingCommas;
            jsonSerializerOptions.MaxDepth = JsonSerializerOptions.MaxDepth;
            jsonSerializerOptions.ReadCommentHandling = JsonSerializerOptions.ReadCommentHandling;
        }
    }
}
