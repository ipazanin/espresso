using Espresso.WebApi.Application.CQRS.Articles.Queries.GetLatestArticles;
using GraphQL.Types;

namespace Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.GetLatestArticlesTypes
{
    /// <summary>
    /// 
    /// </summary>
    public class GetLatestArticlesNewsPortalType : ObjectGraphType<GetLatestArticlesNewsPortal>
    {
        /// <summary>
        /// 
        /// </summary>
        public GetLatestArticlesNewsPortalType()
        {
            Name = nameof(GetLatestArticlesNewsPortal);
            Field<NonNullGraphType<IdGraphType>>(
                name: nameof(GetLatestArticlesNewsPortal.Id)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetLatestArticlesNewsPortal.Name)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetLatestArticlesNewsPortal.IconUrl)
            );
        }
    }
}
