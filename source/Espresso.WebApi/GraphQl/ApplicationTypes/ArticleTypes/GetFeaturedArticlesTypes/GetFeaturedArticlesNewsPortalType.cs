using Espresso.WebApi.Application.CQRS.Articles.Queries.GetFeaturedArticles;
using GraphQL.Types;

namespace Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.GetFeaturedArticlesTypes
{
    /// <summary>
    /// 
    /// </summary>
    public class GetFeaturedArticlesNewsPortalType : ObjectGraphType<GetFeaturedArticlesNewsPortal>
    {
        /// <summary>
        /// 
        /// </summary>
        public GetFeaturedArticlesNewsPortalType()
        {
            Name = nameof(GetFeaturedArticlesNewsPortal);
            Field<NonNullGraphType<IdGraphType>>(
                name: nameof(GetFeaturedArticlesNewsPortal.Id)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetFeaturedArticlesNewsPortal.Name)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetFeaturedArticlesNewsPortal.IconUrl)
            );
        }
    }
}
