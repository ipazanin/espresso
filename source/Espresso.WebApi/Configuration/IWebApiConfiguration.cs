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
        /// <value></value>
        public AppVersionConfiguration AppVersionConfiguration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public AppConfiguration AppConfiguration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public DatabaseConfiguration DatabaseConfiguration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public ApiKeysConfiguration ApiKeysConfiguration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public SpaConfiguration SpaConfiguration { get; }
    }
}
