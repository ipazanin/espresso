using System.Collections.Generic;
using System.IO;
using Espresso.WebApi.Application.Hubs;
using Espresso.WebApi.Application.Initialization;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using Espresso.WebApi.Extensions;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Espresso.Common.Constants;

namespace Espresso.WebApi
{
    internal sealed partial class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="memoryCacheInit"></param>
        /// <param name="env"></param>
        public void Configure(
            IApplicationBuilder app,
            IWebApiInit memoryCacheInit,
            IWebHostEnvironment env
        )
        {
            memoryCacheInit.InitWebApi().GetAwaiter().GetResult();

            app.UseSecurityHeadersMiddleware(securityHeadersBuilder =>
            {
                securityHeadersBuilder.AddDefaultSecurePolicy();
            });

            if (_webApiConfiguration.SpaConfiguration.EnableCors)
            {
                app.UseCors(CustomCorsPolicyName);
            }


            app.UseHsts();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var apiVersion in _webApiConfiguration.AppConfiguration.ApiVersions)
                {
                    options.SwaggerEndpoint(
                        url: $"/{SwaggerDocumentDefinitionRoutePrefix}/{apiVersion}/{SwaggerDefinitionFileName}",
                        name: $"{ApiDescriptionNamePrefix} {apiVersion}"
                    );
                }
                options.RoutePrefix = SwaggerApiExplorerRoute;
            });

            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions
            {
                Path = "/graphql-playground",
                GraphQLEndPoint = "/graphql",
                Headers = new Dictionary<string, object>
                {
                  { HttpHeaderConstants.ApiKeyHeaderName, "" },
                  { HttpHeaderConstants.ApiVersionHeaderName, _webApiConfiguration.AppConfiguration.ApiVersion.ToString() },
                  { HttpHeaderConstants.DeviceTypeHeaderName, DeviceType.WebApp },
                  { HttpHeaderConstants.VersionHeaderName, "1.0.0" }
                },
                EditorReuseHeaders = true,
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = Path.Join(env.ContentRootPath, "ClientApp");

                if (
                    _webApiConfiguration.AppConfiguration.AppEnvironment.Equals(AppEnvironment.Local) &&
                    _webApiConfiguration.SpaConfiguration.UseSpaProxyServer
                )
                {
                    spa.UseProxyToSpaDevelopmentServer(_webApiConfiguration.SpaConfiguration.SpaProxyServerUrl);
                }
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
                endpoints.MapHub<ArticlesNotificationHub>("/notifications/articles");
            });
        }
    }
}
