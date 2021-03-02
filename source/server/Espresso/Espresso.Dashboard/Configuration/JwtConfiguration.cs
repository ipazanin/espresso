using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Espresso.Dashboard.Configuration
{
    public class JwtConfiguration
    {
        #region Fields

        private readonly IConfigurationSection _configuration;

        #endregion

        #region Properties

        public IEnumerable<string> Issuers => _configuration.GetSection("Issuers").Get<IEnumerable<string>>();

        public IEnumerable<string> Audiences => _configuration.GetSection("Audiences").Get<IEnumerable<string>>();

        public IEnumerable<JsonWebKey> JsonWebKeys =>
            JsonSerializer.Deserialize<IEnumerable<JsonWebKey>>(
                json: _configuration.GetValue<string>("JsonWebKeys"),
                options: new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            ) ??
            Array.Empty<JsonWebKey>();

        #endregion

        #region Constructors

        public JwtConfiguration(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }

        #endregion
    }
}
