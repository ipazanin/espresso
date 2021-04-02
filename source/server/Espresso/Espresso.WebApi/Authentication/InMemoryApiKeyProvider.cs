﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Espresso.WebApi.Configuration;

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
                    configuration.ApiKeysConfiguration.AndroidApiKey,
                    new ApiKey(
                        id: 1,
                        role: ApiKey.MobileAppRole,
                        key: configuration.ApiKeysConfiguration.AndroidApiKey
                    )
                },
                {
                    configuration.ApiKeysConfiguration.IosApiKey,
                    new ApiKey(
                        id: 2,
                        role: ApiKey.MobileAppRole,
                        key: configuration.ApiKeysConfiguration.IosApiKey
                    )
                },
                {
                    configuration.ApiKeysConfiguration.WebApiKey,
                    new ApiKey(
                        id: 3,
                        role: ApiKey.WebAppRole,
                        key: configuration.ApiKeysConfiguration.WebApiKey
                    )
                },
                {
                    configuration.ApiKeysConfiguration.ParserApiKey,
                    new ApiKey(
                        id: 4,
                        role: ApiKey.ParserRole,
                        key: configuration.ApiKeysConfiguration.ParserApiKey
                    )
                },
                {
                    configuration.ApiKeysConfiguration.DevAndroidApiKey,
                    new ApiKey(
                        id: 5,
                        role: ApiKey.DevMobileAppRole,
                        key: configuration.ApiKeysConfiguration.DevAndroidApiKey
                    )
                },
                {
                    configuration.ApiKeysConfiguration.DevIosApiKey,
                    new ApiKey(
                        id: 6,
                        role: ApiKey.DevMobileAppRole,
                        key: configuration.ApiKeysConfiguration.DevIosApiKey
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

            return key is null ? throw new ArgumentNullException(paramName: nameof(providedApiKey)) : Task.FromResult(key);
        }
    }
}