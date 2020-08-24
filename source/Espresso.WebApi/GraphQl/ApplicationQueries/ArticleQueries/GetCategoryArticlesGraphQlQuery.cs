using Espresso.Application.CQRS.Articles.Queries.GetCategoryArticles;
using Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.GetCategoryArticlesTypes;
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
    public class GetCategoryArticlesGraphQlQuery : ObjectGraphType, IGraphQlQuery
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="configuration"></param>
        public GetCategoryArticlesGraphQlQuery(IMediator mediator, IWebApiConfiguration configuration)
        {
            Name = nameof(GetCategoryArticlesGraphQlQuery);
            FieldAsync<GetCategoryArticlesQueryResponseType>(
                name: nameof(GetCategoryArticlesGraphQlQuery),
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
                        new QueryArgument<NonNullGraphType<IntGraphType>>
                        {
                            Name = "categoryId",
                        },
                        new QueryArgument<IntGraphType>
                        {
                            Name = "regionId",
                        },
                        new QueryArgument<IntGraphType>
                        {
                            Name = "titleSearchQuery",
                        },
                    }
                ),
                resolve: async resolveContext =>
                {
                    return resolveContext.UserContext is GraphQlApplicationContext graphQlUserContext &&
                        graphQlUserContext != null ?
                        await mediator.Send(
                            request: new GetCategoryArticlesQuery(
                                take: resolveContext.GetArgument<int>("take"),
                                skip: resolveContext.GetArgument<int>("take"),
                                newsPortalIdsString: resolveContext.GetArgument<string?>("newsPortalIds"),
                                categoryId: resolveContext.GetArgument<int>("categoryId"),
                                regionId: resolveContext.GetArgument<int?>("regionId"),
                                newNewsPortalsPosition: configuration.NewNewsPortalsPosition,
                                titleSearchQuery: resolveContext.GetArgument<string?>("titleSearchQuery"),
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
