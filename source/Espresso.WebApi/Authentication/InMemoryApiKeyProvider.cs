using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Espresso.Common.Configuration;
using Espresso.Common.Constants;
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
        /// <param name="memoryCache"></param>
        public InMemoryApiKeyProvider(
            IMemoryCache memoryCache
        )
        {
            var existingApiKeys = memoryCache
                .Get<IEnumerable<string>>(key: MemoryCacheConstants.ApiKeysKey)
                .Select((apiKey, index) => new ApiKey(
                    id: index,
                    owner: "ipazanin",
                    key: apiKey,
                    created: new DateTime(2020, 4, 7)
                ));

            _apiKeys = existingApiKeys.ToDictionary(x => x.Key, x => x);
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

            if (key is null)
            {
                throw new ArgumentNullException(nameof(ApiKey));
            }

            return Task.FromResult(key);
        }
    }
}
