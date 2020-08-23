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
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            IWebApiConfiguration configuration
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
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
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
            services.AddPersistentServices(configuration);
            services.AddSwaggerServices(configuration);

            return services;
        }
    }
}
