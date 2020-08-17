﻿using Espresso.WebApi.GraphQl.ApplicationQueries;
using Espresso.WebApi.GraphQl.ApplicationQueries.ArticlesQueries;
using Espresso.WebApi.GraphQl.ApplicationQueries.ConfigurationQueries;
using Espresso.WebApi.GraphQl.ApplicationSchema;
using Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.GetCategoryArticlesTypes;
using Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.GetLatestArticlesTypes;
using Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.GetTrendingArticlesTypes;
using Espresso.WebApi.GraphQl.ApplicationTypes.ConfigurationTypes;
using GraphQL;
using GraphQL.Http;
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