using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace Espresso.Dashboard.Configuration
{
    /// <summary>
    /// <see cref="JsonSerializer"/> Configuration
    /// </summary>
    public class SystemTextJsonSerializerConfiguration
    {
        #region Fields
        private readonly IConfigurationSection _configuration;
        #endregion

        #region Properties
        /// <summary>
        /// Json Serializer Options
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
        #endregion

        #region Constructors
        /// <summary>
        /// Creates new instance of SystemTextJsonSerializerConfiguration with provided <paramref name="configuration"/>
        /// </summary>
        /// <param name="configuration"></param>
        public SystemTextJsonSerializerConfiguration(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Maps <paramref name="jsonSerializerOptions"/> to defalt <see cref="JsonSerializerOptions"/>
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
        #endregion Methods
    }
}
