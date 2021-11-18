// IApiKeyProvider.cs
//
// © 2021 Espresso News. All rights reserved.

namespace Espresso.WebApi.Authentication
{
    /// <summary>
    ///
    /// </summary>
    public interface IApiKeyProvider
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="providedApiKey"></param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public Task<ApiKey> GetApiKey(string providedApiKey);
    }
}
