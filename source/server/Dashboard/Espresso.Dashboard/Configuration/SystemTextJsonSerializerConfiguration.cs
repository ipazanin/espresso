// SystemTextJsonSerializerConfiguration.cs
//
// © 2021 Espresso News. All rights reserved.

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
        /// Creates new instance of SystemTextJsonSerializerConfiguration with provided <paramref name="configuration"/>.
        /// </summary>
        /// <param name="configuration"></param>
#pragma warning disable SA1201 // Elements should appear in the correct order
        public SystemTextJsonSerializerConfiguration(IConfigurationSection configuration)
#pragma warning restore SA1201 // Elements should appear in the correct order
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Maps <paramref name="jsonSerializerOptions"/> to defalt <see cref="JsonSerializerOptions"/>.
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
