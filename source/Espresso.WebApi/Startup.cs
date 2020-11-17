using Espresso.Common.Constants;
using Espresso.WebApi.Configuration;
using Microsoft.Extensions.Configuration;

namespace Espresso.WebApi
{
    internal sealed partial class Startup
    {
        #region Constants
        private const string CustomCorsPolicyName = nameof(CustomCorsPolicyName);
        private const string ClientAppStaticFilesDirectory = "ClientApp/build";
        private const string SwaggerDefinitionFileName = "swagger" + FileExtensionConstants.Json;
        private const string ApiDescriptionNamePrefix = "Espresso API";
        private const string SwaggerApiExplorerRoute = "docs";
        private const string SwaggerDocumentDefinitionRoutePrefix = "swagger";
        #endregion

        #region Fields
        private readonly IWebApiConfiguration _webApiConfiguration;
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public Startup(IConfiguration configuration)
        {
            _webApiConfiguration = new WebApiConfiguration(configuration);
        }
        #endregion
    }
}
