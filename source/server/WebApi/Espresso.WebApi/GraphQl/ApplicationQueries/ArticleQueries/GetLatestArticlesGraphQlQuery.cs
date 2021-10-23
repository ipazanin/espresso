// GetLatestArticlesGraphQlQuery.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using Espresso.Domain.Infrastructure;
using Espresso.WebApi.Application.Articles.Queries.GetLatestArticles;
using Espresso.WebApi.Configuration;
using Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.GetLatestArticlesTypes;
using Espresso.WebApi.GraphQl.Infrastructure;
using GraphQL;
using GraphQL.Types;
using MediatR;
using System;

namespace Espresso.WebApi.GraphQl.ApplicationQueries.ArticlesQueries
{
    /// <summary>
    /// 
    /// </summary>
    public class GetLatestArticlesGraphQlQuery : ObjectGraphType, IGraphQlQuery
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetLatestArticlesGraphQlQuery"/> class.
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="webApiConfiguration"></param>
        /// <param name="settingProvider"></param>
        public GetLatestArticlesGraphQlQuery(
            IMediator mediator,
            IWebApiConfiguration webApiConfiguration,
            ISettingProvider settingProvider)
        {
            FieldAsync<GetLatestArticlesQueryResponseType>(
                name: "latestArticles",
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
                        new QueryArgument<StringGraphType>
                        {
                            Name = "categoryIds",
                        },
                        new QueryArgument<StringGraphType>
                        {
                            Name = "titleSearchQuery",
                        },
                    }),
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
                        request: new GetLatestArticlesQuery
                        {
                            Take = resolveContext.GetArgument<int>("take"),
                            Skip = resolveContext.GetArgument<int>("skip"),
                            FirstArticleId = firstArticleId,
                            NewsPortalIds = resolveContext.GetArgument<string?>("newsPortalIds"),
                            CategoryIds = resolveContext.GetArgument<string?>("categoryIds"),
                            TitleSearchQuery = resolveContext.GetArgument<string?>("titleSearchQuery"),
                            TargetedApiVersion = userContext.TargetedApiVersion,
                            ConsumerVersion = userContext.ConsumerVersion,
                            DeviceType = userContext.DeviceType,
                        },
                        cancellationToken: resolveContext.CancellationToken);
                },
                deprecationReason: null);
        }
    }
}
