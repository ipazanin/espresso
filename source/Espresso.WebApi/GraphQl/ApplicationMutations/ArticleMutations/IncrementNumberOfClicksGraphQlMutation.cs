using System;
using Espresso.Application.CQRS.Articles.Commands.IncrementTrendingArticleScore;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
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
        public IncrementNumberOfClicksGraphQlMutation(IMediator mediator)
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
                    var articleIdString = (string)resolveContext.Arguments["articleId"];
                    await mediator.Send(
                           request: new IncrementNumberOfClicksCommand(
                            id: Guid.Parse(articleIdString),
                            currentEspressoWebApiVersion: (string)resolveContext.UserContext["currentEspressoWebApiVersion"],
                            targetedEspressoWebApiVersion: (string)resolveContext.UserContext["targetedEspressoWebApiVersion"],
                            consumerVersion: (string)resolveContext.UserContext["consumerVersion"],
                            deviceType: (DeviceType)resolveContext.UserContext["deviceType"],
                            appEnvironment: (AppEnvironment)resolveContext.UserContext["appEnvironment"]
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
