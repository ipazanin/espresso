using System;
using Espresso.Application.CQRS.Articles.Commands.IncrementTrendingArticleScore;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using GraphQL.Types;
using MediatR;

namespace Espresso.WebApi.GraphQl.ApplicationQueries.ArticlesQueries
{
    /// <summary>
    /// 
    /// </summary>
    public class IncrementNumberOfClicksGraphQlMutation : ObjectGraphType, IGraphQlQuery
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public IncrementNumberOfClicksGraphQlMutation(IMediator mediator)
        {
            Name = nameof(IncrementNumberOfClicksGraphQlMutation);
            FieldAsync<IdGraphType>(
                name: nameof(IncrementNumberOfClicksGraphQlMutation),
                arguments: new QueryArguments(
                    args: new QueryArgument[]
                    {
                        new QueryArgument<NonNullGraphType<IdGraphType>>
                        {
                            Name = "articleId"
                        }
                    }
                ),
                resolve: async resolveContext =>
                {
                    var articleId = (Guid)resolveContext.Arguments["articleId"];
                    await mediator.Send(
                           request: new IncrementNumberOfClicksCommand(
                               id: articleId,
                            currentEspressoWebApiVersion: (string)resolveContext.UserContext["currentEspressoWebApiVersion"],
                            targetedEspressoWebApiVersion: (string)resolveContext.UserContext["targetedEspressoWebApiVersion"],
                            consumerVersion: (string)resolveContext.UserContext["consumerVersion"],
                            deviceType: (DeviceType)resolveContext.UserContext["deviceType"]
                           ),
                           cancellationToken: resolveContext.CancellationToken
                        );
                    return articleId;
                },
                deprecationReason: null
            );
        }
    }
}
