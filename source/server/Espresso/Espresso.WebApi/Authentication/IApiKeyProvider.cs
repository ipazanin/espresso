using System.Threading.Tasks;

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
        public Task<ApiKey> GetApiKey(string providedApiKey);
    }
}
