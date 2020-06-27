using Espresso.Common.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace Espresso.WebApi.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public interface IWebApiConfiguration : ICommonConfiguration
    {
        /// <summary>
        /// Connection string
        /// </summary>
        public string ConnectionString { get; }

        /// <summary>
        /// Espresso Web API version
        /// </summary>
        public string EspressoWebApiVersion { get; }

        /// <summary>
        /// 
        /// </summary>
        public ApiVersion EspressoWebApiVersion_1_2 { get; }

        /// <summary>
        /// 
        /// </summary>
        public ApiVersion EspressoWebApiVersion_1_3 { get; }
    }
}
