using System.Collections;
using System.Collections.Generic;
using Espresso.Application.CQRS.Articles.Queries.GetCategoryArticles;
using Espresso.Application.GraphQl.ApplicationTypes.ArticleTypes;
using Espresso.Application.GraphQl.UserContext;
using Espresso.Common.Constants;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using FluentValidation;
using GraphQL.Types;
using MediatR;

namespace Espresso.Application.GraphQl.ApplicationQueries
{
    public class RootGraphQlQuery : ObjectGraphType
    {
        public RootGraphQlQuery(IEnumerable<IGraphQlQuery> graphQlQueries)
        {
            Name = nameof(RootGraphQlQuery);
            foreach (var marker in graphQlQueries)
            {
                if (marker is ObjectGraphType<object> q)
                {
                    foreach (var f in q.Fields)
                    {
                        AddField(f);
                    }
                }
            }
        }
    }
}
