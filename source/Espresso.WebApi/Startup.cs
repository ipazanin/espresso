﻿using System.IO;
using Espresso.Application.Hubs;
using Espresso.Application.Initialization;
using Espresso.Common.Enums;
using Espresso.WebApi.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Espresso.WebApi.Extensions;
using GraphQL.Server.Ui.Playground;

namespace Espresso.WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        #region Fields
        private readonly IWebApiConfiguration _configuration;
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public Startup(IConfiguration configuration)
        {
            _configuration = new WebApiConfiguration(configuration);
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationServices(_configuration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="memoryCacheInit"></param>
        /// <param name="env"></param>
        /// <param name="configuration"></param>
        public void Configure(
            IApplicationBuilder app,
            IApplicationInit memoryCacheInit,
            IWebHostEnvironment env,
            IWebApiConfiguration configuration
        )
        {
            if (configuration.SpaConfiguration.EnableCors)
            {
                app.UseCors("CustomCorsPolicy");
            }

            memoryCacheInit.InitWebApi().GetAwaiter().GetResult();
            app.UseSecurityHeadersMiddleware(securityHeadersBuilder =>
            {
                securityHeadersBuilder.AddDefaultSecurePolicy();
            });

            app.UseHsts();

            app.UseSwaggerServices(configuration);
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions
            {
                Path = "/graphql-playground",
                GraphQLEndPoint = "/graphql"
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseMvc();
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = Path.Join(env.ContentRootPath, "ClientApp");

                if (
                    _configuration.AppConfiguration.AppEnvironment.Equals(AppEnvironment.Local) &&
                    _configuration.SpaConfiguration.UseSpaProxyServer
                )
                {
                    spa.UseProxyToSpaDevelopmentServer(_configuration.SpaConfiguration.SpaProxyServerUrl);
                }
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
                endpoints.MapHub<ArticlesNotificationHub>("/notifications/articles");
            });
        }
        #endregion
    }
}
