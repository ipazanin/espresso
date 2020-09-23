using System;
using Espresso.WebApi.Application.Articles.Queries.GetTrendingArticles;
using Espresso.Common.Constants;
using Espresso.WebApi.Configuration;
using Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.GetTrendingArticlesTypes;
using Espresso.WebApi.GraphQl.Infrastructure;
using GraphQL;
using GraphQL.Types;
using MediatR;

namespace Espresso.WebApi.GraphQl.ApplicationQueries.ArticlesQueries
{
    /// <summary>
    /// 
    /// </summary>
    public class GetTrendingArticlesGraphQlQuery : ObjectGraphType, IGraphQlQuery
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="webApiConfiguration"></param>
        public GetTrendingArticlesGraphQlQuery(IMediator mediator, IWebApiConfiguration webApiConfiguration)
        {
            FieldAsync<GetTrendingArticlesQueryResponseType>(
                name: "trendingArticles",
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
                    }
                ),
                resolve: async resolveContext =>
                {
                    var userContext = resolveContext.UserContext as GraphQlUserContext ??
                        throw new Exception("Invalid GraphQL User Context");

                    var firstArticleIdString = resolveContext.GetArgument<string?>("firstArticleId");
                    Guid? firstArticleId;
                    if (Guid.TryParse(firstArticleIdString, out var temporaryFirstArticleId))
                    {
                        firstArticleId = temporaryFirstArticleId;
                    }
                    else
                    {
                        firstArticleId = null;
                    }

                    return await mediator.Send(
                        request: new GetTrendingArticlesQuery(
                            take: resolveContext.GetArgument<int>("take"),
                            skip: resolveContext.GetArgument<int>("skip"),
                            firstArticleId: firstArticleId,
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
