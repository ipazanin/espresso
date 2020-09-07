using Espresso.WebApi.GraphQl.ApplicationMutations;
using Espresso.WebApi.GraphQl.ApplicationMutations.ArticlesQueries;
using Espresso.WebApi.GraphQl.ApplicationQueries;
using Espresso.WebApi.GraphQl.ApplicationQueries.ArticlesQueries;
using Espresso.WebApi.GraphQl.ApplicationQueries.ConfigurationQueries;
using Espresso.WebApi.GraphQl.ApplicationSchema;
using Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.ConfigurationTypes.GetWebCategoryArticlesTypes;
using Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.GetCategoryArticlesTypes;
using Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.GetFeaturedArticlesTypes;
using Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.GetLatestArticlesTypes;
using Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.GetTrendingArticlesTypes;
using Espresso.WebApi.GraphQl.ApplicationTypes.ConfigurationTypes.GetConfigurationTypes;
using Espresso.WebApi.GraphQl.ApplicationTypes.ConfigurationTypes.GetWebCategoryArticlesTypes;
using GraphQL;
using GraphQL.SystemTextJson;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Espresso.WebApi.GraphQl.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public static class GraphQlServiceCollectionExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddGraphQlServices(
            this IServiceCollection services
        )
        {
            services.AddGraphQlInfrastructure();
            services.AddGraphQlQueries();
            services.AddGraphQlMutations();
            services.AddGraphQlTypes();

            return services;
        }

        private static IServiceCollection AddGraphQlInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ISchema, GraphQlSchema>();
            services.AddScoped<IDocumentExecuter, DocumentExecuter>();
            services.AddScoped<IDocumentWriter, DocumentWriter>();

            services.AddScoped<RootGraphQlQuery>();
            services.AddScoped<RootGraphQlMutation>();

            return services;
        }


        private static IServiceCollection AddGraphQlQueries(this IServiceCollection services)
        {
            services.AddScoped<GetLatestArticlesGraphQlQuery>();
            services.AddScoped<GetFeaturedArticlesGraphQlQuery>();
            services.AddScoped<GetCategoryArticlesGraphQlQuery>();
            services.AddScoped<GetTrendingArticlesGraphQlQuery>();
            services.AddScoped<GetConfigurationGraphQlQuery>();
            services.AddScoped<GetWebConfigurationGraphQlQuery>();

            services.AddScoped<IGraphQlQuery, GetLatestArticlesGraphQlQuery>();
            services.AddScoped<IGraphQlQuery, GetFeaturedArticlesGraphQlQuery>();
            services.AddScoped<IGraphQlQuery, GetCategoryArticlesGraphQlQuery>();
            services.AddScoped<IGraphQlQuery, GetTrendingArticlesGraphQlQuery>();
            services.AddScoped<IGraphQlQuery, GetConfigurationGraphQlQuery>();
            services.AddScoped<IGraphQlQuery, GetWebConfigurationGraphQlQuery>();

            return services;
        }

        private static IServiceCollection AddGraphQlMutations(this IServiceCollection services)
        {
            services.AddScoped<IncrementNumberOfClicksGraphQlMutation>();

            services.AddScoped<IGraphQlMutation, IncrementNumberOfClicksGraphQlMutation>();

            return services;
        }

        private static IServiceCollection AddGraphQlTypes(this IServiceCollection services)
        {
            services.AddGetCategoryArticlesGraphQlTypes();
            services.AddGetLatestArticlesGraphQlTypes();
            services.AddGetFeaturedArticlesGraphQlTypes();
            services.AddGetTrendingArticlesGraphQlTypes();
            services.AddGetConfigurationGraphQlTypes();
            services.AddGetWebConfigurationGraphQlTypes();

            return services;
        }

        private static IServiceCollection AddGetCategoryArticlesGraphQlTypes(this IServiceCollection services)
        {
            services.AddScoped<GetCategoryArticlesArticleType>();
            services.AddScoped<GetCategoryArticlesCategoryType>();
            services.AddScoped<GetCategoryArticlesNewsPortalType>();
            services.AddScoped<GetCategoryArticlesQueryResponseType>();

            return services;
        }

        private static IServiceCollection AddGetLatestArticlesGraphQlTypes(this IServiceCollection services)
        {
            services.AddScoped<GetLatestArticlesArticleType>();
            services.AddScoped<GetLatestArticlesCategoryType>();
            services.AddScoped<GetLatestArticlesNewsPortalType>();
            services.AddScoped<GetLatestArticlesQueryResponseType>();

            return services;
        }

        private static IServiceCollection AddGetFeaturedArticlesGraphQlTypes(this IServiceCollection services)
        {
            services.AddScoped<GetFeaturedArticlesArticleType>();
            services.AddScoped<GetFeaturedArticlesCategoryType>();
            services.AddScoped<GetFeaturedArticlesNewsPortalType>();
            services.AddScoped<GetFeaturedArticlesQueryResponseType>();

            return services;
        }

        private static IServiceCollection AddGetTrendingArticlesGraphQlTypes(this IServiceCollection services)
        {
            services.AddScoped<GetTrendingArticlesArticleType>();
            services.AddScoped<GetTrendingArticlesCategoryType>();
            services.AddScoped<GetTrendingArticlesNewsPortalType>();
            services.AddScoped<GetTrendingArticlesQueryResponseType>();

            return services;
        }

        private static IServiceCollection AddGetConfigurationGraphQlTypes(this IServiceCollection services)
        {
            services.AddScoped<GetConfigurationCategoryType>();
            services.AddScoped<GetConfigurationCategoryWithNewsPortalsType>();
            services.AddScoped<GetConfigurationNewsPortalType>();
            services.AddScoped<GetConfigurationQueryResponseType>();
            services.AddScoped<GetConfigurationRegionType>();

            return services;
        }

        private static IServiceCollection AddGetWebConfigurationGraphQlTypes(this IServiceCollection services)
        {
            services.AddScoped<GetWebConfigurationCategoryType>();
            services.AddScoped<GetWebConfigurationQueryResponseType>();

            return services;
        }
    }
}
