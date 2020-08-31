using Espresso.Common.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Espresso.WebApi.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class AppVersionConfiguration
    {
        #region Fields

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public string Version =>
            $"{ApiVersionConstants.CurrentMajorVersion}." +
            $"{ApiVersionConstants.CurrentMinorVersion}." +
            $"{ApiVersionConstants.CurrentFixVersion}";

        /// <summary>
        /// 
        /// </summary>
        public ApiVersion EspressoWebApiVersion_1_2 => new ApiVersion(1, 2);

        /// <summary>
        /// 
        /// </summary>
        public ApiVersion EspressoWebApiVersion_1_3 => new ApiVersion(1, 3);

        /// <summary>
        /// 
        /// </summary>
        public ApiVersion EspressoWebApiCurrentVersion =>
            new ApiVersion(
                majorVersion: ApiVersionConstants.CurrentMajorVersion,
                minorVersion: ApiVersionConstants.CurrentMinorVersion
            );
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public AppVersionConfiguration()
        {
        }
        #endregion
    }
}
