using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Espresso.Application.CQRS.NewsPortals.Queries.GetNewsPortals;
using Espresso.Application.DomainServices;
using Espresso.Application.GraphQl.ApplicationQueries;
using Espresso.Application.GraphQl.ApplicationQueries.ArticlesQueries;
using Espresso.Application.GraphQl.ApplicationQueries.ConfigurationQueries;
using Espresso.Application.GraphQl.ApplicationSchema;
using Espresso.Application.GraphQl.ApplicationTypes.ArticleTypes;
using Espresso.Application.GraphQl.ApplicationTypes.ConfigurationTypes;
using Espresso.Application.Hubs;
using Espresso.Application.Infrastructure;
using Espresso.Application.Initialization;
using Espresso.Common.Configuration;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.DataAccessLayer.IRepository;
using Espresso.DataAccessLayer.Repository;
using Espresso.Domain.IServices;
using Espresso.Domain.IValidators;
using Espresso.Domain.Services;
using Espresso.Domain.Validators;
using Espresso.Persistence.Database;
using Espresso.WebApi.Authentication;
using Espresso.WebApi.Configuration;
using Espresso.WebApi.Controllers;
using Espresso.WebApi.Filters;
using Espresso.WebApi.Middleware;
using FluentValidation.AspNetCore;
using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Espresso.WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        #region Fields
        private readonly IWebApiConfiguration _configuration;
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public Startup(IConfiguration configuration)
        {
            _configuration = new WebApiConfiguration(configuration);
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            #region Configuration
            services.AddTransient<IWebApiConfiguration, WebApiConfiguration>();
            services.AddTransient<ICommonConfiguration, WebApiConfiguration>();
            #endregion

            #region Services
            services.AddScoped<ILoggerService, LoggerService>();
            services.AddScoped<ISlackService, SlackService>();
            services.AddScoped<IArticleParserService, ArticleParserService>();
            services.AddScoped<IWebScrapingService, WebScrapingService>();
            services.AddScoped<IHttpService, HttpService>();
            #endregion

            #region MemoryCache
            services.AddMemoryCache();
            services.AddTransient<IApplicationInit, ApplicationInit>();
            #endregion

            #region Validators
            services.AddScoped<IArticleValidator, ArticleValidator>();
            #endregion

            #region MediatR
            services.AddMediatR(typeof(GetNewsPortalsQuery).GetTypeInfo().Assembly);
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggerPipelineBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ApplicationLifeTimePipelineBehavior<,>));
            #endregion

            #region Web Server
            services.AddHttpClient();
            services.AddControllers();

            services.AddApiVersioning(ConfigureApiVersioning);

            services
                .AddMvc(options =>
                {
                    options.Filters.Add(typeof(CustomExceptionFilterAttribute));
                    options.EnableEndpointRouting = false;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddFluentValidation(fluentValidatorConfiguration => fluentValidatorConfiguration.RegisterValidatorsFromAssemblyContaining<GetNewsPortalsQueryValidator>());

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
            #endregion

            #region Authentification
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = ApiKeyAuthenticationOptions.DefaultScheme;
                options.DefaultChallengeScheme = ApiKeyAuthenticationOptions.DefaultScheme;
            })
            .AddApiKeySupport(options => { });
            services.AddTransient<IApiKeyProvider, InMemoryApiKeyProvider>();
            #endregion

            #region Health Checks
            services.AddHealthChecks();
            #endregion


            #region WebSockets
            services.AddSignalR();
            #endregion

            #region GraphQL
            services.AddScoped<ISchema, GraphQlSchema>();
            services.AddScoped<IDependencyResolver>(
                serviceProvider => new FuncDependencyResolver(serviceProvider.GetRequiredService)
            );
            services.AddScoped<IDocumentExecuter, DocumentExecuter>();
            services.AddScoped<IDocumentWriter, DocumentWriter>();

            services.AddScoped<RootGraphQlQuery>();

            services.AddScoped<GetLatestArticlesGraphQlQuery>();
            services.AddScoped<GetCategoryArticlesGraphQlQuery>();
            services.AddScoped<GetTrendingArticlesGraphQlQuery>();
            services.AddScoped<GetConfigurationGraphQlQuery>();
            services.AddScoped<IncrementNumberOfClicksGraphQlMutation>();

            services.AddScoped<IGraphQlQuery, GetLatestArticlesGraphQlQuery>();
            services.AddScoped<IGraphQlQuery, GetCategoryArticlesGraphQlQuery>();
            services.AddScoped<IGraphQlQuery, GetTrendingArticlesGraphQlQuery>();
            services.AddScoped<IGraphQlQuery, GetConfigurationGraphQlQuery>();
            services.AddScoped<IGraphQlQuery, IncrementNumberOfClicksGraphQlMutation>();

            services.AddScoped<GetLatestArticlesQueryResponseType>();
            services.AddScoped<GetCategoryArticlesQueryResponseType>();
            services.AddScoped<GetTrendingArticlesQueryResponseType>();
            services.AddScoped<GetConfigurationQueryResponseType>();

            services.AddScoped<CategoryViewModelWithNewsPortalsType>();
            services.AddScoped<ArticleViewModelType>();
            services.AddScoped<ArticleTrendingViewModelType>();
            services.AddScoped<CategoryViewModelType>();
            services.AddScoped<NewsPortalViewModelType>();
            #endregion

            #region Swagger
            services.AddSwaggerGen(ConfigureSwagger);
            #endregion

            #region Database
            services.AddDbContext<IEspressoDatabaseContext, EspressoDatabaseContext>(options =>
            {
                options.UseSqlServer(_configuration.ConnectionString);
                switch (_configuration.AppEnvironment)
                {
                    case AppEnvironment.Undefined:
                    case AppEnvironment.Local:
                    case AppEnvironment.Dev:
                    default:
                        options.EnableDetailedErrors();
                        options.UseLoggerFactory(LoggerFactory.Create(builder =>
                        {
                            builder.AddConsole();
                        }));
                        options.EnableSensitiveDataLogging(true);
                        break;
                    case AppEnvironment.Prod:
                        break;
                }
            });

            services.AddScoped<IDatabaseConnectionFactory>(serviceProvider => new DatabaseConnectionFactory(_configuration.ConnectionString));
            services.AddScoped<IApplicationDownloadRepository, ApplicationDownloadRepository>();
            services.AddScoped<IArticleCategoryRepository, ArticleCategoryRepository>();
            services.AddScoped<IArticleRepository, ArticleRepository>();
            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="memoryCacheInit"></param>
        /// <param name="env"></param>
        public void Configure(
            IApplicationBuilder app,
            IApplicationInit memoryCacheInit,
            IWebHostEnvironment env
        )
        {
            memoryCacheInit.InitWebApi().GetAwaiter().GetResult();

            app.UseSecurityHeadersMiddleware(securityHeadersBuilder =>
            {
                securityHeadersBuilder.AddDefaultSecurePolicy();
            });

            app.UseHsts();

            #region Swagger
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(
                    url: $"/swagger/{_configuration.EspressoWebApiCurrentVersion}/swagger.json",
                    name: $"Espresso API {_configuration.EspressoWebApiCurrentVersion}"
                );
                options.SwaggerEndpoint(
                    url: $"/swagger/{_configuration.EspressoWebApiVersion_1_3}/swagger.json",
                    name: $"Espresso API {_configuration.EspressoWebApiVersion_1_3}"
                );
                options.SwaggerEndpoint(
                    url: $"/swagger/{_configuration.EspressoWebApiVersion_1_2}/swagger.json",
                    name: $"Espresso API {_configuration.EspressoWebApiVersion_1_2}"
                );
                options.RoutePrefix = "docs";
            });
            #endregion

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseMvc();
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = Path.Join(env.ContentRootPath, "ClientApp");

                // switch (_configuration.AppEnvironment)
                // {
                //     case AppEnvironment.Undefined:
                //     case AppEnvironment.Dev:
                //     case AppEnvironment.Prod:
                //     default:
                //         break;
                //     case AppEnvironment.Local:
                //         spa.UseReactDevelopmentServer(npmScript: "start");
                //         break;
                // }
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
                endpoints.MapHub<ArticlesNotificationHub>("/notifications/articles");
            });
        }

        private void ConfigureSwagger(SwaggerGenOptions options)
        {
            options.SwaggerDoc(_configuration.EspressoWebApiCurrentVersion.ToString(), new OpenApiInfo
            {
                Title = $"Espresso API",
                Version = _configuration.EspressoWebApiCurrentVersion.ToString(),
                Description = "Espresso APP Web Api"
            });

            options.SwaggerDoc(_configuration.EspressoWebApiVersion_1_3.ToString(), new OpenApiInfo
            {
                Title = $"Espresso API",
                Version = _configuration.EspressoWebApiVersion_1_3.ToString(),
                Description = "Espresso APP Web Api"
            });

            options.SwaggerDoc(_configuration.EspressoWebApiVersion_1_2.ToString(), new OpenApiInfo
            {
                Title = $"Espresso API",
                Version = _configuration.EspressoWebApiVersion_1_2.ToString(),
                Description = "Espresso APP Web Api"
            });

            options.DocInclusionPredicate((version, desc) =>
            {
                var apiVersions = desc
                    .ActionDescriptor
                    .GetApiVersionModel()
                    .DeclaredApiVersions
                    .Select(supportedApiVersion => supportedApiVersion.ToString());

                return apiVersions.Any(v => version.Equals(v));
            });

            options.AddSecurityDefinition(HttpHeaderConstants.HeaderName, new OpenApiSecurityScheme
            {
                Description = "API Key",
                In = ParameterLocation.Header,
                Name = HttpHeaderConstants.HeaderName,
                Type = SecuritySchemeType.ApiKey
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = HttpHeaderConstants.HeaderName,
                            Type = SecuritySchemeType.ApiKey,
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = HttpHeaderConstants.HeaderName
                            },
                        },
                        new string[] {}
                    }
                });

            // Set the comments path for the Swagger JSON and UI.
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        }

        /// <summary>
        /// https://dev.to/htissink/versioning-asp-net-core-apis-with-swashbuckle-making-space-potatoes-v-x-x-x-3po7
        /// </summary>
        /// <param name="apiVersioningOptions"></param>
        private void ConfigureApiVersioning(ApiVersioningOptions apiVersioningOptions)
        {
            apiVersioningOptions.DefaultApiVersion = new ApiVersion(
                majorVersion: ApiVersionConstants.DefaultMajorVersion,
                minorVersion: ApiVersionConstants.DefaultMinorVersion
            );
            apiVersioningOptions.AssumeDefaultVersionWhenUnspecified = true;
            apiVersioningOptions.ReportApiVersions = true;
            apiVersioningOptions.ApiVersionReader = new HeaderApiVersionReader(HttpHeaderConstants.EspressoApiHeaderName);

            #region ApplicationDownloads Controller
            apiVersioningOptions
                .Conventions
                .Controller<ApplicationDownloadsController>()
                .Action(typeof(ApplicationDownloadsController)
                .GetMethod(nameof(ApplicationDownloadsController.CreateApplicationDownload))!)
                .HasApiVersion(_configuration.EspressoWebApiCurrentVersion);
            apiVersioningOptions
                .Conventions
                .Controller<ApplicationDownloadsController>()
                .Action(typeof(ApplicationDownloadsController)
                .GetMethod(nameof(ApplicationDownloadsController.CreateApplicationDownload))!)
                .HasApiVersion(_configuration.EspressoWebApiVersion_1_3);
            apiVersioningOptions
                .Conventions
                .Controller<ApplicationDownloadsController>()
                .Action(typeof(ApplicationDownloadsController)
                .GetMethod(nameof(ApplicationDownloadsController.CreateApplicationDownload_1_2))!)
                .HasApiVersion(_configuration.EspressoWebApiVersion_1_2);

            apiVersioningOptions
                .Conventions
                .Controller<ApplicationDownloadsController>()
                .Action(typeof(ApplicationDownloadsController)
                .GetMethod(nameof(ApplicationDownloadsController.GetApplicationDownloadsStatistics))!)
                .HasApiVersion(_configuration.EspressoWebApiCurrentVersion);
            apiVersioningOptions
                .Conventions
                .Controller<ApplicationDownloadsController>()
                .Action(typeof(ApplicationDownloadsController)
                .GetMethod(nameof(ApplicationDownloadsController.GetApplicationDownloadsStatistics))!)
                .HasApiVersion(_configuration.EspressoWebApiVersion_1_3);
            apiVersioningOptions
                .Conventions
                .Controller<ApplicationDownloadsController>()
                .Action(typeof(ApplicationDownloadsController)
                .GetMethod(nameof(ApplicationDownloadsController.GetApplicationDownloadsStatistics))!)
                .HasApiVersion(_configuration.EspressoWebApiVersion_1_2);
            #endregion

            #region Articles Controller

            #region GetCategoryArticles
            apiVersioningOptions
                .Conventions
                .Controller<ArticlesController>()
                .Action(typeof(ArticlesController)
                .GetMethod(nameof(ArticlesController.GetCategoryArticles))!)
                .HasApiVersion(_configuration.EspressoWebApiCurrentVersion);
            apiVersioningOptions
                .Conventions
                .Controller<ArticlesController>()
                .Action(typeof(ArticlesController)
                .GetMethod(nameof(ArticlesController.GetCategoryArticles_1_3))!)
                .HasApiVersion(_configuration.EspressoWebApiVersion_1_3);
            apiVersioningOptions
                .Conventions
                .Controller<ArticlesController>()
                .Action(typeof(ArticlesController)
                .GetMethod(nameof(ArticlesController.GetCategoryArticles_1_3))!)
                .HasApiVersion(_configuration.EspressoWebApiVersion_1_2);
            #endregion

            #region GetLatestArticles
            apiVersioningOptions
                .Conventions
                .Controller<ArticlesController>()
                .Action(typeof(ArticlesController)
                .GetMethod(nameof(ArticlesController.GetLatestArticles))!)
                .HasApiVersion(_configuration.EspressoWebApiCurrentVersion);
            apiVersioningOptions
                .Conventions
                .Controller<ArticlesController>()
                .Action(typeof(ArticlesController)
                .GetMethod(nameof(ArticlesController.GetLatestArticles))!)
                .HasApiVersion(_configuration.EspressoWebApiVersion_1_3);
            apiVersioningOptions
                .Conventions
                .Controller<ArticlesController>()
                .Action(typeof(ArticlesController)
                .GetMethod(nameof(ArticlesController.GetLatestArticles))!)
                .HasApiVersion(_configuration.EspressoWebApiVersion_1_2);
            #endregion

            #region GetTrendingArticles
            apiVersioningOptions
                .Conventions
                .Controller<ArticlesController>()
                .Action(typeof(ArticlesController)
                .GetMethod(nameof(ArticlesController.GetTrendingArticles))!)
                .HasApiVersion(_configuration.EspressoWebApiCurrentVersion);
            apiVersioningOptions
                .Conventions
                .Controller<ArticlesController>()
                .Action(typeof(ArticlesController)
                .GetMethod(nameof(ArticlesController.GetTrendingArticles))!)
                .HasApiVersion(_configuration.EspressoWebApiVersion_1_3);
            apiVersioningOptions
                .Conventions
                .Controller<ArticlesController>()
                .Action(typeof(ArticlesController)
                .GetMethod(nameof(ArticlesController.GetTrendingArticles))!)
                .HasApiVersion(_configuration.EspressoWebApiVersion_1_2);
            #endregion

            #region IncrementArticleScore
            apiVersioningOptions
                .Conventions
                .Controller<ArticlesController>()
                .Action(typeof(ArticlesController)
                .GetMethod(nameof(ArticlesController.IncrementArticleScore))!)
                .HasApiVersion(_configuration.EspressoWebApiCurrentVersion);
            apiVersioningOptions
                .Conventions
                .Controller<ArticlesController>()
                .Action(typeof(ArticlesController)
                .GetMethod(nameof(ArticlesController.IncrementArticleScore))!)
                .HasApiVersion(_configuration.EspressoWebApiVersion_1_3);
            apiVersioningOptions
                .Conventions
                .Controller<ArticlesController>()
                .Action(typeof(ArticlesController)
                .GetMethod(nameof(ArticlesController.IncrementArticleScore))!)
                .HasApiVersion(_configuration.EspressoWebApiVersion_1_2);
            #endregion

            #region HideArticles
            apiVersioningOptions
                .Conventions
                .Controller<ArticlesController>()
                .Action(typeof(ArticlesController)
                .GetMethod(nameof(ArticlesController.HideArticle))!)
                .HasApiVersion(_configuration.EspressoWebApiCurrentVersion);
            #endregion

            #endregion

            #region Notifications Controller

            #region SendLatestArticlesNotificition
            apiVersioningOptions
                .Conventions
                .Controller<NotificationsController>()
                .Action(typeof(NotificationsController)
                .GetMethod(nameof(NotificationsController.SendLatestArticlesNotificition))!)
                .HasApiVersion(_configuration.EspressoWebApiCurrentVersion);
            apiVersioningOptions
                .Conventions
                .Controller<NotificationsController>()
                .Action(typeof(NotificationsController)
                .GetMethod(nameof(NotificationsController.SendLatestArticlesNotificition))!)
                .HasApiVersion(_configuration.EspressoWebApiVersion_1_3);
            apiVersioningOptions
                .Conventions
                .Controller<NotificationsController>()
                .Action(typeof(NotificationsController)
                .GetMethod(nameof(NotificationsController.SendLatestArticlesNotificition))!)
                .HasApiVersion(_configuration.EspressoWebApiVersion_1_2);

            #endregion

            #region SendPushNotification   
            apiVersioningOptions
                .Conventions
                .Controller<NotificationsController>()
                .Action(typeof(NotificationsController)
                .GetMethod(nameof(NotificationsController.SendPushNotificition))!)
                .HasApiVersion(_configuration.EspressoWebApiCurrentVersion);
            #endregion

            #region GetPushNotifications
            apiVersioningOptions
                .Conventions
                .Controller<NotificationsController>()
                .Action(typeof(NotificationsController)
                .GetMethod(nameof(NotificationsController.GetPushNotificition))!)
                .HasApiVersion(_configuration.EspressoWebApiCurrentVersion);
            #endregion

            #endregion

            #region Categories Controller
            apiVersioningOptions
                .Conventions
                .Controller<CategoriesController>()
                .Action(typeof(CategoriesController)
                .GetMethod(nameof(CategoriesController.GetCategories))!)
                .HasApiVersion(_configuration.EspressoWebApiCurrentVersion);
            apiVersioningOptions
                .Conventions
                .Controller<CategoriesController>()
                .Action(typeof(CategoriesController)
                .GetMethod(nameof(CategoriesController.GetCategories))!)
                .HasApiVersion(_configuration.EspressoWebApiVersion_1_3);
            apiVersioningOptions
                .Conventions
                .Controller<CategoriesController>()
                .Action(typeof(CategoriesController)
                .GetMethod(nameof(CategoriesController.GetCategories))!)
                .HasApiVersion(_configuration.EspressoWebApiVersion_1_2);
            #endregion

            #region Configuration Controller

            #region GetConfiguration
            apiVersioningOptions
                .Conventions
                .Controller<ConfigurationController>()
                .Action(typeof(ConfigurationController)
                .GetMethod(nameof(ConfigurationController.GetConfiguration))!)
                .HasApiVersion(_configuration.EspressoWebApiCurrentVersion);
            apiVersioningOptions
                .Conventions
                .Controller<ConfigurationController>()
                .Action(typeof(ConfigurationController)
                .GetMethod(nameof(ConfigurationController.GetConfiguration_1_3))!)
                .HasApiVersion(_configuration.EspressoWebApiVersion_1_3);
            apiVersioningOptions
                .Conventions
                .Controller<ConfigurationController>()
                .Action(typeof(ConfigurationController)
                .GetMethod(nameof(ConfigurationController.GetConfiguration_1_2))!)
                .HasApiVersion(_configuration.EspressoWebApiVersion_1_2);
            #endregion

            #endregion

            #region NewsPortalsController

            #region GetNewsPortals
            apiVersioningOptions
                .Conventions
                .Controller<NewsPortalsController>()
                .Action(typeof(NewsPortalsController)
                .GetMethod(nameof(NewsPortalsController.GetNewsPortals))!)
                .HasApiVersion(_configuration.EspressoWebApiCurrentVersion);
            apiVersioningOptions
                .Conventions
                .Controller<NewsPortalsController>()
                .Action(typeof(NewsPortalsController)
                .GetMethod(nameof(NewsPortalsController.GetNewsPortals))!)
                .HasApiVersion(_configuration.EspressoWebApiVersion_1_3);
            apiVersioningOptions
                .Conventions
                .Controller<NewsPortalsController>()
                .Action(typeof(NewsPortalsController)
                .GetMethod(nameof(NewsPortalsController.GetNewsPortals))!)
                .HasApiVersion(_configuration.EspressoWebApiVersion_1_2);
            #endregion

            #region RequestNewsPortal
            apiVersioningOptions
                .Conventions
                .Controller<NewsPortalsController>()
                .Action(typeof(NewsPortalsController)
                .GetMethod(nameof(NewsPortalsController.RequestNewsPortal))!)
                .HasApiVersion(_configuration.EspressoWebApiCurrentVersion);
            #endregion

            #endregion

            #region GraphQlController

            #region Post
            apiVersioningOptions
                .Conventions
                .Controller<GraphQlController>()
                .Action(typeof(GraphQlController)
                .GetMethod(nameof(GraphQlController.Post))!)
                .HasApiVersion(_configuration.EspressoWebApiCurrentVersion);
            #endregion

            #endregion
        }
        #endregion
    }
}
