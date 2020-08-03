using Espresso.Common.Constants;
using Espresso.WebApi.Configuration;
using Microsoft.AspNetCore.Builder;

namespace Espresso.WebApi.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class SwaggerApplicationBuilderExtensions
    {
        #region Constants
        private const string SwaggerDefinitionFileName = "swagger" + FileExtensionConstants.Json;
        private const string ApiDescriptionNamePrefix = "Espresso API";
        private const string SwaggerApiExplorerRoute = "docs";
        private const string SwaggerDocumentDefinitionRoutePrefix = "swagger";
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwaggerServices(this IApplicationBuilder app, IWebApiConfiguration configuration)
        {
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(
                    url: $"/{SwaggerDocumentDefinitionRoutePrefix}/{configuration.EspressoWebApiCurrentVersion}/{SwaggerDefinitionFileName}",
                    name: $"{ApiDescriptionNamePrefix} {configuration.EspressoWebApiCurrentVersion}"
                );
                options.SwaggerEndpoint(
                    url: $"/{SwaggerDocumentDefinitionRoutePrefix}/{configuration.EspressoWebApiVersion_1_3}/{SwaggerDefinitionFileName}",
                    name: $"{ApiDescriptionNamePrefix} {configuration.EspressoWebApiVersion_1_3}"
                );
                options.SwaggerEndpoint(
                    url: $"/{SwaggerDocumentDefinitionRoutePrefix}/{configuration.EspressoWebApiVersion_1_2}/{SwaggerDefinitionFileName}",
                    name: $"{ApiDescriptionNamePrefix} {configuration.EspressoWebApiVersion_1_2}"
                );
                options.RoutePrefix = SwaggerApiExplorerRoute;
            });

            return app;
        }
        #endregion
    }
}
