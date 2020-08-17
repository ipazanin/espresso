using System.IO;
using Espresso.Application.CQRS.NewsPortals.Queries.GetNewsPortals;
using Espresso.WebApi.GraphQl.Infrastructure;
using Espresso.Application.Hubs;
using Espresso.Application.Initialization;
using Espresso.Common.Enums;
using Espresso.Domain.IValidators;
using Espresso.Domain.Validators;
using Espresso.WebApi.Configuration;
using Espresso.WebApi.Extensions;
using Espresso.WebApi.Filters;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddAppConfiguration();
            services.AddAppServices();

            #region MemoryCache
            services.AddMemoryCache();
            services.AddTransient<IApplicationInit, ApplicationInit>();
            #endregion

            #region Validators
            services.AddScoped<IArticleValidator, ArticleValidator>();
            #endregion

            services.AddMediatRServices();

            services.AddHttpClient();
            services.AddControllers();

            services.AddApiVersioningServices(_configuration);

            services
                .AddMvc(options =>
                {
                    options.Filters.Add(typeof(CustomExceptionFilterAttribute));
                    options.EnableEndpointRouting = false;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddFluentValidation(fluentValidatorConfiguration => fluentValidatorConfiguration.RegisterValidatorsFromAssemblyContaining<GetNewsPortalsQueryValidator>());

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            services.AddAuthServices();

            #region Health Checks
            services.AddHealthChecks();
            #endregion

            services.AddSignalR();

            services.AddGraphQlServices();

            services.AddSwaggerServices(_configuration);

            services.AddPersistentServices(_configuration);
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
            memoryCacheInit.InitWebApi().GetAwaiter().GetResult();
            app.UseSecurityHeadersMiddleware(securityHeadersBuilder =>
            {
                securityHeadersBuilder.AddDefaultSecurePolicy();
            });

            app.UseHsts();

            app.UseSwaggerServices(configuration);

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
                    _configuration.AppEnvironment.Equals(AppEnvironment.Local) &&
                    !string.IsNullOrEmpty(_configuration.SpaProxyServerUrl)
                )
                {
                    spa.UseProxyToSpaDevelopmentServer(_configuration.SpaProxyServerUrl);
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
