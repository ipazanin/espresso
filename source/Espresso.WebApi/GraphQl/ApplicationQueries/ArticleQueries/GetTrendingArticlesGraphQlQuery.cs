using Espresso.Application.CQRS.Articles.Queries.GetTrendingArticles;
using Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.GetTrendingArticlesTypes;
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
    public class GetTrendingArticlesGraphQlQuery : ObjectGraphType, IGraphQlQuery
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="webApiConfiguration"></param>
        public GetTrendingArticlesGraphQlQuery(IMediator mediator, IWebApiConfiguration webApiConfiguration)
        {
            Name = nameof(GetTrendingArticlesGraphQlQuery);
            FieldAsync<GetTrendingArticlesQueryResponseType>(
                name: nameof(GetTrendingArticlesGraphQlQuery),
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
                        }
                    }
                ),
                resolve: async resolveContext =>
                {
                    return resolveContext.UserContext is GraphQlApplicationContext graphQlUserContext &&
                        graphQlUserContext != null ?
                        await mediator.Send(
                            request: new GetTrendingArticlesQuery(
                                take: resolveContext.GetArgument<int>("take"),
                                skip: resolveContext.GetArgument<int>("take"),
                                maxAgeOfTrendingArticle: webApiConfiguration.MaxAgeOfTrendingArticle,
                                currentEspressoWebApiVersion: graphQlUserContext.CurrentEspressoWebApiVersion,
                                targetedEspressoWebApiVersion: graphQlUserContext.CurrentEspressoWebApiVersion,
                                consumerVersion: graphQlUserContext.ConsumerVersion,
                                deviceType: graphQlUserContext.DeviceType
                            ),
                            cancellationToken: resolveContext.CancellationToken
                        ) :
                        throw new ValidationException("Appropriate Headers must be defined!");
                },
                deprecationReason: null
            );
        }
    }
}
