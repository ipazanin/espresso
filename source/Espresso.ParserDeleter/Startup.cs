using Espresso.Application.Infrastructure.MediatorInfrastructure;
using Espresso.Application.IServices;
using Espresso.Common.Enums;
using Espresso.ParserDeleter.Configuration;
using Espresso.Persistence.Database;
using Espresso.Persistence.IRepositories;
using Espresso.Persistence.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Espresso.ParserDeleter.Application.Initialization;
using Espresso.ParserDeleter.Application.IServices;
using Espresso.Application.Services;
using Espresso.ParserDeleter.Application.Services;
using Espresso.Wepi.Application.IServices;
using Espresso.ParserDeleter.ParseRssFeeds.Validators;
using Espresso.ParserDeleter.ParseRssFeeds;
using Espresso.ParserDeleter.Jobs;
using FluentValidation;

namespace Espresso.ParserDeleter
{
    public class Startup
    {
        #region Fields
        private readonly ParserDeleterConfiguration _parserDeleterConfiguration;
        #endregion

        #region Constructors
        public Startup(IConfiguration configuration)
        {
            _parserDeleterConfiguration = new ParserDeleterConfiguration(configuration);
        }
        #endregion

        #region Methods
        public void ConfigureServices(IServiceCollection services)
        {
            #region Configuration
            services.AddSingleton<IParserDeleterConfiguration, ParserDeleterConfiguration>();
            #endregion

            #region Validators
            services.AddValidatorsFromAssembly(typeof(ArticleDataValidator).Assembly);
            #endregion

            #region Services
            services.AddScoped<ISlackService, SlackService>();
            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped<ILoadRssFeedsService, LoadRssFeedsService>();
            services.AddScoped<ICreateArticlesService, CreateArticlesService>();
            services.AddScoped<IScrapeWebService, ScrapeWebService>();
            services.AddScoped<IParseHtmlService, ParseHtmlService>();
            services.AddScoped<ISortArticlesService, SortArticlesService>();
            #endregion

            #region MemoryCache
            services.AddMemoryCache();
            services.AddTransient<IParserDeleterInit, ParserDeleterInit>();
            #endregion

            #region Http
            services.AddHttpClient();
            #endregion

            #region MediatR
            services.AddMediatR(typeof(ParseRssFeedsCommandHandler).Assembly);
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggerPipelineBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ApplicationLifeTimePipelineBehavior<,>));
            #endregion

            #region Database
            services.AddDbContext<IApplicationDatabaseContext, ApplicationDatabaseContext>(options =>
             {
                 options.UseSqlServer(
                     connectionString: _parserDeleterConfiguration.DatabaseConfiguration.ConnectionString,
                     sqlServerOptionsAction: sqlServerOptions =>
                     {
                         sqlServerOptions.CommandTimeout(_parserDeleterConfiguration.DatabaseConfiguration.CommandTimeoutInSeconds);
                     }
                 );
                 options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                 switch (_parserDeleterConfiguration.AppConfiguration.AppEnvironment)
                 {
                     case AppEnvironment.Undefined:
                     case AppEnvironment.Local:
                     case AppEnvironment.Dev:
                     default:
                         options.EnableDetailedErrors();
                         options.EnableSensitiveDataLogging(true);
                         break;
                     case AppEnvironment.Prod:
                         break;
                 }
             });

            services.AddScoped<IDatabaseConnectionFactory>(o => new DatabaseConnectionFactory(_parserDeleterConfiguration.DatabaseConfiguration.ConnectionString));
            services.AddScoped<IApplicationDownloadRepository, ApplicationDownloadRepository>();
            services.AddScoped<IArticleCategoryRepository, ArticleCategoryRepository>();
            services.AddScoped<IArticleRepository, ArticleRepository>();
            #endregion

            #region Jobs
            services.AddHostedService<ParseArticlesJob>();
            services.AddHostedService<DeleteArticlesJob>();
            #endregion
        }
        #endregion
    }
}
