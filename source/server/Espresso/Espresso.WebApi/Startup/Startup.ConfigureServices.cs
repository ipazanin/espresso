using System;
using System.Net.Http;
using System.Reflection;
using Espresso.Application.Infrastructure.CronJobsInfrastructure;
using Espresso.Application.Infrastructure.MediatorInfrastructure;
using Espresso.Application.Models;
using Espresso.Application.Services.Contracts;
using Espresso.Application.Services.Implementations;
using Espresso.Application.Utilities;
using Espresso.Common.Constants;
using Espresso.Common.Services.Contracts;
using Espresso.Common.Services.Implementations;
using Espresso.Dashboard.Application.Constants;
using Espresso.Domain.IServices;
using Espresso.Domain.Services;
using Espresso.Persistence.Database;
using Espresso.WebApi.Application.Initialization;
using Espresso.WebApi.Application.NewsPortals.Queries.GetNewsPortals;
using Espresso.WebApi.Authentication;
using Espresso.WebApi.Extensions;
using Espresso.WebApi.Filters;
using Espresso.WebApi.Jobs.CronJobs;
using Espresso.WebApi.Services;
using FluentValidation.AspNetCore;
using MediatR;
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
        /// Configures Applications DI IoC Container.
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
        private void AddEssentials(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddTransient<IWebApiInit, WebApiInit>();
            services
                .AddHttpClient(
                    name: HttpClientConstants.SlackHttpClientName,
                    configureClient: (serviceProvider, httpClient) => httpClient.Timeout = _webApiConfiguration.SlackHttpClientConfiguration.Timeout
                )
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler())
                .AddHttpMessageHandler(serviceProvider => new RetryHttpRequestHandler(
                    maxRetries: _webApiConfiguration.SlackHttpClientConfiguration.MaxRetries
                ));

            services.AddSingleton(serviceProvider => _webApiConfiguration);
            services.AddSingleton(serviceProvider => new ApplicationInformation(
                appEnvironment: _webApiConfiguration.AppConfiguration.AppEnvironment,
                version: _webApiConfiguration.AppConfiguration.Version
            ));
        }

        /// <summary>
        /// Web Api Services.
        /// </summary>
        /// <param name="services"></param>
        private void AddWebApi(IServiceCollection services)
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
                .AddSpaStaticFiles(configuration => configuration.RootPath = ClientAppStaticFilesDirectory);

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
        }

        /// <summary>
        /// Mediator Services.
        /// </summary>
        /// <param name="services"></param>
        private static void AddMediatRServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(GetNewsPortalsQuery).GetTypeInfo(), typeof(LoggerRequestPipeline<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionRequestPipeline<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggerRequestPipeline<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipeline<,>));
        }

        /// <summary>
        /// Application Services: SlackService, TrendingScoreService, LoggerService.
        /// </summary>
        /// <param name="services"></param>
        private void AddApplicationServices(IServiceCollection services)
        {
            services.AddScoped<ISlackService, SlackService>(serviceProvider => new SlackService(
                memoryCache: serviceProvider.GetRequiredService<IMemoryCache>(),
                httpClientFactory: serviceProvider.GetRequiredService<IHttpClientFactory>(),
                loggerService: serviceProvider.GetRequiredService<ILoggerService<SlackService>>(),
                jsonService: serviceProvider.GetRequiredService<IJsonService>(),
                webHookUrl: _webApiConfiguration.AppConfiguration.SlackWebHook,
                applicationInformation: serviceProvider.GetRequiredService<ApplicationInformation>()
            ));
            services.AddTransient<ITrendingScoreService, TrendingScoreService>(_ => new TrendingScoreService(
                halfOfMaxTrendingScoreValue: _webApiConfiguration.TrendingScoreConfiguration.HalfOfMaxTrendingScoreValue,
                ageWeight: _webApiConfiguration.TrendingScoreConfiguration.AgeWeight
            ));
            services.AddScoped(typeof(ILoggerService<>), typeof(LoggerService<>));
            services.AddTransient<IJsonService, SystemTextJsonService>(_ => new SystemTextJsonService(
                defaultJsonSerializerOptions: _webApiConfiguration.SystemTextJsonSerializerConfiguration.JsonSerializerOptions
            ));
            services.AddScoped<IRemoveOldArticlesService, RemoveOldArticlesService>(
                _ => new RemoveOldArticlesService(
                    maxAgeOfArticle: _webApiConfiguration.DateTimeConfiguration.MaxAgeOfArticle
                )
            );

            services.AddScoped<IGoogleAnalyticsService, GoogleAnalyticsService>();
        }

        /// <summary>
        /// Persistence Services.
        /// </summary>
        /// <param name="services"></param>
        private void AddPersistence(IServiceCollection services)
        {
            services.AddDbContext<IEspressoDatabaseContext, EspressoDatabaseContext>(options =>
            {
                options.UseNpgsql(
                    connectionString: _webApiConfiguration.DatabaseConfiguration.EspressoDatabaseConnectionString,
                    npgsqlOptionsAction: npgOptions => npgOptions.CommandTimeout(_webApiConfiguration.DatabaseConfiguration.CommandTimeoutInSeconds)
                );
                options.UseQueryTrackingBehavior(_webApiConfiguration.DatabaseConfiguration.QueryTrackingBehavior);
                options.EnableDetailedErrors(_webApiConfiguration.DatabaseConfiguration.EnableDetailedErrors);
                options.EnableSensitiveDataLogging(_webApiConfiguration.DatabaseConfiguration.EnableSensitiveDataLogging);
            });

            services.AddScoped<IDatabaseConnectionFactory>(
                _ => new DatabaseConnectionFactory(_webApiConfiguration.DatabaseConfiguration.EspressoDatabaseConnectionString)
            );
        }

        /// <summary>
        /// Adds Jobs.
        /// </summary>
        /// <param name="services"></param>
        private void AddJobs(IServiceCollection services)
        {
            services.AddCronJob<AnalyticsCronJob>(cronJobConfiguration =>
                {
                    cronJobConfiguration.CronExpression = _webApiConfiguration
                        .CronJobsConfiguration
                        .AnalyticsCronExpression;
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
        }
    }
}
