using Espresso.Application.CQRS.RssFeeds.Commands.ParseRssFeeds;
using Espresso.Application.Infrastructure.MediatorInfrastructure;
using Espresso.Application.Initialization;
using Espresso.Application.IService;
using Espresso.Application.IServices;
using Espresso.Application.Services;
using Espresso.Common.Enums;
using Espresso.Domain.IValidators;
using Espresso.Domain.Validators;
using Espresso.Jobs;
using Espresso.ParserDeleter.Configuration;
using Espresso.Persistence.Database;
using Espresso.Persistence.IRepositories;
using Espresso.Persistence.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            #region Services
            services.AddScoped<ISlackService, SlackService>();
            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped<ILoadRssFeedsService, LoadRssFeedsService>();
            services.AddScoped<IParseArticlesService, ParseArticlesService>();
            services.AddScoped<IScrapeWebService, ScrapeWebService>();
            services.AddScoped<IParseHtmlService, ParseHtmlService>();
            services.AddScoped<ISortArticlesService, SortArticlesService>();
            #endregion

            #region Validators
            services.AddScoped<IArticleValidator, ArticleValidator>();
            #endregion

            #region MemoryCache
            services.AddMemoryCache();
            services.AddTransient<IApplicationInit, ApplicationInit>();
            #endregion

            #region Http
            services.AddHttpClient();
            #endregion

            #region MediatR
            services.AddSignalR();
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
