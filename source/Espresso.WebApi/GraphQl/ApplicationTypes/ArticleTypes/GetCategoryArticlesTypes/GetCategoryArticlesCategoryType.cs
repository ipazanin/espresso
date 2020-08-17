using Espresso.Application.CQRS.Articles.Queries.GetCategoryArticles;
using GraphQL.Types;

namespace Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.GetCategoryArticlesTypes
{
    /// <summary>
    /// 
    /// </summary>
    public class GetCategoryArticlesCategoryType : ObjectGraphType<GetCategoryArticlesCategory>
    {
        /// <summary>
        /// 
        /// </summary>
        public GetCategoryArticlesCategoryType()
        {
            Name = nameof(GetCategoryArticlesCategory);
            Field<NonNullGraphType<IdGraphType>>(
                name: nameof(GetCategoryArticlesCategory.Id)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetCategoryArticlesCategory.Name)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetCategoryArticlesCategory.Color)
            );
        }
    }
}
