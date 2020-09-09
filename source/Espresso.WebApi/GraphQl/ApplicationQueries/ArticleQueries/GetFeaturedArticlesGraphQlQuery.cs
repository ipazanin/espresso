using System;
using Espresso.Application.CQRS.Articles.Queries.GetFeaturedArticles;
using Espresso.Common.Constants;
using Espresso.WebApi.Configuration;
using Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.GetFeaturedArticlesTypes;
using Espresso.WebApi.GraphQl.Infrastructure;
using GraphQL;
using GraphQL.Types;
using MediatR;

namespace Espresso.WebApi.GraphQl.ApplicationQueries.ArticlesQueries
{
    /// <summary>
    /// 
    /// </summary>
    public class GetFeaturedArticlesGraphQlQuery : ObjectGraphType, IGraphQlQuery
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="webApiConfiguration"></param>
        public GetFeaturedArticlesGraphQlQuery(
            IMediator mediator,
            IWebApiConfiguration webApiConfiguration
        )
        {
            FieldAsync<GetFeaturedArticlesQueryResponseType>(
                name: "featuredArticles",
                arguments: new QueryArguments(
                    args: new QueryArgument[]
                    {
                        new QueryArgument<NonNullGraphType<IntGraphType>>
                        {
                            Name = "take",
                            DefaultValue = DefaultValueConstants.DefaultTake
                        },
                        new QueryArgument<NonNullGraphType<IntGraphType>>
                        {
                            Name = "skip",
                            DefaultValue = DefaultValueConstants.DefaultSkip
                        },
                        new QueryArgument<StringGraphType>
                        {
                            Name = "firstArticleId",
                        },
                        new QueryArgument<StringGraphType>
                        {
                            Name = "newsPortalIds",
                        },
                        new QueryArgument<StringGraphType>
                        {
                            Name = "categoryIds",
                        },
                    }
                ),
                resolve: async resolveContext =>
                {
                    var userContext = resolveContext.UserContext as GraphQlUserContext ??
                        throw new Exception("Invalid GraphQL User Context");

                    var firstArticleIdString = resolveContext.GetArgument<string?>("firstArticleId");
                    var firstArticleId = firstArticleIdString is null ?
                        (Guid?)null :
                        Guid.Parse(firstArticleIdString);

                    return await mediator.Send(
                        request: new GetFeaturedArticlesQuery(
                            take: resolveContext.GetArgument<int>("take"),
                            skip: resolveContext.GetArgument<int>("skip"),
                            firstArticleId: firstArticleId,
                            newsPortalIdsString: resolveContext.GetArgument<string?>("newsPortalIds"),
                            categoryIdsString: resolveContext.GetArgument<string?>("categoryIds"),
                            maxAgeOfFeaturedArticle: webApiConfiguration.DateTimeConfiguration.MaxAgeOfFeaturedArticle,
                            maxAgeOfTrendingArticle: webApiConfiguration.DateTimeConfiguration.MaxAgeOfTrendingArticle,
                            currentEspressoWebApiVersion: webApiConfiguration.AppVersionConfiguration.Version,
                            targetedEspressoWebApiVersion: userContext.TargetedApiVersion,
                            consumerVersion: userContext.ConsumerVersion,
                            deviceType: userContext.DeviceType,
                            appEnvironment: webApiConfiguration.AppConfiguration.AppEnvironment
                        ),
                        cancellationToken: resolveContext.CancellationToken
                    );
                },
                deprecationReason: null
            );
        }
    }
}
