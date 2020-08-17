using Espresso.Application.CQRS.Articles.Queries.GetLatestArticles;
using Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.GetLatestArticlesTypes;
using Espresso.WebApi.GraphQl.Infrastructure;
using Espresso.Common.Constants;
using FluentValidation;
using GraphQL.Types;
using MediatR;
using Espresso.WebApi.Configuration;

namespace Espresso.WebApi.GraphQl.ApplicationQueries.ArticlesQueries
{
    /// <summary>
    /// 
    /// </summary>
    public class GetLatestArticlesGraphQlQuery : ObjectGraphType, IGraphQlQuery
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="configuration"></param>
        public GetLatestArticlesGraphQlQuery(IMediator mediator, IWebApiConfiguration configuration)
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
                                newNewsPortalsPosition: configuration.NewNewsPortalsPosition,
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
