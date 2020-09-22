using Espresso.WebApi.Application.CQRS.Articles.Queries.GetFeaturedArticles;
using GraphQL.Types;

namespace Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.GetFeaturedArticlesTypes
{
    /// <summary>
    /// 
    /// </summary>
    public class GetFeaturedArticlesCategoryType : ObjectGraphType<GetFeaturedArticlesCategory>
    {
        /// <summary>
        /// 
        /// </summary>
        public GetFeaturedArticlesCategoryType()
        {
            Name = nameof(GetFeaturedArticlesCategory);
            Field<NonNullGraphType<IdGraphType>>(
                name: nameof(GetFeaturedArticlesCategory.Id)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetFeaturedArticlesCategory.Name)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetFeaturedArticlesCategory.Color)
            );
        }
    }
}
