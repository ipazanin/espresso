using Espresso.Application.Infrastructure.MediatorInfrastructure;
using Espresso.Application.IServices;
using Espresso.ParserDeleter.Configuration;
using Espresso.Persistence.Database;
using Espresso.Persistence.IRepositories;
using Espresso.Persistence.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Espresso.ParserDeleter.Application.IServices;
using Espresso.Application.Services;
using Espresso.ParserDeleter.Application.Services;
using Espresso.ParserDeleter.ParseRssFeeds.Validators;
using Espresso.ParserDeleter.ParseRssFeeds;
using FluentValidation;
using System;
using Espresso.ParserDeleter.CronJobs;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http;
using Espresso.Domain.IServices;
using Espresso.Domain.Services;
using Espresso.ParserDeleter.Services;
using Espresso.Application.Infrastructure.CronJobsInfrastructure;
using Espresso.ParserDeleter.Application.Initialization;
using Espresso.Application.Models;
using Espresso.Common.Services;
using Espresso.Common.IServices;
using Espresso.Application.Utilities;
using Espresso.ParserDeleter.Application.Constants;

namespace Espresso.ParserDeleter.Startup
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
        }

        /// <summary>
        /// Essential Services: MemoryCache, Initialization, HttpClient, Configuration ...
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private IServiceCollection AddEssentials(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddTransient<IParserDeleterInit, ParserDeleterInit>();
            services.AddSingleton(serviceProvider => _parserDeleterConfiguration);

            services
                .AddHttpClient(
                    name: HttpClientConstants.SlackHttpClientName,
                    configureClient: (serviceProvider, httpClient) =>
                    {
                        httpClient.Timeout = _parserDeleterConfiguration.SlackHttpClientConfiguration.Timeout;
                    }
                )
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                {
                })
                .AddHttpMessageHandler(serviceProvider => new RetryHttpRequestHandler(
                    maxRetries: _parserDeleterConfiguration.SlackHttpClientConfiguration.MaxRetries
                ));

            services
                .AddHttpClient(
                    name: HttpClientConstants.SendArticlesHttpClientName,
                    configureClient: (serviceProvider, httpClient) =>
                    {
                        httpClient.Timeout = _parserDeleterConfiguration.SendArticlesHttpClientConfiguration.Timeout;
                    }
                )
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                {
                })
                .AddHttpMessageHandler(serviceProvider => new RetryHttpRequestHandler(
                    maxRetries: _parserDeleterConfiguration.SendArticlesHttpClientConfiguration.MaxRetries
                ));

            services
                .AddHttpClient(
                    name: HttpClientConstants.LoadRssFeedsHttpClientName,
                    configureClient: (serviceProvider, httpClient) =>
                    {
                        httpClient.Timeout = _parserDeleterConfiguration.LoadRssFeedsHttpClientConfiguration.Timeout;
                    }
                )
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                {
                })
                .AddHttpMessageHandler(serviceProvider => new RetryHttpRequestHandler(
                    maxRetries: _parserDeleterConfiguration.LoadRssFeedsHttpClientConfiguration.MaxRetries
                ));

            services
                .AddHttpClient(
                    name: HttpClientConstants.ScrapeWebHttpClientName,
                    configureClient: (serviceProvider, httpClient) =>
                    {
                        httpClient.Timeout = _parserDeleterConfiguration.ScrapeWebHttpClientConfiguration.Timeout;
                    }
                )
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                {
                })
                .AddHttpMessageHandler(serviceProvider => new RetryHttpRequestHandler(
                    maxRetries: _parserDeleterConfiguration.ScrapeWebHttpClientConfiguration.MaxRetries
                ));

            services.AddSingleton<IParserDeleterConfiguration, ParserDeleterConfiguration>();
            services.AddValidatorsFromAssembly(typeof(ArticleDataValidator).Assembly);
            services.AddSingleton(serviceProvider => new ApplicationInformation(
                appEnvironment: _parserDeleterConfiguration.AppConfiguration.AppEnvironment,
                version: _parserDeleterConfiguration.AppConfiguration.Version
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
                .AddControllers()
                .AddJsonOptions(jsonOptions =>
                {
                    _parserDeleterConfiguration
                        .SystemTextJsonSerializerConfiguration
                        .MapJsonSerializerOptionsToDefaultOptions(
                            jsonSerializerOptions: jsonOptions.JsonSerializerOptions
                        );
                });

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
               webHookUrl: _parserDeleterConfiguration.AppConfiguration.SlackWebHook,
               applicationInformation: serviceProvider.GetRequiredService<ApplicationInformation>()
           ));
            services.AddScoped<ILoadRssFeedsService, LoadRssFeedsService>();
            services.AddScoped<ICreateArticleService>(serviceProvider => new CreateArticleService(
                webScrapingService: serviceProvider.GetRequiredService<IScrapeWebService>(),
                htmlParsingService: serviceProvider.GetRequiredService<IParseHtmlService>(),
                articleDataValidator: serviceProvider.GetRequiredService<ArticleDataValidator>(),
                loggerService: serviceProvider.GetRequiredService<ILoggerService<CreateArticleService>>(),
                maxAgeOfArticle: _parserDeleterConfiguration.AppConfiguration.MaxAgeOfArticles
            ));
            services.AddScoped<IScrapeWebService, ScrapeWebService>();
            services.AddScoped<IParseHtmlService, ParseHtmlService>();
            services.AddScoped<ISortArticlesService, SortArticlesService>();
            services.AddScoped(typeof(ILoggerService<>), typeof(LoggerService<>));
            services.AddScoped<IGroupSimilarArticlesService>(
                serviceProvider => new GroupSimilarArticlesService(
                    similarityScoreThreshold: _parserDeleterConfiguration.ArticleSimilarityConfiguration.SimilarityScoreThreshold,
                    articlePublishDateTimeDiferenceThreshold: _parserDeleterConfiguration.ArticleSimilarityConfiguration.ArticlePublishDateTimeDiferenceThreshold,
                    loggerService: serviceProvider.GetRequiredService<ILoggerService<GroupSimilarArticlesService>>(),
                    maxAgeOfSimilarArticleChecking: _parserDeleterConfiguration.ArticleSimilarityConfiguration.MaxAgeOfSimilarArticleChecking,
                    minimalNumberOfWordsForArticleToBeComparable: _parserDeleterConfiguration.ArticleSimilarityConfiguration.MinimalNumberOfWordsForArticleToBeComparable
                )
            );
            services.AddScoped<ISendArticlesService>(
                serviceProvider =>
                {
                    return _parserDeleterConfiguration.RabbitMqConfiguration.UseRabbitMqServer ?
                        new SendArticlesRabbitMqService(
                            jsonService: serviceProvider.GetRequiredService<IJsonService>(),
                            hostName: _parserDeleterConfiguration.RabbitMqConfiguration.HostName,
                            queueName: _parserDeleterConfiguration.RabbitMqConfiguration.ArticlesQueueName,
                            port: _parserDeleterConfiguration.RabbitMqConfiguration.Port,
                            username: _parserDeleterConfiguration.RabbitMqConfiguration.Username,
                            password: _parserDeleterConfiguration.RabbitMqConfiguration.Password
                        ) :
                        new SendArticlesHttpService(
                           httpClientFactory: serviceProvider.GetRequiredService<IHttpClientFactory>(),
                           loggerService: serviceProvider.GetRequiredService<ILoggerService<SendArticlesHttpService>>(),
                           slackService: serviceProvider.GetRequiredService<ISlackService>(),
                           jsonService: serviceProvider.GetRequiredService<IJsonService>(),
                           parserApiKey: _parserDeleterConfiguration.ApiKeysConfiguration.ParserApiKey,
                           targetedApiVersion: _parserDeleterConfiguration.AppConfiguration.RssFeedParserMajorMinorVersion,
                           currentVersion: _parserDeleterConfiguration.AppConfiguration.Version,
                           serverUrl: _parserDeleterConfiguration.AppConfiguration.ServerUrl
                        );
                }
            );
            services.AddTransient<IJsonService, SystemTextJsonService>(serviceProvider => new SystemTextJsonService(
                defaultJsonSerializerOptions: _parserDeleterConfiguration.SystemTextJsonSerializerConfiguration.JsonSerializerOptions
            ));
            services.AddScoped<IRemoveOldArticlesService, RemoveOldArticlesService>(
                serviceProvider => new RemoveOldArticlesService(
                    maxAgeOfArticle: _parserDeleterConfiguration.AppConfiguration.MaxAgeOfArticles
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
                     connectionString: _parserDeleterConfiguration.DatabaseConfiguration.ConnectionString,
                     sqlServerOptionsAction: sqlServerOptions =>
                     {
                         sqlServerOptions.CommandTimeout(_parserDeleterConfiguration.DatabaseConfiguration.CommandTimeoutInSeconds);
                     }
                 );
                 options.UseQueryTrackingBehavior(_parserDeleterConfiguration.DatabaseConfiguration.QueryTrackingBehavior);
                 options.EnableDetailedErrors(_parserDeleterConfiguration.DatabaseConfiguration.EnableDetailedErrors);
                 options.EnableSensitiveDataLogging(_parserDeleterConfiguration.DatabaseConfiguration.EnableSensitiveDataLogging);
             });

            services.AddScoped<IDatabaseConnectionFactory>(o => new DatabaseConnectionFactory(
                    connectionString: _parserDeleterConfiguration.DatabaseConfiguration.ConnectionString
                )
            );
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
                cronJobConfiguration.CronExpression = _parserDeleterConfiguration
                    .CronJobsConfiguration
                    .DeleteArticlesCronExpression;
                cronJobConfiguration.TimeZoneInfo = TimeZoneInfo.Utc;
                cronJobConfiguration.AppEnvironment = _parserDeleterConfiguration.AppConfiguration.AppEnvironment;
                cronJobConfiguration.Version = _parserDeleterConfiguration.AppConfiguration.Version;
            });
            services.AddCronJob<ParseArticlesCronJob>(cronJobConfiguration =>
            {
                cronJobConfiguration.CronExpression = _parserDeleterConfiguration
                    .CronJobsConfiguration
                    .ParseArticlesCronExpression;
                cronJobConfiguration.TimeZoneInfo = TimeZoneInfo.Utc;
                cronJobConfiguration.AppEnvironment = _parserDeleterConfiguration.AppConfiguration.AppEnvironment;
                cronJobConfiguration.Version = _parserDeleterConfiguration.AppConfiguration.Version;
            });

            return services;
        }
    }
}
