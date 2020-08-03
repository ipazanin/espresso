using Espresso.Application.CQRS.Articles.Queries.GetCategoryArticles;
using GraphQL.Types;

namespace Espresso.Application.GraphQl.ApplicationTypes.ArticleTypes.GetCategoryArticlesTypes
{
    public class GetCategoryArticlesArticleType : ObjectGraphType<GetCategoryArticlesArticle>
    {
        public GetCategoryArticlesArticleType()
        {
            Name = nameof(GetCategoryArticlesArticle);
            Field<NonNullGraphType<IdGraphType>>(
                name: nameof(GetCategoryArticlesArticle.Id)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetCategoryArticlesArticle.Title)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetCategoryArticlesArticle.Url)
            );
            Field<StringGraphType>(
                name: nameof(GetCategoryArticlesArticle.ImageUrl)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetCategoryArticlesArticle.PublishDateTime)
            );
            Field<NonNullGraphType<GetCategoryArticlesNewsPortalType>>(
                name: nameof(GetCategoryArticlesArticle.NewsPortal)
            );
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<GetCategoryArticlesCategoryType>>>>(
                name: nameof(GetCategoryArticlesArticle.Categories)
            );
        }
    }
}
