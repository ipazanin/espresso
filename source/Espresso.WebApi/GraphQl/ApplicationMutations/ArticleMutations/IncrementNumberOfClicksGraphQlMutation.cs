using System;
using Espresso.WebApi.Application.CQRS.Articles.Commands.IncrementTrendingArticleScore;
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
                           request: new IncrementNumberOfClicksCommand(
                            id: Guid.Parse(articleIdString),
                            currentEspressoWebApiVersion: webApiConfiguration.AppVersionConfiguration.Version,
                            targetedEspressoWebApiVersion: userContext.TargetedApiVersion,
                            consumerVersion: userContext.ConsumerVersion,
                            deviceType: userContext.DeviceType,
                            appEnvironment: webApiConfiguration.AppConfiguration.AppEnvironment
                           ),
                           cancellationToken: resolveContext.CancellationToken
                        );
                    return articleIdString;
                },
                deprecationReason: null
            );
        }
    }
}
