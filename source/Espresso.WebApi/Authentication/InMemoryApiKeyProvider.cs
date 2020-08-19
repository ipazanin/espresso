using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Espresso.Common.Constants;
using Espresso.WebApi.Configuration;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.WebApi.Authentication
{
    /// <summary>
    /// 
    /// </summary>
    public class InMemoryApiKeyProvider : IApiKeyProvider
    {
        #region Fields
        private readonly IDictionary<string, ApiKey> _apiKeys;
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public InMemoryApiKeyProvider(
            IWebApiConfiguration configuration
        )
        {
            _apiKeys = new Dictionary<string, ApiKey>
            {
                {
                    configuration.AndroidApiKey,
                    new ApiKey(
                        id: 1,
                        role: ApiKey.MobileAppRole,
                        key: configuration.AndroidApiKey
                    )
                },
                {
                    configuration.IosApiKey,
                    new ApiKey(
                        id: 2,
                        role: ApiKey.MobileAppRole,
                        key: configuration.IosApiKey
                    )
                },
                {
                    configuration.WebApiKey,
                    new ApiKey(
                        id: 3,
                        role: ApiKey.WebAppRole,
                        key: configuration.WebApiKey
                    )
                },
                {
                    configuration.ParserApiKey,
                    new ApiKey(
                        id: 4,
                        role: ApiKey.ParserRole,
                        key: configuration.ParserApiKey
                    )
                },
            };
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="providedApiKey"></param>
        /// <returns></returns>
        public Task<ApiKey> GetApiKey(string providedApiKey)
        {
            _apiKeys.TryGetValue(providedApiKey, out var key);

            return key is null ? throw new ArgumentNullException(nameof(ApiKey)) : Task.FromResult(key);
        }
    }
}
