using Espresso.Application.GraphQl.ApplicationQueries;
using Espresso.Application.GraphQl.ApplicationQueries.ArticlesQueries;
using Espresso.Application.GraphQl.ApplicationQueries.ConfigurationQueries;
using Espresso.Application.GraphQl.ApplicationSchema;
using Espresso.Application.GraphQl.ApplicationTypes.ArticleTypes.GetCategoryArticlesTypes;
using Espresso.Application.GraphQl.ApplicationTypes.ArticleTypes.GetLatestArticlesTypes;
using Espresso.Application.GraphQl.ApplicationTypes.ArticleTypes.GetTrendingArticlesTypes;
using Espresso.Application.GraphQl.ApplicationTypes.ConfigurationTypes;
using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Espresso.Application.GraphQl.Infrastructure
{
    public static class GraphQlServiceCollectionExtension
    {
        public static IServiceCollection AddGraphQlServices(
            this IServiceCollection services
        )
        {
            services.AddGraphQlInfrastructure();
            services.AddGraphQlQueries();
            services.AddGraphQlTypes();

            return services;
        }

        private static IServiceCollection AddGraphQlInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ISchema, GraphQlSchema>();
            services.AddScoped<IDependencyResolver>(
                serviceProvider => new FuncDependencyResolver(serviceProvider.GetRequiredService)
            );
            services.AddScoped<IDocumentExecuter, DocumentExecuter>();
            services.AddScoped<IDocumentWriter, DocumentWriter>();

            services.AddScoped<RootGraphQlQuery>();

            return services;
        }


        private static IServiceCollection AddGraphQlQueries(this IServiceCollection services)
        {
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

            return services;
        }

        private static IServiceCollection AddGraphQlTypes(this IServiceCollection services)
        {
            services.AddGetCategoryArticlesGraphQlTypes();
            services.AddGetLatestArticlesGraphQlTypes();
            services.AddGetTrendingArticlesGraphQlTypes();
            services.AddConfigurationGraphQlTypes();

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

        private static IServiceCollection AddGetTrendingArticlesGraphQlTypes(this IServiceCollection services)
        {
            services.AddScoped<GetTrendingArticlesArticleType>();
            services.AddScoped<GetTrendingArticlesCategoryType>();
            services.AddScoped<GetTrendingArticlesNewsPortalType>();
            services.AddScoped<GetTrendingArticlesQueryResponseType>();

            return services;
        }

        private static IServiceCollection AddConfigurationGraphQlTypes(this IServiceCollection services)
        {
            services.AddScoped<GetConfigurationCategoryType>();
            services.AddScoped<GetConfigurationCategoryWithNewsPortalsType>();
            services.AddScoped<GetConfigurationNewsPortalType>();
            services.AddScoped<GetConfigurationQueryResponseType>();
            services.AddScoped<GetConfigurationRegionType>();

            return services;
        }
    }
}
