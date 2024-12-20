﻿// SwaggerServiceCollectionExtensions.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Reflection;
using Espresso.Common.Constants;
using Espresso.WebApi.Configuration;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Espresso.WebApi.Extensions;

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
    public static IServiceCollection AddSwaggerServices(this IServiceCollection services, IWebApiConfiguration configuration)
    {
        _ = services.AddSwaggerGen(options => ConfigureSwagger(options, configuration));

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
        foreach (var apiVersion in configuration.AppConfiguration.ApiVersions)
        {
            options.SwaggerDoc(apiVersion.ToString(), new OpenApiInfo
            {
                Title = "Espresso API",
                Version = apiVersion.ToString(),
                Description = $"Espresso APP Web Api {configuration.AppConfiguration.Version}",
            });
        }

        options.DocInclusionPredicate((version, desc) =>
        {
            var apiVersions = desc
                .ActionDescriptor
                .GetApiVersionModel()
                .DeclaredApiVersions
                .Select(supportedApiVersion => supportedApiVersion.ToString());

            return apiVersions.Any(v => version.Equals(v, StringComparison.Ordinal));
        });
    }

    private static void ConfigureApiSecurity(SwaggerGenOptions options)
    {
        options.AddSecurityDefinition(HttpHeaderConstants.ApiKeyHeaderName, new OpenApiSecurityScheme
        {
            Description = "API Key",
            In = ParameterLocation.Header,
            Name = HttpHeaderConstants.ApiKeyHeaderName,
            Type = SecuritySchemeType.ApiKey,
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
                                Id = HttpHeaderConstants.ApiKeyHeaderName,
                            },
                        },
                        Array.Empty<string>()
                    },
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
