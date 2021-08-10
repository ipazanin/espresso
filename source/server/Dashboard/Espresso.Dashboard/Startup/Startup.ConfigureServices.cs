// Startup.ConfigureServices.cs
//
// © 2021 Espresso News. All rights reserved.

using System;
using System.Net.Http;
using Espresso.Application.Infrastructure.CronJobsInfrastructure;
using Espresso.Application.Infrastructure.MediatorInfrastructure;
using Espresso.Application.Models;
using Espresso.Application.Services.Contracts;
using Espresso.Application.Services.Implementations;
using Espresso.Common.Services.Contracts;
using Espresso.Common.Services.Implementations;
using Espresso.Dashboard.Application.Constants;
using Espresso.Dashboard.Application.Initialization;
using Espresso.Dashboard.Application.IServices;
using Espresso.Dashboard.Application.Services;
using Espresso.Dashboard.Areas.Identity;
using Espresso.Dashboard.Configuration;
using Espresso.Dashboard.CronJobs;
using Espresso.Dashboard.ParseRssFeeds;
using Espresso.Dashboard.ParseRssFeeds.Validators;
using Espresso.Dashboard.Services;
using Espresso.Domain.IServices;
using Espresso.Domain.Services;
using Espresso.Persistence.Database;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;

namespace Espresso.Dashboard.Startup
{
    internal sealed partial class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            AddEssentials(services);
            AddWebApi(services);
            AddMediatRServices(services);
            AddApplicationServices(services);
            AddPersistence(services);
            AddJobs(services);
            AddAuth(services);
        }

        /// <summary>
        /// Essential Services: MemoryCache, Initialization, HttpClient, Configuration ...
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private IServiceCollection AddEssentials(IServiceCollection services)
        {
            var retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .Or<TimeoutRejectedException>() // thrown by Polly's TimeoutPolicy if the inner execution times out
                .RetryAsync(3);

            services.AddMemoryCache();
            services.AddTransient<IDashboardInit, DashboardInit>(serviceProvider => new DashboardInit(
                espressoIdentityContext: serviceProvider.GetRequiredService<IEspressoIdentityDatabaseContext>(),
                roleManager: serviceProvider.GetRequiredService<RoleManager<IdentityRole>>(),
                userManager: serviceProvider.GetRequiredService<UserManager<IdentityUser>>(),
                adminUserPassword: _dashboardConfiguration.AppConfiguration.AdminUserPassword
            ));
            services.AddSingleton(_ => _dashboardConfiguration);

            services
                .AddHttpClient(
                    name: HttpClientConstants.SlackHttpClientName,
                    configureClient: (_, httpClient) => httpClient.Timeout = _dashboardConfiguration.SlackHttpClientConfiguration.Timeout
                )
                .AddPolicyHandler(retryPolicy)
                .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(20)));

            services
                .AddHttpClient(
                    name: HttpClientConstants.SendArticlesHttpClientName,
                    configureClient: (_, httpClient) =>
                    {
                        httpClient.Timeout = _dashboardConfiguration.SendArticlesHttpClientConfiguration.Timeout;
                    }
                )
                .AddPolicyHandler(retryPolicy)
                .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(90)));

            services
                .AddHttpClient(
                    name: HttpClientConstants.LoadRssFeedsHttpClientName,
                    configureClient: (_, httpClient) =>
                    {
                        httpClient.Timeout = _dashboardConfiguration.LoadRssFeedsHttpClientConfiguration.Timeout;
                    }
                )
                .AddPolicyHandler(retryPolicy)
                .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(10)));

            services
                .AddHttpClient(
                    name: HttpClientConstants.ScrapeWebHttpClientName,
                    configureClient: (_, httpClient) =>
                    {
                        httpClient.Timeout = _dashboardConfiguration.ScrapeWebHttpClientConfiguration.Timeout;
                    }
                )
                .AddPolicyHandler(retryPolicy)
                .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(10)));

            services.AddSingleton<IDashboardConfiguration, DashboardConfiguration>();
            services.AddValidatorsFromAssembly(typeof(ArticleDataValidator).Assembly);
            services.AddSingleton(_ => new ApplicationInformation(
                appEnvironment: _dashboardConfiguration.AppConfiguration.AppEnvironment,
                version: _dashboardConfiguration.AppConfiguration.Version
            ));

            services.AddScoped<IEmailService, SendGridEmailService>(_ => new SendGridEmailService(
                sendGridKey: _dashboardConfiguration.AppConfiguration.SendGridApiKey
            ));

            return services;
        }

        /// <summary>
        /// Web Api Services.
        /// </summary>
        /// <param name="services">Services.</param>
        private void AddWebApi(IServiceCollection services)
        {
            services.AddServerSideBlazor();
            services.AddRazorPages()
                .AddJsonOptions(jsonOptions =>
                {
                    _dashboardConfiguration
                        .SystemTextJsonSerializerConfiguration
                        .MapJsonSerializerOptionsToDefaultOptions(
                            jsonSerializerOptions: jsonOptions.JsonSerializerOptions
                        );
                });
            services.AddHealthChecks();
        }

        /// <summary>
        /// Mediator Services.
        /// </summary>
        /// <param name="services">Services.</param>
        private static void AddMediatRServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(ParseRssFeedsCommandHandler), typeof(LoggerRequestPipeline<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionRequestPipeline<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggerRequestPipeline<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipeline<,>));
        }

        /// <summary>
        /// Application Services: SlackService, TrendingScoreService, LoggerService.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private IServiceCollection AddApplicationServices(IServiceCollection services)
        {
            services.AddScoped<ISlackService, SlackService>(serviceProvider => new SlackService(
               memoryCache: serviceProvider.GetRequiredService<IMemoryCache>(),
               httpClientFactory: serviceProvider.GetRequiredService<IHttpClientFactory>(),
               loggerService: serviceProvider.GetRequiredService<ILoggerService<SlackService>>(),
               jsonService: serviceProvider.GetRequiredService<IJsonService>(),
               webHookUrl: _dashboardConfiguration.AppConfiguration.SlackWebHook,
               applicationInformation: serviceProvider.GetRequiredService<ApplicationInformation>()
           ));
            services.AddScoped<ILoadRssFeedsService, LoadRssFeedsService>();
            services.AddScoped<ICreateArticleService>(serviceProvider => new CreateArticleService(
                webScrapingService: serviceProvider.GetRequiredService<IScrapeWebService>(),
                htmlParsingService: serviceProvider.GetRequiredService<IParseHtmlService>(),
                articleDataValidator: serviceProvider.GetRequiredService<ArticleDataValidator>(),
                loggerService: serviceProvider.GetRequiredService<ILoggerService<CreateArticleService>>(),
                maxAgeOfArticle: _dashboardConfiguration.AppConfiguration.MaxAgeOfArticles
            ));
            services.AddScoped<IScrapeWebService, ScrapeWebService>();
            services.AddScoped<IParseHtmlService, ParseHtmlService>();
            services.AddScoped<ISortArticlesService, SortArticlesService>();
            services.AddScoped(typeof(ILoggerService<>), typeof(LoggerService<>));
            services.AddScoped<IGroupSimilarArticlesService>(
                serviceProvider => new GroupSimilarArticlesService(
                    similarityScoreThreshold: _dashboardConfiguration.ArticleSimilarityConfiguration.SimilarityScoreThreshold,
                    articlePublishDateTimeDiferenceThreshold: _dashboardConfiguration.ArticleSimilarityConfiguration.ArticlePublishDateTimeDifferenceThreshold,
                    loggerService: serviceProvider.GetRequiredService<ILoggerService<GroupSimilarArticlesService>>(),
                    maxAgeOfSimilarArticleChecking: _dashboardConfiguration.ArticleSimilarityConfiguration.MaxAgeOfSimilarArticleChecking,
                    minimalNumberOfWordsForArticleToBeComparable: _dashboardConfiguration.ArticleSimilarityConfiguration.MinimalNumberOfWordsForArticleToBeComparable
                )
            );
            services.AddScoped<ISendArticlesService>(
                serviceProvider =>
                {
                    return _dashboardConfiguration.RabbitMqConfiguration.UseRabbitMqServer ?
                        new SendArticlesRabbitMqService(
                            jsonService: serviceProvider.GetRequiredService<IJsonService>(),
                            hostName: _dashboardConfiguration.RabbitMqConfiguration.HostName,
                            queueName: _dashboardConfiguration.RabbitMqConfiguration.ArticlesQueueName,
                            port: _dashboardConfiguration.RabbitMqConfiguration.Port,
                            username: _dashboardConfiguration.RabbitMqConfiguration.Username,
                            password: _dashboardConfiguration.RabbitMqConfiguration.Password
                        ) :
                        new SendArticlesHttpService(
                           httpClientFactory: serviceProvider.GetRequiredService<IHttpClientFactory>(),
                           loggerService: serviceProvider.GetRequiredService<ILoggerService<SendArticlesHttpService>>(),
                           slackService: serviceProvider.GetRequiredService<ISlackService>(),
                           jsonService: serviceProvider.GetRequiredService<IJsonService>(),
                           parserApiKey: _dashboardConfiguration.ApiKeysConfiguration.ParserApiKey,
                           targetedApiVersion: _dashboardConfiguration.AppConfiguration.RssFeedParserMajorMinorVersion,
                           currentVersion: _dashboardConfiguration.AppConfiguration.Version,
                           serverUrl: _dashboardConfiguration.AppConfiguration.ServerUrl
                        );
                }
            );
            services.AddTransient<IJsonService, SystemTextJsonService>(serviceProvider => new SystemTextJsonService(
                defaultJsonSerializerOptions: _dashboardConfiguration.SystemTextJsonSerializerConfiguration.JsonSerializerOptions
            ));
            services.AddScoped<IRemoveOldArticlesService, RemoveOldArticlesService>(
                serviceProvider => new RemoveOldArticlesService(
                    maxAgeOfArticle: _dashboardConfiguration.AppConfiguration.MaxAgeOfArticles
                )
            );

            return services;
        }

        /// <summary>
        /// Persistence Services.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private IServiceCollection AddPersistence(IServiceCollection services)
        {
            services.AddDbContext<IEspressoDatabaseContext, EspressoDatabaseContext>(options =>
             {
                 options.UseNpgsql(
                     connectionString: _dashboardConfiguration.DatabaseConfiguration.EspressoDatabaseConnectionString,
                     npgsqlOptionsAction: npgOptions =>
                     {
                         npgOptions.CommandTimeout(_dashboardConfiguration.DatabaseConfiguration.CommandTimeoutInSeconds);
                     }
                 );
                 options.UseQueryTrackingBehavior(_dashboardConfiguration.DatabaseConfiguration.QueryTrackingBehavior);
                 options.EnableDetailedErrors(_dashboardConfiguration.DatabaseConfiguration.EnableDetailedErrors);
                 options.EnableSensitiveDataLogging(_dashboardConfiguration.DatabaseConfiguration.EnableSensitiveDataLogging);
             });

            services.AddDbContext<IEspressoIdentityDatabaseContext, EspressoIdentityDatabaseContext>(options =>
            {
                options.UseNpgsql(
                    connectionString: _dashboardConfiguration.DatabaseConfiguration.EspressoIdentityDatabaseConnectionString,
                    npgsqlOptionsAction: npgOptions =>
                    {
                        npgOptions.CommandTimeout(_dashboardConfiguration.DatabaseConfiguration.CommandTimeoutInSeconds);
                    }
                );
                options.UseQueryTrackingBehavior(_dashboardConfiguration.DatabaseConfiguration.QueryTrackingBehavior);
                options.EnableDetailedErrors(_dashboardConfiguration.DatabaseConfiguration.EnableDetailedErrors);
                options.EnableSensitiveDataLogging(_dashboardConfiguration.DatabaseConfiguration.EnableSensitiveDataLogging);
            });

            services.AddDbContext<EspressoIdentityDatabaseContext>(options =>
            {
                options.UseNpgsql(
                    connectionString: _dashboardConfiguration.DatabaseConfiguration.EspressoIdentityDatabaseConnectionString,
                    npgsqlOptionsAction: npgOptions =>
                    {
                        npgOptions.CommandTimeout(_dashboardConfiguration.DatabaseConfiguration.CommandTimeoutInSeconds);
                    }
                );
                options.UseQueryTrackingBehavior(_dashboardConfiguration.DatabaseConfiguration.QueryTrackingBehavior);
                options.EnableDetailedErrors(_dashboardConfiguration.DatabaseConfiguration.EnableDetailedErrors);
                options.EnableSensitiveDataLogging(_dashboardConfiguration.DatabaseConfiguration.EnableSensitiveDataLogging);
            });

            services.AddScoped<IDatabaseConnectionFactory, DatabaseConnectionFactory>(serviceProvider => new DatabaseConnectionFactory(
                connectionString: _dashboardConfiguration.DatabaseConfiguration.EspressoDatabaseConnectionString
            ));

            return services;
        }

        /// <summary>
        /// Adds Jobs.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private IServiceCollection AddJobs(IServiceCollection services)
        {
            services.AddCronJob<ParseArticlesCronJob>(cronJobConfiguration =>
            {
                cronJobConfiguration.CronExpression = _dashboardConfiguration
                    .CronJobsConfiguration
                    .ParseArticlesCronExpression;
                cronJobConfiguration.TimeZoneInfo = TimeZoneInfo.Utc;
                cronJobConfiguration.AppEnvironment = _dashboardConfiguration.AppConfiguration.AppEnvironment;
                cronJobConfiguration.Version = _dashboardConfiguration.AppConfiguration.Version;
            });

            return services;
        }

        private static IServiceCollection AddAuth(IServiceCollection services)
        {
            services.AddOptions();
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
            services.AddIdentity<IdentityUser, IdentityRole>(identityOptions =>
                {
                    identityOptions.Password.RequireDigit = true;
                    identityOptions.Password.RequiredLength = 8;
                    identityOptions.Password.RequireLowercase = true;
                    identityOptions.Password.RequireNonAlphanumeric = false;
                    identityOptions.Password.RequireUppercase = true;

                    identityOptions.SignIn.RequireConfirmedEmail = false;

                    identityOptions.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<EspressoIdentityDatabaseContext>()
                .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>(TokenOptions.DefaultProvider);

            services.AddAuthentication(authenticationOptions =>
                {
                });

            services.AddAuthorization(authorizationOptions =>
                {
                });

            return services;
        }
    }
}
