using Espresso.Application.CQRS.Articles.Queries.GetCategoryArticles;
using Espresso.Application.GraphQl.ApplicationTypes.ArticleTypes;
using Espresso.Application.GraphQl.UserContext;
using Espresso.Common.Constants;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using FluentValidation;
using GraphQL.Types;
using MediatR;

namespace Espresso.Application.GraphQl.ApplicationQueries.ArticlesQueries
{
    public class GetCategoryArticlesGraphQlQuery : ObjectGraphType, IGraphQlQuery
    {
        public GetCategoryArticlesGraphQlQuery(IMediator mediator)
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
                            request: new GetCategoryArticlesQuery(
                                take: resolveContext.GetArgument<int>("take"),
                                skip: resolveContext.GetArgument<int>("take"),
                                newsPortalIdsString: resolveContext.GetArgument<string?>("newsPortalIds"),
                                categoryId: resolveContext.GetArgument<int>("categoryId"),
                                regionId: resolveContext.GetArgument<int?>("regionId"),
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
