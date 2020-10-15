using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Espresso.Common.Constants;
using Espresso.WebApi.Configuration;
using Espresso.WebApi.RequestData.Header;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Espresso.WebApi.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class SwaggerServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services, IWebApiConfiguration configuration)
        {
            services.AddSwaggerGen(options => ConfigureSwagger(options, configuration));

            return services;
        }

        private static void ConfigureSwagger(SwaggerGenOptions options, IWebApiConfiguration configuration)
        {
            ConfigureSwaggerDocumentVersions(options, configuration);
            ConfigureApiSecurity(options);
            ConfigureDocumentGenerationPath(options);
        }

        private static void ConfigureSwaggerDocumentVersions(SwaggerGenOptions options, IWebApiConfiguration configuration)
        {
            options.SwaggerDoc(configuration.AppConfiguration.EspressoWebApiCurrentVersion.ToString(), new OpenApiInfo
            {
                Title = $"Espresso API",
                Version = configuration.AppConfiguration.EspressoWebApiCurrentVersion.ToString(),
                Description = "Espresso APP Web Api",
            });

            options.SwaggerDoc(configuration.AppConfiguration.EspressoWebApiVersion_1_3.ToString(), new OpenApiInfo
            {
                Title = $"Espresso API",
                Version = configuration.AppConfiguration.EspressoWebApiVersion_1_3.ToString(),
                Description = "Espresso APP Web Api"
            });

            options.SwaggerDoc(configuration.AppConfiguration.EspressoWebApiVersion_1_2.ToString(), new OpenApiInfo
            {
                Title = $"Espresso API",
                Version = configuration.AppConfiguration.EspressoWebApiVersion_1_2.ToString(),
                Description = "Espresso APP Web Api"
            });

            options.DocInclusionPredicate((version, desc) =>
            {
                var basicInformationHeaders = desc.ParameterDescriptions
                    .FirstOrDefault(
                        parameterDescription => parameterDescription.Name
                            .Equals(
                                nameof(BasicInformationsHeaderParameters),
                                StringComparison.InvariantCultureIgnoreCase
                            )
                    );

                var apiVersions = desc
                    .ActionDescriptor
                    .GetApiVersionModel()
                    .DeclaredApiVersions
                    .Select(supportedApiVersion => supportedApiVersion.ToString());

                return apiVersions.Any(v => version.Equals(v));
            });
        }

        private static void ConfigureApiSecurity(SwaggerGenOptions options)
        {
            options.AddSecurityDefinition(HttpHeaderConstants.ApiKeyHeaderName, new OpenApiSecurityScheme
            {
                Description = "API Key",
                In = ParameterLocation.Header,
                Name = HttpHeaderConstants.ApiKeyHeaderName,
                Type = SecuritySchemeType.ApiKey
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = HttpHeaderConstants.ApiKeyHeaderName,
                            Type = SecuritySchemeType.ApiKey,
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = HttpHeaderConstants.ApiKeyHeaderName
                            },
                        },
                        Array.Empty<string>()
                    }
                });
        }

        private static void ConfigureDocumentGenerationPath(SwaggerGenOptions options)
        {
            // Set the comments path for the Swagger JSON and UI.
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        }
    }
}
