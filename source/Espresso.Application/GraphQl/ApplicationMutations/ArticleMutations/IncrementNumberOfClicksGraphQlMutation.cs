using System;
using Espresso.Application.CQRS.Articles.Commands.IncrementTrendingArticleScore;
using Espresso.Application.GraphQl.ApplicationTypes.ArticleTypes;
using Espresso.Application.GraphQl.UserContext;
using FluentValidation;
using GraphQL.Types;
using MediatR;

namespace Espresso.Application.GraphQl.ApplicationQueries.ArticlesQueries
{
    public class IncrementNumberOfClicksGraphQlMutation : ObjectGraphType, IGraphQlQuery
    {
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
                    if (
                        resolveContext.UserContext is GraphQlApplicationContext graphQlUserContext &&
                        graphQlUserContext != null
                    )
                    {
                        var articleId = resolveContext.GetArgument<Guid>("articleId");
                        await mediator.Send(
                           request: new IncrementNumberOfClicksCommand(
                               id: articleId,
                               currentEspressoWebApiVersion: graphQlUserContext.CurrentEspressoWebApiVersion,
                               targetedEspressoWebApiVersion: graphQlUserContext.CurrentEspressoWebApiVersion,
                               consumerVersion: graphQlUserContext.ConsumerVersion,
                               deviceType: graphQlUserContext.DeviceType
                           ),
                           cancellationToken: resolveContext.CancellationToken
                        );
                        return articleId;
                    }
                    else
                    {
                        throw new ValidationException("Appropriate Headers must be defined!");
                    }
                },
                deprecationReason: null
            );
        }
    }
}
