using System;
using Espresso.WebApi.Application.Articles.Commands.IncrementTrendingArticleScore;
using Espresso.WebApi.Configuration;
using Espresso.WebApi.GraphQl.Infrastructure;
using GraphQL;
using GraphQL.Types;
using MediatR;

namespace Espresso.WebApi.GraphQl.ApplicationMutations.ArticlesQueries
{
    /// <summary>
    /// 
    /// </summary>
    public class IncrementNumberOfClicksGraphQlMutation : ObjectGraphType, IGraphQlMutation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="webApiConfiguration"></param>
        public IncrementNumberOfClicksGraphQlMutation(
            IMediator mediator,
            IWebApiConfiguration webApiConfiguration
        )
        {
            FieldAsync<StringGraphType>(
                name: "incrementNumberOfClicks",
                arguments: new QueryArguments(
                    args: new QueryArgument[]
                    {
                        new QueryArgument<NonNullGraphType<StringGraphType>>
                        {
                            Name = "articleId"
                        }
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
                               CurrentApiVersion = webApiConfiguration.AppConfiguration.Version,
                               TargetedApiVersion = userContext.TargetedApiVersion,
                               ConsumerVersion = userContext.ConsumerVersion,
                               DeviceType = userContext.DeviceType,
                               AppEnvironment = webApiConfiguration.AppConfiguration.AppEnvironment
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
