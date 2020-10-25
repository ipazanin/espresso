using Espresso.WebApi.Application.NewsPortals.Queries.GetNewsPortals;
using Espresso.WebApi.Application.Initialization;
using Espresso.WebApi.Configuration;
using Espresso.WebApi.Filters;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Espresso.WebApi.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ApplicationServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="webApiConfiguration"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            IWebApiConfiguration webApiConfiguration
        )
        {
            services.AddMemoryCache();
            services.AddTransient<IWebApiInit, WebApiInit>();
            services.AddHttpClient();
            services.AddControllers()
                .AddJsonOptions(jsonOptions =>
                {
                    jsonOptions.JsonSerializerOptions.MaxDepth = 100;
                });

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(CustomExceptionFilterAttribute));
                options.EnableEndpointRouting = false;
            }).SetCompatibilityVersion(CompatibilityVersion.Latest)
                .AddFluentValidation(
                    fluentValidatorConfiguration =>
                        fluentValidatorConfiguration
                            .RegisterValidatorsFromAssemblyContaining<GetNewsPortalsQueryValidator>()
                );

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            services.AddHealthChecks();
            services.AddSignalR();

            services.AddGraphQlServices();
            services.AddApiVersioningServices();
            services.AddAppConfiguration();
            services.AddServices(webApiConfiguration);
            services.AddAuthServices();
            services.AddMediatRServices();
            services.AddDatabaseServices(webApiConfiguration);
            services.AddSwaggerServices(webApiConfiguration);

            if (webApiConfiguration.SpaConfiguration.EnableCors)
            {
                services.AddCors(
                    setupAction: o => o.AddPolicy(
                         name: "CustomCorsPolicy",
                         configurePolicy: builder =>
                         {
                             builder.AllowAnyOrigin()
                                    .AllowAnyMethod()
                                    .AllowAnyHeader();
                         }
                     )
                );
            }

            services.AddCronJobs(webApiConfiguration);

            return services;
        }
    }
}
