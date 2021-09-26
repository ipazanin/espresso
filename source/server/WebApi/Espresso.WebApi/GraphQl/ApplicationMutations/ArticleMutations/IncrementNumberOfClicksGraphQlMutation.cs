// IncrementNumberOfClicksGraphQlMutation.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.WebApi.Application.Articles.Commands.IncrementTrendingArticleScore;
using Espresso.WebApi.GraphQl.Infrastructure;
using GraphQL;
using GraphQL.Types;
using MediatR;
using System;

namespace Espresso.WebApi.GraphQl.ApplicationMutations.ArticlesQueries
{
    /// <summary>
    /// 
    /// </summary>
    public class IncrementNumberOfClicksGraphQlMutation : ObjectGraphType, IGraphQlMutation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IncrementNumberOfClicksGraphQlMutation"/> class.
        /// </summary>
        /// <param name="mediator"></param>
        public IncrementNumberOfClicksGraphQlMutation(
            IMediator mediator
        )
        {
            FieldAsync<StringGraphType>(
                name: "incrementNumberOfClicks",
                arguments: new QueryArguments(
                    args: new QueryArgument[]
                    {
                        new QueryArgument<NonNullGraphType<StringGraphType>>
                        {
                            Name = "articleId",
                        },
                    }
                ),
                resolve: async resolveContext =>
                {
                    var userContext = resolveContext.UserContext as GraphQlUserContext ??
                        throw new Exception("Invalid GraphQL User Context");

                    var articleIdString = resolveContext.GetArgument<string>("articleId");
                    await mediator.Send(
                           request: new IncrementNumberOfClicksCommand
                           {
                               Id = Guid.Parse(articleIdString),
                               TargetedApiVersion = userContext.TargetedApiVersion,
                               ConsumerVersion = userContext.ConsumerVersion,
                               DeviceType = userContext.DeviceType,
                           },
                           cancellationToken: resolveContext.CancellationToken
                        );
                    return articleIdString;
                },
                deprecationReason: null
            );
        }
    }
}
