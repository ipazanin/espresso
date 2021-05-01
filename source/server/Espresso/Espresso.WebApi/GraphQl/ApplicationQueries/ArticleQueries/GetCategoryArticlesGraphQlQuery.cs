using System;
using Espresso.Common.Constants;
using Espresso.WebApi.Application.Articles.Queries.GetCategoryArticles;
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
                            DefaultValue = DefaultValueConstants.DefaultTake,
                        },
                        new QueryArgument<NonNullGraphType<IntGraphType>>
                        {
                            Name = "skip",
                            DefaultValue = DefaultValueConstants.DefaultSkip,
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
                        request: new GetCategoryArticlesQuery
                        {
                            Take = resolveContext.GetArgument<int>("take"),
                            Skip = resolveContext.GetArgument<int>("skip"),
                            FirstArticleId = firstArticleId,
                            NewsPortalIds = resolveContext.GetArgument<string?>("newsPortalIds"),
                            CategoryId = resolveContext.GetArgument<int>("categoryId"),
                            RegionId = resolveContext.GetArgument<int?>("regionId"),
                            TitleSearchQuery = resolveContext.GetArgument<string?>("titleSearchQuery"),
                            NewNewsPortalsPosition = webApiConfiguration.AppConfiguration.NewNewsPortalsPosition,
                            MaxAgeOfNewNewsPortal = webApiConfiguration.DateTimeConfiguration.MaxAgeOfNewNewsPortal,
                            TargetedApiVersion = userContext.TargetedApiVersion,
                            ConsumerVersion = userContext.ConsumerVersion,
                            DeviceType = userContext.DeviceType,
                        },
                        cancellationToken: resolveContext.CancellationToken
                    );
                },
                deprecationReason: null
            );
        }
    }
}
