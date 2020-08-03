using Espresso.Common.Enums;

using Espresso.Persistence.Database;
using Espresso.Persistence.IRepository;
using Espresso.Persistence.Repository;
using Espresso.WebApi.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Espresso.WebApi.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class PersistenceServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddPersistentServices(this IServiceCollection services, IWebApiConfiguration configuration)
        {
            services.AddPersistenceConfiguration(configuration);
            services.AddRepositories();

            return services;
        }

        private static IServiceCollection AddPersistenceConfiguration(this IServiceCollection services, IWebApiConfiguration configuration)
        {
            services.AddDbContext<IApplicationDatabaseContext, ApplicationDatabaseContext>(options =>
            {
                options.UseSqlServer(configuration.ConnectionString);
                switch (configuration.AppEnvironment)
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

            services.AddScoped<IDatabaseConnectionFactory>(serviceProvider => new DatabaseConnectionFactory(configuration.ConnectionString));

            return services;
        }


        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IApplicationDownloadRepository, ApplicationDownloadRepository>();
            services.AddScoped<IArticleCategoryRepository, ArticleCategoryRepository>();
            services.AddScoped<IArticleRepository, ArticleRepository>();

            return services;
        }

    }
}
