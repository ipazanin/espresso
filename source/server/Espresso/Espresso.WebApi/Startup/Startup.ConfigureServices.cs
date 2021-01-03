using System;
using System.Net.Http;
using System.Reflection;
using Espresso.Application.Infrastructure.CronJobsInfrastructure;
using Espresso.Application.Infrastructure.MediatorInfrastructure;
using Espresso.Application.IServices;
using Espresso.Application.Models;
using Espresso.Application.Services;
using Espresso.Application.Utilities;
using Espresso.Common.Constants;
using Espresso.Common.IServices;
using Espresso.Common.Services;
using Espresso.Domain.IServices;
using Espresso.Domain.Services;
using Espresso.ParserDeleter.Application.Constants;
using Espresso.Persistence.Database;
using Espresso.Persistence.IRepositories;
using Espresso.Persistence.Repositories;
using Espresso.WebApi.Application.Initialization;
using Espresso.WebApi.Application.NewsPortals.Queries.GetNewsPortals;
using Espresso.WebApi.Authentication;
using Espresso.WebApi.Extensions;
using Espresso.WebApi.Filters;
using Espresso.WebApi.Jobs.CronJobs;
using Espresso.WebApi.Services;
using FluentValidation.AspNetCore;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Espresso.WebApi.Startup
{
    internal sealed partial class Startup
    {
        /// <summary>
        /// Configures Applications DI IoC Container
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            AddWebApi(services);
            AddEssentials(services);
            AddMediatRServices(services);
            AddApplicationServices(services);
            AddPersistence(services);
            AddJobs(services);

            services.AddGraphQlServices();
            services.AddSwaggerServices(_webApiConfiguration);
        }

        /// <summary>
        /// Essential Services: MemoryCache, Initialization, HttpClient, Configuration ...
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private IServiceCollection AddEssentials(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddTransient<IWebApiInit, WebApiInit>();
            services
                .AddHttpClient(
                    name: HttpClientConstants.SlackHttpClientName,
                    configureClient: (serviceProvider, httpClient) =>
                    {
                        httpClient.Timeout = _webApiConfiguration.SlackHttpClientConfiguration.Timeout;
                    }
                )
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                {
                })
                .AddHttpMessageHandler(serviceProvider => new RetryHttpRequestHandler(
                    maxRetries: _webApiConfiguration.SlackHttpClientConfiguration.MaxRetries
                ));

            services.AddSingleton(serviceProvider => _webApiConfiguration);
            services.AddSingleton(serviceProvider => new ApplicationInformation(
                appEnvironment: _webApiConfiguration.AppConfiguration.AppEnvironment,
                version: _webApiConfiguration.AppConfiguration.Version
            ));

            return services;
        }

        /// <summary>
        /// Web Api Services 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private IServiceCollection AddWebApi(IServiceCollection services)
        {
            services
                .AddControllers(mvcOptions =>
                {
                    mvcOptions.Filters.Add(typeof(CustomExceptionFilterAttribute));
                    mvcOptions.EnableEndpointRouting = false;
                })
                .AddJsonOptions(jsonOptions =>
                {
                    _webApiConfiguration
                        .SystemTextJsonSerializerConfiguration
                        .MapJsonSerializerOptionsToDefaultOptions(
                            jsonSerializerOptions: jsonOptions.JsonSerializerOptions
                        );
                })
                .SetCompatibilityVersion(CompatibilityVersion.Latest)
                .AddFluentValidation(
                    fluentValidatorConfiguration =>
                        fluentValidatorConfiguration
                            .RegisterValidatorsFromAssemblyContaining<GetNewsPortalsQueryValidator>()
                );

            services.AddResponseCaching();

            services.AddHealthChecks();
            services.AddSignalR();

            services
                .AddSpaStaticFiles(configuration =>
                {
                    configuration.RootPath = ClientAppStaticFilesDirectory;
                });

            if (_webApiConfiguration.SpaConfiguration.EnableCors)
            {
                services
                    .AddCors(corsOptions =>
                        corsOptions.AddPolicy(
                            name: CustomCorsPolicyName,
                            configurePolicy: corsPolicyBuilder =>
                            {
                                corsPolicyBuilder.AllowAnyOrigin()
                                       .AllowAnyMethod()
                                       .AllowAnyHeader();
                            }
                        )
                    );
            }

            services
                .AddApiVersioning(apiVersioningOptions =>
                {
                    apiVersioningOptions.DefaultApiVersion = new ApiVersion(
                        majorVersion: 1,
                        minorVersion: 2
                    );
                    apiVersioningOptions.AssumeDefaultVersionWhenUnspecified = true;
                    apiVersioningOptions.ReportApiVersions = true;
                    apiVersioningOptions.ApiVersionReader = new HeaderApiVersionReader(HttpHeaderConstants.ApiVersionHeaderName);
                });

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = ApiKeyAuthenticationOptions.DefaultScheme;
                    options.DefaultChallengeScheme = ApiKeyAuthenticationOptions.DefaultScheme;
                })
                .AddApiKeySupport(options => { });

            services.AddTransient<IApiKeyProvider, InMemoryApiKeyProvider>();

            return services;
        }

        /// <summary>
        /// Mediator Services
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMediatRServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(GetNewsPortalsQuery).GetTypeInfo(), typeof(LoggerRequestPipeline<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionRequestPipeline<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggerRequestPipeline<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipeline<,>));

            return services;
        }

        /// <summary>
        /// Application Services: SlackService, TrendingScoreService, LoggerService
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceCollection AddApplicationServices(IServiceCollection services)
        {
            services.AddScoped<ISlackService, SlackService>(serviceProvider => new SlackService(
                memoryCache: serviceProvider.GetRequiredService<IMemoryCache>(),
                httpClientFactory: serviceProvider.GetRequiredService<IHttpClientFactory>(),
                loggerService: serviceProvider.GetRequiredService<ILoggerService<SlackService>>(),
                jsonService: serviceProvider.GetRequiredService<IJsonService>(),
                webHookUrl: _webApiConfiguration.AppConfiguration.SlackWebHook,
                applicationInformation: serviceProvider.GetRequiredService<ApplicationInformation>()
            ));
            services.AddTransient<ITrendingScoreService, TrendingScoreService>(serviceProvider => new TrendingScoreService(
                halfOfMaxTrendingScoreValue: _webApiConfiguration.TrendingScoreConfiguration.HalfOfMaxTrendingScoreValue,
                ageWeight: _webApiConfiguration.TrendingScoreConfiguration.AgeWeight
            ));
            services.AddScoped(typeof(ILoggerService<>), typeof(LoggerService<>));
            services.AddTransient<IJsonService, SystemTextJsonService>(serviceProvider => new SystemTextJsonService(
                defaultJsonSerializerOptions: _webApiConfiguration.SystemTextJsonSerializerConfiguration.JsonSerializerOptions
            ));
            services.AddScoped<IRemoveOldArticlesService, RemoveOldArticlesService>(
                serviceProvider => new RemoveOldArticlesService(
                    maxAgeOfArticle: _webApiConfiguration.DateTimeConfiguration.MaxAgeOfArticle
                )
            );

            return services;
        }

        /// <summary>
        /// Persistence Services
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private IServiceCollection AddPersistence(IServiceCollection services)
        {
            services.AddDbContext<IApplicationDatabaseContext, ApplicationDatabaseContext>(options =>
            {
                options.UseSqlServer(
                    connectionString: _webApiConfiguration.DatabaseConfiguration.ConnectionString,
                    sqlServerOptionsAction: sqlServerOptions =>
                    {
                        sqlServerOptions.CommandTimeout(_webApiConfiguration.DatabaseConfiguration.CommandTimeoutInSeconds);
                    }
                );
                options.UseQueryTrackingBehavior(_webApiConfiguration.DatabaseConfiguration.QueryTrackingBehavior);
                options.EnableDetailedErrors(_webApiConfiguration.DatabaseConfiguration.EnableDetailedErrors);
                options.EnableSensitiveDataLogging(_webApiConfiguration.DatabaseConfiguration.EnableSensitiveDataLogging);
            });

            services.AddScoped<IDatabaseConnectionFactory>(
                serviceProvider => new DatabaseConnectionFactory(_webApiConfiguration.DatabaseConfiguration.ConnectionString)
            );
            services.AddScoped<IApplicationDownloadRepository, ApplicationDownloadRepository>();
            services.AddScoped<IArticleCategoryRepository, ArticleCategoryRepository>();
            services.AddScoped<IArticleRepository, ArticleRepository>();

            return services;
        }

        /// <summary>
        /// Adds Jobs
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private IServiceCollection AddJobs(IServiceCollection services)
        {
            services.AddCronJob<ApplicationDownloadStatisticsCronJob>(cronJobConfiguration =>
                {
                    cronJobConfiguration.CronExpression = _webApiConfiguration
                        .CronJobsConfiguration
                        .ApplicationDownloadStatisticsCronExpression;
                    cronJobConfiguration.TimeZoneInfo = TimeZoneInfo.Utc;
                    cronJobConfiguration.Version = _webApiConfiguration.AppConfiguration.Version;
                    cronJobConfiguration.AppEnvironment = _webApiConfiguration.AppConfiguration.AppEnvironment;
                });

            services.AddCronJob<WebApiReportCronJob>(cronJobConfiguration =>
            {
                cronJobConfiguration.CronExpression = _webApiConfiguration
                    .CronJobsConfiguration
                    .WebApiReportCronExpression;
                cronJobConfiguration.TimeZoneInfo = TimeZoneInfo.Utc;
                cronJobConfiguration.Version = _webApiConfiguration.AppConfiguration.Version;
                cronJobConfiguration.AppEnvironment = _webApiConfiguration.AppConfiguration.AppEnvironment;
            });

            if (_webApiConfiguration.RabbitMqConfiguration.UseRabbitMqServer)
            {
                services.AddHostedService(serviceProvider => new ReceiveArticlesBackgroundJob(
                    serviceScopeFactory: serviceProvider.GetRequiredService<IServiceScopeFactory>(),
                    webApiConfiguration: _webApiConfiguration,
                    jsonService: serviceProvider.GetRequiredService<IJsonService>(),
                    hostName: _webApiConfiguration.RabbitMqConfiguration.HostName,
                    queueName: _webApiConfiguration.RabbitMqConfiguration.ArticlesQueueName,
                    port: _webApiConfiguration.RabbitMqConfiguration.Port,
                    userName: _webApiConfiguration.RabbitMqConfiguration.Username,
                    password: _webApiConfiguration.RabbitMqConfiguration.Password
                ));
            }

            return services;
        }
    }
}
