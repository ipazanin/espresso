using Espresso.WebApi.Application.CQRS.Articles.Queries.GetLatestArticles;
using GraphQL.Types;

namespace Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.GetLatestArticlesTypes
{
    /// <summary>
    /// 
    /// </summary>
    public class GetLatestArticlesCategoryType : ObjectGraphType<GetLatestArticlesCategory>
    {
        /// <summary>
        /// 
        /// </summary>
        public GetLatestArticlesCategoryType()
        {
            Name = nameof(GetLatestArticlesCategory);
            Field<NonNullGraphType<IdGraphType>>(
                name: nameof(GetLatestArticlesCategory.Id)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetLatestArticlesCategory.Name)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetLatestArticlesCategory.Color)
            );
        }
    }
}
