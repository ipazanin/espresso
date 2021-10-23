// GetFeaturedArticlesGraphQlQuery.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using Espresso.WebApi.Application.Articles.Queries.GetFeaturedArticles;
using Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.GetFeaturedArticlesTypes;
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
    public class GetFeaturedArticlesGraphQlQuery : ObjectGraphType, IGraphQlQuery
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetFeaturedArticlesGraphQlQuery"/> class.
        /// </summary>
        /// <param name="mediator"></param>
        public GetFeaturedArticlesGraphQlQuery(IMediator mediator)
        {
            FieldAsync<GetFeaturedArticlesQueryResponseType>(
                name: "featuredArticles",
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
                        request: new GetFeaturedArticlesQuery
                        {
                            Take = resolveContext.GetArgument<int>("take"),
                            Skip = resolveContext.GetArgument<int>("skip"),
                            FirstArticleId = firstArticleId,
                            NewsPortalIds = resolveContext.GetArgument<string?>("newsPortalIds"),
                            CategoryIds = resolveContext.GetArgument<string?>("categoryIds"),
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
