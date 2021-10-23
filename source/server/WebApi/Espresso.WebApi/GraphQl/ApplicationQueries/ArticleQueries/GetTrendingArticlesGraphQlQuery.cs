﻿// GetTrendingArticlesGraphQlQuery.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using Espresso.WebApi.Application.Articles.Queries.GetTrendingArticles;
using Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.GetTrendingArticlesTypes;
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
    public class GetTrendingArticlesGraphQlQuery : ObjectGraphType, IGraphQlQuery
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetTrendingArticlesGraphQlQuery"/> class.
        /// </summary>
        /// <param name="mediator"></param>
        public GetTrendingArticlesGraphQlQuery(IMediator mediator)
        {
            FieldAsync<GetTrendingArticlesQueryResponseType>(
                name: "trendingArticles",
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
                        request: new GetTrendingArticlesQuery
                        {
                            Take = resolveContext.GetArgument<int>("take"),
                            Skip = resolveContext.GetArgument<int>("skip"),
                            FirstArticleId = firstArticleId,
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
