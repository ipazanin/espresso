using Espresso.Application.Infrastructure.MediatorInfrastructure;
using Espresso.Application.IServices;
using Espresso.Dashboard.Configuration;
using Espresso.Persistence.Database;
using Espresso.Persistence.IRepositories;
using Espresso.Persistence.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Espresso.Dashboard.Application.IServices;
using Espresso.Application.Services;
using Espresso.Dashboard.Application.Services;
using Espresso.Dashboard.ParseRssFeeds.Validators;
using Espresso.Dashboard.ParseRssFeeds;
using FluentValidation;
using System;
using Espresso.Dashboard.CronJobs;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http;
using Espresso.Domain.IServices;
using Espresso.Domain.Services;
using Espresso.Dashboard.Services;
using Espresso.Application.Infrastructure.CronJobsInfrastructure;
using Espresso.Dashboard.Application.Initialization;
using Espresso.Application.Models;
using Espresso.Common.Services;
using Espresso.Common.IServices;
using Espresso.Application.Utilities;
using Espresso.Dashboard.Application.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using Espresso.Dashboard.Areas.Identity;

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
            services.AddMemoryCache();
            services.AddTransient<IDashboardInit, DashboardInit>(serviceProvider => new DashboardInit(
                memoryCache: serviceProvider.GetRequiredService<IMemoryCache>(),
                context: serviceProvider.GetRequiredService<IEspressoDatabaseContext>(),
                espressoIdentityContext: serviceProvider.GetRequiredService<IEspressoIdentityDatabaseContext>(),
                roleManager: serviceProvider.GetRequiredService<RoleManager<IdentityRole>>(),
                userManager: serviceProvider.GetRequiredService<UserManager<IdentityUser>>(),
                adminUserPassword: _dashboardConfiguration.AppConfiguration.AdminUserPassword,
                loggerService: serviceProvider.GetRequiredService<ILoggerService<DashboardInit>>()
            ));
            services.AddSingleton(serviceProvider => _dashboardConfiguration);

            services
                .AddHttpClient(
                    name: HttpClientConstants.SlackHttpClientName,
                    configureClient: (serviceProvider, httpClient) =>
                    {
                        httpClient.Timeout = _dashboardConfiguration.SlackHttpClientConfiguration.Timeout;
                    }
                )
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                {
                })
                .AddHttpMessageHandler(serviceProvider => new RetryHttpRequestHandler(
                    maxRetries: _dashboardConfiguration.SlackHttpClientConfiguration.MaxRetries
                ));

            services
                .AddHttpClient(
                    name: HttpClientConstants.SendArticlesHttpClientName,
                    configureClient: (serviceProvider, httpClient) =>
                    {
                        httpClient.Timeout = _dashboardConfiguration.SendArticlesHttpClientConfiguration.Timeout;
                    }
                )
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                {
                })
                .AddHttpMessageHandler(serviceProvider => new RetryHttpRequestHandler(
                    maxRetries: _dashboardConfiguration.SendArticlesHttpClientConfiguration.MaxRetries
                ));

            services
                .AddHttpClient(
                    name: HttpClientConstants.LoadRssFeedsHttpClientName,
                    configureClient: (serviceProvider, httpClient) =>
                    {
                        httpClient.Timeout = _dashboardConfiguration.LoadRssFeedsHttpClientConfiguration.Timeout;
                    }
                )
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                {
                })
                .AddHttpMessageHandler(serviceProvider => new RetryHttpRequestHandler(
                    maxRetries: _dashboardConfiguration.LoadRssFeedsHttpClientConfiguration.MaxRetries
                ));

            services
                .AddHttpClient(
                    name: HttpClientConstants.ScrapeWebHttpClientName,
                    configureClient: (serviceProvider, httpClient) =>
                    {
                        httpClient.Timeout = _dashboardConfiguration.ScrapeWebHttpClientConfiguration.Timeout;
                    }
                )
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                {
                })
                .AddHttpMessageHandler(serviceProvider => new RetryHttpRequestHandler(
                    maxRetries: _dashboardConfiguration.ScrapeWebHttpClientConfiguration.MaxRetries
                ));

            services.AddSingleton<IDashboardConfiguration, DashboardConfiguration>();
            services.AddValidatorsFromAssembly(typeof(ArticleDataValidator).Assembly);
            services.AddSingleton(serviceProvider => new ApplicationInformation(
                appEnvironment: _dashboardConfiguration.AppConfiguration.AppEnvironment,
                version: _dashboardConfiguration.AppConfiguration.Version
            ));

            services.AddScoped<IEmailService, SendGridEmailService>(serviceProvider => new SendGridEmailService(
                sendGridKey: _dashboardConfiguration.AppConfiguration.SendGridApiKey
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

            return services;
        }

        /// <summary>
        /// Mediator Services
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private static IServiceCollection AddMediatRServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(ParseRssFeedsCommandHandler), typeof(LoggerRequestPipeline<,>));
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
                    articlePublishDateTimeDiferenceThreshold: _dashboardConfiguration.ArticleSimilarityConfiguration.ArticlePublishDateTimeDiferenceThreshold,
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
        /// Persistence Services
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private IServiceCollection AddPersistence(IServiceCollection services)
        {
            services.AddDbContext<IEspressoDatabaseContext, EspressoDatabaseContext>(options =>
             {
                 options.UseSqlServer(
                     connectionString: _dashboardConfiguration.DatabaseConfiguration.EspressoDatabaseConnectionString,
                     sqlServerOptionsAction: sqlServerOptions =>
                     {
                         sqlServerOptions.CommandTimeout(_dashboardConfiguration.DatabaseConfiguration.CommandTimeoutInSeconds);
                     }
                 );
                 options.UseQueryTrackingBehavior(_dashboardConfiguration.DatabaseConfiguration.QueryTrackingBehavior);
                 options.EnableDetailedErrors(_dashboardConfiguration.DatabaseConfiguration.EnableDetailedErrors);
                 options.EnableSensitiveDataLogging(_dashboardConfiguration.DatabaseConfiguration.EnableSensitiveDataLogging);
             });

            services.AddDbContext<IEspressoIdentityDatabaseContext, EspressoIdentityDatabaseContext>(options =>
            {
                options.UseSqlServer(
                    connectionString: _dashboardConfiguration.DatabaseConfiguration.EspressoIdentityDatabaseConnectionString,
                    sqlServerOptionsAction: sqlServerOptions =>
                    {
                        sqlServerOptions.CommandTimeout(_dashboardConfiguration.DatabaseConfiguration.CommandTimeoutInSeconds);
                    }
                );
                options.UseQueryTrackingBehavior(_dashboardConfiguration.DatabaseConfiguration.QueryTrackingBehavior);
                options.EnableDetailedErrors(_dashboardConfiguration.DatabaseConfiguration.EnableDetailedErrors);
                options.EnableSensitiveDataLogging(_dashboardConfiguration.DatabaseConfiguration.EnableSensitiveDataLogging);
            });

            services.AddDbContext<EspressoIdentityDatabaseContext>(options =>
            {
                options.UseSqlServer(
                    connectionString: _dashboardConfiguration.DatabaseConfiguration.EspressoIdentityDatabaseConnectionString,
                    sqlServerOptionsAction: sqlServerOptions =>
                    {
                        sqlServerOptions.CommandTimeout(_dashboardConfiguration.DatabaseConfiguration.CommandTimeoutInSeconds);
                    }
                );
                options.UseQueryTrackingBehavior(_dashboardConfiguration.DatabaseConfiguration.QueryTrackingBehavior);
                options.EnableDetailedErrors(_dashboardConfiguration.DatabaseConfiguration.EnableDetailedErrors);
                options.EnableSensitiveDataLogging(_dashboardConfiguration.DatabaseConfiguration.EnableSensitiveDataLogging);
            });


            services.AddScoped<IDatabaseConnectionFactory, DatabaseConnectionFactory>(serviceProvider => new DatabaseConnectionFactory(
                connectionString: _dashboardConfiguration.DatabaseConfiguration.EspressoDatabaseConnectionString
            ));
            services.AddScoped<IApplicationDownloadRepository, ApplicationDownloadRepository>();
            services.AddScoped<IArticleCategoryRepository, ArticleCategoryRepository>();
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<ISimilarArticleRepository, SimilarArticleRepository>();

            return services;
        }

        /// <summary>
        /// Adds Jobs
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private IServiceCollection AddJobs(IServiceCollection services)
        {
            services.AddCronJob<DeleteArticlesCronJob>(cronJobConfiguration =>
            {
                cronJobConfiguration.CronExpression = _dashboardConfiguration
                    .CronJobsConfiguration
                    .DeleteArticlesCronExpression;
                cronJobConfiguration.TimeZoneInfo = TimeZoneInfo.Utc;
                cronJobConfiguration.AppEnvironment = _dashboardConfiguration.AppConfiguration.AppEnvironment;
                cronJobConfiguration.Version = _dashboardConfiguration.AppConfiguration.Version;
            });
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
