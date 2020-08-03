using Espresso.Common.Constants;
using Espresso.WebApi.Configuration;
using Espresso.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace Espresso.WebApi.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ApiVersioningServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddApiVersioningServices(this IServiceCollection services, IWebApiConfiguration configuration)
        {
            services.AddApiVersioning(options => ConfigureApiVersioning(options, configuration));

            return services;
        }

        /// <summary>
        /// https://dev.to/htissink/versioning-asp-net-core-apis-with-swashbuckle-making-space-potatoes-v-x-x-x-3po7
        /// </summary>
        private static void ConfigureApiVersioning(ApiVersioningOptions apiVersioningOptions, IWebApiConfiguration configuration)
        {
            apiVersioningOptions.DefaultApiVersion = new ApiVersion(
                majorVersion: ApiVersionConstants.DefaultMajorVersion,
                minorVersion: ApiVersionConstants.DefaultMinorVersion
            );
            apiVersioningOptions.AssumeDefaultVersionWhenUnspecified = true;
            apiVersioningOptions.ReportApiVersions = true;
            apiVersioningOptions.ApiVersionReader = new HeaderApiVersionReader(HttpHeaderConstants.EspressoApiHeaderName);

            ConfigureApplicationDownloadsControllerVersioning(apiVersioningOptions, configuration);
            ConfigureArticlesControllerVersioning(apiVersioningOptions, configuration);
            ConfigureNotificationsControllerVersioning(apiVersioningOptions, configuration);
            ConfigureCategoriesControllerVersioning(apiVersioningOptions, configuration);
            ConfigureConfigurationControllerVersioning(apiVersioningOptions, configuration);
            ConfigureNewsPortalsControllerVersioning(apiVersioningOptions, configuration);
            ConfigureGraphQlControllerVersioning(apiVersioningOptions, configuration);
        }

        private static void ConfigureApplicationDownloadsControllerVersioning(ApiVersioningOptions apiVersioningOptions, IWebApiConfiguration configuration)
        {
            apiVersioningOptions
                .Conventions
                .Controller<ApplicationDownloadsController>()
                .Action(typeof(ApplicationDownloadsController)
                .GetMethod(nameof(ApplicationDownloadsController.CreateApplicationDownload))!)
                .HasApiVersion(configuration.EspressoWebApiCurrentVersion);
            apiVersioningOptions
                .Conventions
                .Controller<ApplicationDownloadsController>()
                .Action(typeof(ApplicationDownloadsController)
                .GetMethod(nameof(ApplicationDownloadsController.CreateApplicationDownload_1_3))!)
                .HasApiVersion(configuration.EspressoWebApiVersion_1_3);
            apiVersioningOptions
                .Conventions
                .Controller<ApplicationDownloadsController>()
                .Action(typeof(ApplicationDownloadsController)
                .GetMethod(nameof(ApplicationDownloadsController.CreateApplicationDownload_1_2))!)
                .HasApiVersion(configuration.EspressoWebApiVersion_1_2);

            apiVersioningOptions
                .Conventions
                .Controller<ApplicationDownloadsController>()
                .Action(typeof(ApplicationDownloadsController)
                .GetMethod(nameof(ApplicationDownloadsController.GetApplicationDownloadsStatistics))!)
                .HasApiVersion(configuration.EspressoWebApiCurrentVersion);
            apiVersioningOptions
                .Conventions
                .Controller<ApplicationDownloadsController>()
                .Action(typeof(ApplicationDownloadsController)
                .GetMethod(nameof(ApplicationDownloadsController.GetApplicationDownloadsStatistics))!)
                .HasApiVersion(configuration.EspressoWebApiVersion_1_3);
            apiVersioningOptions
                .Conventions
                .Controller<ApplicationDownloadsController>()
                .Action(typeof(ApplicationDownloadsController)
                .GetMethod(nameof(ApplicationDownloadsController.GetApplicationDownloadsStatistics))!)
                .HasApiVersion(configuration.EspressoWebApiVersion_1_2);
        }

        private static void ConfigureArticlesControllerVersioning(ApiVersioningOptions apiVersioningOptions, IWebApiConfiguration configuration)
        {
            #region GetCategoryArticles
            apiVersioningOptions
                .Conventions
                .Controller<ArticlesController>()
                .Action(typeof(ArticlesController)
                .GetMethod(nameof(ArticlesController.GetCategoryArticles))!)
                .HasApiVersion(configuration.EspressoWebApiCurrentVersion);
            apiVersioningOptions
                .Conventions
                .Controller<ArticlesController>()
                .Action(typeof(ArticlesController)
                .GetMethod(nameof(ArticlesController.GetCategoryArticles_1_3))!)
                .HasApiVersion(configuration.EspressoWebApiVersion_1_3);
            apiVersioningOptions
                .Conventions
                .Controller<ArticlesController>()
                .Action(typeof(ArticlesController)
                .GetMethod(nameof(ArticlesController.GetCategoryArticles_1_3))!)
                .HasApiVersion(configuration.EspressoWebApiVersion_1_2);
            #endregion

            #region GetLatestArticles
            apiVersioningOptions
                .Conventions
                .Controller<ArticlesController>()
                .Action(typeof(ArticlesController)
                .GetMethod(nameof(ArticlesController.GetLatestArticles))!)
                .HasApiVersion(configuration.EspressoWebApiCurrentVersion);
            apiVersioningOptions
                .Conventions
                .Controller<ArticlesController>()
                .Action(typeof(ArticlesController)
                .GetMethod(nameof(ArticlesController.GetLatestArticles_1_3))!)
                .HasApiVersion(configuration.EspressoWebApiVersion_1_3);
            apiVersioningOptions
                .Conventions
                .Controller<ArticlesController>()
                .Action(typeof(ArticlesController)
                .GetMethod(nameof(ArticlesController.GetLatestArticles_1_3))!)
                .HasApiVersion(configuration.EspressoWebApiVersion_1_2);
            #endregion

            #region GetTrendingArticles
            apiVersioningOptions
                .Conventions
                .Controller<ArticlesController>()
                .Action(typeof(ArticlesController)
                .GetMethod(nameof(ArticlesController.GetTrendingArticles))!)
                .HasApiVersion(configuration.EspressoWebApiCurrentVersion);
            apiVersioningOptions
                .Conventions
                .Controller<ArticlesController>()
                .Action(typeof(ArticlesController)
                .GetMethod(nameof(ArticlesController.GetTrendingArticles))!)
                .HasApiVersion(configuration.EspressoWebApiVersion_1_3);
            apiVersioningOptions
                .Conventions
                .Controller<ArticlesController>()
                .Action(typeof(ArticlesController)
                .GetMethod(nameof(ArticlesController.GetTrendingArticles))!)
                .HasApiVersion(configuration.EspressoWebApiVersion_1_2);
            #endregion

            #region IncrementArticleScore
            apiVersioningOptions
                .Conventions
                .Controller<ArticlesController>()
                .Action(typeof(ArticlesController)
                .GetMethod(nameof(ArticlesController.IncrementArticleScore))!)
                .HasApiVersion(configuration.EspressoWebApiCurrentVersion);
            apiVersioningOptions
                .Conventions
                .Controller<ArticlesController>()
                .Action(typeof(ArticlesController)
                .GetMethod(nameof(ArticlesController.IncrementArticleScore_1_3))!)
                .HasApiVersion(configuration.EspressoWebApiVersion_1_3);
            apiVersioningOptions
                .Conventions
                .Controller<ArticlesController>()
                .Action(typeof(ArticlesController)
                .GetMethod(nameof(ArticlesController.IncrementArticleScore_1_3))!)
                .HasApiVersion(configuration.EspressoWebApiVersion_1_2);
            #endregion

            #region HideArticles
            apiVersioningOptions
                .Conventions
                .Controller<ArticlesController>()
                .Action(typeof(ArticlesController)
                .GetMethod(nameof(ArticlesController.HideArticle))!)
                .HasApiVersion(configuration.EspressoWebApiCurrentVersion);
            #endregion
        }

        private static void ConfigureNotificationsControllerVersioning(ApiVersioningOptions apiVersioningOptions, IWebApiConfiguration configuration)
        {
            #region SendLatestArticlesNotificition
            apiVersioningOptions
                .Conventions
                .Controller<NotificationsController>()
                .Action(typeof(NotificationsController)
                .GetMethod(nameof(NotificationsController.SendLatestArticlesNotificition))!)
                .HasApiVersion(configuration.EspressoWebApiCurrentVersion);
            #endregion

            #region SendPushNotification   
            apiVersioningOptions
                .Conventions
                .Controller<NotificationsController>()
                .Action(typeof(NotificationsController)
                .GetMethod(nameof(NotificationsController.SendPushNotificition))!)
                .HasApiVersion(configuration.EspressoWebApiCurrentVersion);
            #endregion

            #region GetPushNotifications
            apiVersioningOptions
                .Conventions
                .Controller<NotificationsController>()
                .Action(typeof(NotificationsController)
                .GetMethod(nameof(NotificationsController.GetPushNotificition))!)
                .HasApiVersion(configuration.EspressoWebApiCurrentVersion);
            #endregion
        }

        private static void ConfigureCategoriesControllerVersioning(ApiVersioningOptions apiVersioningOptions, IWebApiConfiguration configuration)
        {
            apiVersioningOptions
                .Conventions
                .Controller<CategoriesController>()
                .Action(typeof(CategoriesController)
                .GetMethod(nameof(CategoriesController.GetCategories))!)
                .HasApiVersion(configuration.EspressoWebApiVersion_1_3);
            apiVersioningOptions
                .Conventions
                .Controller<CategoriesController>()
                .Action(typeof(CategoriesController)
                .GetMethod(nameof(CategoriesController.GetCategories))!)
                .HasApiVersion(configuration.EspressoWebApiVersion_1_2);
        }

        private static void ConfigureConfigurationControllerVersioning(ApiVersioningOptions apiVersioningOptions, IWebApiConfiguration configuration)
        {
            apiVersioningOptions
                .Conventions
                .Controller<ConfigurationController>()
                .Action(typeof(ConfigurationController)
                .GetMethod(nameof(ConfigurationController.GetConfiguration))!)
                .HasApiVersion(configuration.EspressoWebApiCurrentVersion);
            apiVersioningOptions
                .Conventions
                .Controller<ConfigurationController>()
                .Action(typeof(ConfigurationController)
                .GetMethod(nameof(ConfigurationController.GetConfiguration_1_3))!)
                .HasApiVersion(configuration.EspressoWebApiVersion_1_3);
            apiVersioningOptions
                .Conventions
                .Controller<ConfigurationController>()
                .Action(typeof(ConfigurationController)
                .GetMethod(nameof(ConfigurationController.GetConfiguration_1_2))!)
                .HasApiVersion(configuration.EspressoWebApiVersion_1_2);
        }

        private static void ConfigureNewsPortalsControllerVersioning(ApiVersioningOptions apiVersioningOptions, IWebApiConfiguration configuration)
        {
            #region GetNewsPortals
            apiVersioningOptions
                .Conventions
                .Controller<NewsPortalsController>()
                .Action(typeof(NewsPortalsController)
                .GetMethod(nameof(NewsPortalsController.GetNewsPortals))!)
                .HasApiVersion(configuration.EspressoWebApiVersion_1_3);
            apiVersioningOptions
                .Conventions
                .Controller<NewsPortalsController>()
                .Action(typeof(NewsPortalsController)
                .GetMethod(nameof(NewsPortalsController.GetNewsPortals))!)
                .HasApiVersion(configuration.EspressoWebApiVersion_1_2);
            #endregion

            #region RequestNewsPortal
            apiVersioningOptions
                .Conventions
                .Controller<NewsPortalsController>()
                .Action(typeof(NewsPortalsController)
                .GetMethod(nameof(NewsPortalsController.RequestNewsPortal))!)
                .HasApiVersion(configuration.EspressoWebApiCurrentVersion);
            #endregion
        }

        private static void ConfigureGraphQlControllerVersioning(ApiVersioningOptions apiVersioningOptions, IWebApiConfiguration configuration)
        {
            apiVersioningOptions
                .Conventions
                .Controller<GraphQlController>()
                .Action(typeof(GraphQlController)
                .GetMethod(nameof(GraphQlController.Post))!)
                .HasApiVersion(configuration.EspressoWebApiCurrentVersion);
        }
    }
}
