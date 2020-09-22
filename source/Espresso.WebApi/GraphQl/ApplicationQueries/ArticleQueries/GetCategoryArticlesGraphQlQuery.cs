using System;
using Espresso.WebApi.Application.CQRS.Articles.Queries.GetCategoryArticles;
using Espresso.Common.Constants;
using Espresso.WebApi.Configuration;
using Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.ConfigurationTypes.GetWebCategoryArticlesTypes;
using Espresso.WebApi.GraphQl.Infrastructure;
using GraphQL;
using GraphQL.Types;
using MediatR;

namespace Espresso.WebApi.GraphQl.ApplicationQueries.ArticlesQueries
{
    /// <summary>
    /// 
    /// </summary>
    public class GetCategoryArticlesGraphQlQuery : ObjectGraphType, IGraphQlQuery
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="webApiConfiguration"></param>
        public GetCategoryArticlesGraphQlQuery(
            IMediator mediator,
            IWebApiConfiguration webApiConfiguration
        )
        {
            FieldAsync<GetCategoryArticlesQueryResponseType>(
                name: "categoryArticles",
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
                        new QueryArgument<NonNullGraphType<IntGraphType>>
                        {
                            Name = "categoryId",
                        },
                        new QueryArgument<IntGraphType>
                        {
                            Name = "regionId",
                        },
                        new QueryArgument<StringGraphType>
                        {
                            Name = "titleSearchQuery",
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
                        request: new GetCategoryArticlesQuery(
                            take: resolveContext.GetArgument<int>("take"),
                            skip: resolveContext.GetArgument<int>("skip"),
                            firstArticleId: firstArticleId,
                            newsPortalIdsString: resolveContext.GetArgument<string?>("newsPortalIds"),
                            categoryId: resolveContext.GetArgument<int>("categoryId"),
                            regionId: resolveContext.GetArgument<int?>("regionId"),
                            titleSearchQuery: resolveContext.GetArgument<string?>("titleSearchQuery"),
                            newNewsPortalsPosition: webApiConfiguration.AppConfiguration.NewNewsPortalsPosition,
                            maxAgeOfNewNewsPortal: webApiConfiguration.DateTimeConfiguration.MaxAgeOfNewNewsPortal,
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
