using Espresso.Application.CQRS.Articles.Queries.GetLatestArticles;
using Espresso.Application.GraphQl.ApplicationTypes.ArticleTypes.GetLatestArticlesTypes;
using Espresso.Application.GraphQl.Infrastructure;
using Espresso.Common.Constants;
using FluentValidation;
using GraphQL.Types;
using MediatR;

namespace Espresso.Application.GraphQl.ApplicationQueries.ArticlesQueries
{
    public class GetLatestArticlesGraphQlQuery : ObjectGraphType, IGraphQlQuery
    {
        public GetLatestArticlesGraphQlQuery(IMediator mediator)
        {
            Name = nameof(GetLatestArticlesGraphQlQuery);
            FieldAsync<GetLatestArticlesQueryResponseType>(
                name: nameof(GetLatestArticlesGraphQlQuery),
                arguments: new QueryArguments(
                    args: new QueryArgument[]
                    {
                        new QueryArgument<NonNullGraphType<IntGraphType>>
                        {
                            Name = "take",
                            DefaultValue = DefaultValueConstants.DefaultTake
                        },
                        new QueryArgument<NonNullGraphType<IntGraphType>>
                        {
                            Name = "skip",
                            DefaultValue = DefaultValueConstants.DefaultSkip
                        },
                        new QueryArgument<StringGraphType>
                        {
                            Name = "newsPortalIds",
                        },
                        new QueryArgument<StringGraphType>
                        {
                            Name = "categoryIds",
                        },
                    }
                ),
                resolve: async resolveContext =>
                {
                    if (
                        resolveContext.UserContext is GraphQlApplicationContext graphQlUserContext &&
                        graphQlUserContext != null
                    )
                    {
                        return await mediator.Send(
                            request: new GetLatestArticlesQuery(
                                take: resolveContext.GetArgument<int>("take"),
                                skip: resolveContext.GetArgument<int>("take"),
                                newsPortalIdsString: resolveContext.GetArgument<string?>("newsPortalIds"),
                                categoryIdsString: resolveContext.GetArgument<string?>("categoryIds"),
                                currentEspressoWebApiVersion: graphQlUserContext.CurrentEspressoWebApiVersion,
                                targetedEspressoWebApiVersion: graphQlUserContext.CurrentEspressoWebApiVersion,
                                consumerVersion: graphQlUserContext.ConsumerVersion,
                                deviceType: graphQlUserContext.DeviceType
                            ),
                            cancellationToken: resolveContext.CancellationToken
                        );
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
