// Startup.ConfigureServices.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.Infrastructure.CronJobsInfrastructure;
using Espresso.Application.Infrastructure.MediatorInfrastructure;
using Espresso.Application.Infrastructure.SettingsInfrastructure;
using Espresso.Application.Models;
using Espresso.Application.Services.Contracts;
using Espresso.Application.Services.Implementations;
using Espresso.Common.Constants;
using Espresso.Common.Services.Contacts;
using Espresso.Common.Services.Implementations;
using Espresso.Domain.Infrastructure;
using Espresso.Domain.IServices;
using Espresso.Domain.Services;
using Espresso.Persistence.Database;
using Espresso.WebApi.Application.HealthChecks;
using Espresso.WebApi.Application.Initialization;
using Espresso.WebApi.Application.NewsPortals.Queries.GetNewsPortals;
using Espresso.WebApi.Authentication;
using Espresso.WebApi.Extensions;
using Espresso.WebApi.Filters;
using Espresso.WebApi.Jobs.CronJobs;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;

namespace Espresso.WebApi.StartupConfiguration;

public sealed partial class Startup
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

        _ = services.AddSwaggerServices(_webApiConfiguration);
    }

    /// <summary>
    /// Mediator Services.
    /// </summary>
    /// <param name="services"></param>
    private static void AddMediatRServices(IServiceCollection services)
    {
        _ = services.AddMediatR(configuration =>
        {
            _ = configuration.RegisterServicesFromAssemblyContaining<GetNewsPortalsQuery>();
            _ = configuration.RegisterServicesFromAssemblyContaining(typeof(LoggerRequestPipeline<,>));
        });
        _ = services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionRequestPipeline<,>));
        _ = services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggerRequestPipeline<,>));
        _ = services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipeline<,>));
    }

    /// <summary>
    /// Essential Services: MemoryCache, Initialization, HttpClient, Configuration ...
    /// </summary>
    /// <param name="services"></param>
    private void AddEssentials(IServiceCollection services)
    {
        _ = services.AddMemoryCache();
        _ = services.AddTransient<IWebApiInit, WebApiInit>();

        var retryPolicy = HttpPolicyExtensions
            .HandleTransientHttpError()
            .Or<TimeoutRejectedException>() // thrown by Polly's TimeoutPolicy if the inner execution times out
            .RetryAsync(3);

        _ = services
            .AddHttpClient(
                name: HttpClientConstants.SlackHttpClientName,
                configureClient: (_, httpClient) => httpClient.Timeout = _webApiConfiguration.SlackHttpClientConfiguration.Timeout)
            .AddPolicyHandler(retryPolicy)
            .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(20)));

        _ = services.AddSingleton(_ => _webApiConfiguration);
        _ = services.AddSingleton(_ => new ApplicationInformation(
            appEnvironment: _webApiConfiguration.AppConfiguration.AppEnvironment,
            version: _webApiConfiguration.AppConfiguration.Version));
        _ = services.AddScoped<ISettingProvider, SettingProvider>();
    }

    /// <summary>
    /// Web Api Services.
    /// </summary>
    /// <param name="services"></param>
    private void AddWebApi(IServiceCollection services)
    {
        _ = services
            .AddControllers(mvcOptions =>
            {
                _ = mvcOptions.Filters.Add(typeof(CustomExceptionFilterAttribute));
                mvcOptions.EnableEndpointRouting = false;
            })
            .AddJsonOptions(jsonOptions =>
            {
                _webApiConfiguration
                    .SystemTextJsonSerializerConfiguration
                    .MapJsonSerializerOptionsToDefaultOptions(
                        jsonSerializerOptions: jsonOptions.JsonSerializerOptions);
            });

        _ = services.AddResponseCaching();

        _ = services.AddSingleton<ReadinessHealthCheck>();
        _ = services.AddHealthChecks()
            .AddCheck<StartupHealthCheck>(
                name: nameof(StartupHealthCheck),
                failureStatus: Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Unhealthy,
                tags: [HealthCheckConstants.StartupTag])
            .AddCheck<ReadinessHealthCheck>(
                name: nameof(ReadinessHealthCheck),
                failureStatus: Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Unhealthy,
                tags: [HealthCheckConstants.ReadinessTag])
            .AddCheck<LivenessHealthCheck>(
                name: nameof(LivenessHealthCheck),
                failureStatus: Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Unhealthy,
                tags: [HealthCheckConstants.LivenessTag]);

        _ = services.AddSignalR();

        services
            .AddSpaStaticFiles(configuration => configuration.RootPath = ClientAppStaticFilesDirectory);

        if (_webApiConfiguration.SpaConfiguration.EnableCors)
        {
            _ = services
                .AddCors(corsOptions =>
                    corsOptions.AddPolicy(
                        name: CustomCorsPolicyName,
                        configurePolicy: corsPolicyBuilder =>
                        {
                            _ = corsPolicyBuilder.AllowAnyOrigin()
                                   .AllowAnyMethod()
                                   .AllowAnyHeader();
                        }));
        }

        _ = services
            .AddApiVersioning(apiVersioningOptions =>
            {
                apiVersioningOptions.DefaultApiVersion = new ApiVersion(
                    majorVersion: 1,
                    minorVersion: 2);
                apiVersioningOptions.AssumeDefaultVersionWhenUnspecified = true;
                apiVersioningOptions.ReportApiVersions = true;
                apiVersioningOptions.ApiVersionReader = new HeaderApiVersionReader(HttpHeaderConstants.ApiVersionHeaderName);
            });

        _ = services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = ApiKeyAuthenticationOptions.DefaultScheme;
                options.DefaultChallengeScheme = ApiKeyAuthenticationOptions.DefaultScheme;
            })
            .AddApiKeySupport(_ => { });

        _ = services.AddTransient<IApiKeyProvider, InMemoryApiKeyProvider>();
    }

    /// <summary>
    /// Application Services: SlackService, TrendingScoreService, LoggerService.
    /// </summary>
    /// <param name="services"></param>
    private void AddApplicationServices(IServiceCollection services)
    {
        _ = services.AddScoped<ISlackService, SlackService>(serviceProvider => new SlackService(
            memoryCache: serviceProvider.GetRequiredService<IMemoryCache>(),
            httpClientFactory: serviceProvider.GetRequiredService<IHttpClientFactory>(),
            loggerService: serviceProvider.GetRequiredService<ILoggerService<SlackService>>(),
            jsonService: serviceProvider.GetRequiredService<IJsonService>(),
            analyticsWebHookUrl: _webApiConfiguration.AppConfiguration.AnalyticsSlackWebHook,
            crashReportWebHookUrl: _webApiConfiguration.AppConfiguration.CrashReportSlackWebHook,
            newSourceRequestWebHookUrl: _webApiConfiguration.AppConfiguration.NewSourceRequestSlackWebHook,
            applicationInformation: serviceProvider.GetRequiredService<ApplicationInformation>()));
        _ = services.AddTransient<ITrendingScoreService, TrendingScoreService>(_ => new TrendingScoreService(
            halfOfMaxTrendingScoreValue: _webApiConfiguration.TrendingScoreConfiguration.HalfOfMaxTrendingScoreValue,
            ageWeight: _webApiConfiguration.TrendingScoreConfiguration.AgeWeight));
        _ = services.AddScoped(typeof(ILoggerService<>), typeof(LoggerService<>));
        _ = services.AddTransient<IJsonService, SystemTextJsonService>(_ => new SystemTextJsonService(
            defaultJsonSerializerOptions: _webApiConfiguration.SystemTextJsonSerializerConfiguration.JsonSerializerOptions));
        _ = services.AddScoped<IRemoveOldArticlesService, RemoveOldArticlesService>();

        _ = services.AddScoped<IGoogleAnalyticsService, GoogleAnalyticsService>();
        _ = services.AddScoped<IArticleLoaderService, ArticleDatabaseLoaderService>();
        _ = services.AddScoped<IRefreshWebApiCache, RefreshWebApiCache>();
        _ = services.AddScoped<INewsPortalImagesService>(serviceProvider => new NewsPortalImagesService(
            espressoDatabaseContext: serviceProvider.GetRequiredService<IEspressoDatabaseContext>(),
            folderRootPath: serviceProvider.GetRequiredService<IWebHostEnvironment>().WebRootPath));
    }

    /// <summary>
    /// Persistence Services.
    /// </summary>
    /// <param name="services"></param>
    private void AddPersistence(IServiceCollection services)
    {
        _ = services.AddDbContext<IEspressoDatabaseContext, EspressoDatabaseContext>(options =>
        {
            _ = options.UseNpgsql(
                connectionString: _webApiConfiguration.DatabaseConfiguration.EspressoDatabaseConnectionString,
                npgsqlOptionsAction: npgOptions => npgOptions.CommandTimeout(_webApiConfiguration.DatabaseConfiguration.CommandTimeoutInSeconds));
            _ = options.UseQueryTrackingBehavior(_webApiConfiguration.DatabaseConfiguration.QueryTrackingBehavior);
            _ = options.EnableDetailedErrors(_webApiConfiguration.DatabaseConfiguration.EnableDetailedErrors);
            _ = options.EnableSensitiveDataLogging(_webApiConfiguration.DatabaseConfiguration.EnableSensitiveDataLogging);
        });

        _ = services.AddScoped<IDatabaseConnectionFactory>(
            _ => new DatabaseConnectionFactory(_webApiConfiguration.DatabaseConfiguration.EspressoDatabaseConnectionString));
    }

    /// <summary>
    /// Adds Jobs.
    /// </summary>
    /// <param name="services"></param>
    private void AddJobs(IServiceCollection services)
    {
        _ = services.AddSingleton<ICronJobConfiguration>(_ =>
        {
            return new CronJobConfiguration(
                timeZoneInfo: TimeZoneInfo.Utc,
                version: _webApiConfiguration.AppConfiguration.Version,
                appEnvironment: _webApiConfiguration.AppConfiguration.AppEnvironment);
        });

        _ = services.AddHostedService<AnalyticsCronJob>();
        _ = services.AddHostedService<WebApiReportCronJob>();
    }
}
