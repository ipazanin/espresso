using Espresso.Application.CQRS.NewsPortals.Queries.GetNewsPortals;
using Espresso.Application.Initialization;
using Espresso.Domain.IValidators;
using Espresso.Domain.Validators;
using Espresso.WebApi.Configuration;
using Espresso.WebApi.Filters;
using Espresso.WebApi.GraphQl.Infrastructure;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
            services.AddTransient<IApplicationInit, ApplicationInit>();
            services.AddScoped<IArticleValidator, ArticleValidator>();
            services.AddHttpClient();
            services.AddControllers();

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
            services.AddServices();
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

            return services;
        }
    }
}
