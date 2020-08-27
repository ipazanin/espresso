using System;
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
        /// 
        /// </summary>
        public ApiVersion EspressoWebApiVersion_1_2 { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ApiVersion EspressoWebApiVersion_1_3 { get; }


        /// <summary>
        /// 
        /// </summary>
        public ApiVersion EspressoWebApiCurrentVersion { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string SpaProxyServerUrl { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public bool UseSpaProxyServer { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public bool EnableCors { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public int NewNewsPortalsPosition { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public TimeSpan MaxAgeOfTrendingArticle { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string AndroidApiKey { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string IosApiKey { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string WebApiKey { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string ParserApiKey { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string DevIosApiKey { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string DevAndroidApiKey { get; }
    }
}
