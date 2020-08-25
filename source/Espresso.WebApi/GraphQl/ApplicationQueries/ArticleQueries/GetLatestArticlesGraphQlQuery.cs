using Espresso.Application.CQRS.Articles.Queries.GetLatestArticles;
using Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.GetLatestArticlesTypes;
using Espresso.Common.Constants;
using GraphQL.Types;
using MediatR;
using Espresso.WebApi.Configuration;
using Espresso.Domain.Enums.ApplicationDownloadEnums;

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
                        new QueryArgument<StringGraphType>
                        {
                            Name = "titleSearchQuery",
                        },
                    }
                ),
                resolve: async resolveContext =>
                {
                    return await mediator.Send(
                        request: new GetLatestArticlesQuery(
                            take: (int?)resolveContext.Arguments["take"],
                            skip: (int?)resolveContext.Arguments["skip"],
                            newsPortalIdsString: (string?)resolveContext.Arguments["newsPortalIds"],
                            categoryIdsString: (string?)resolveContext.Arguments["categoryIds"],
                            newNewsPortalsPosition: configuration.NewNewsPortalsPosition,
                            titleSearchQuery: (string?)resolveContext.Arguments["titleSearchQuery"],
                            currentEspressoWebApiVersion: (string)resolveContext.UserContext["currentEspressoWebApiVersion"],
                            targetedEspressoWebApiVersion: (string)resolveContext.UserContext["targetedEspressoWebApiVersion"],
                            consumerVersion: (string)resolveContext.UserContext["consumerVersion"],
                            deviceType: (DeviceType)resolveContext.UserContext["deviceType"]
                        ),
                        cancellationToken: resolveContext.CancellationToken
                    );
                },
                deprecationReason: null
            );
        }
    }
}
