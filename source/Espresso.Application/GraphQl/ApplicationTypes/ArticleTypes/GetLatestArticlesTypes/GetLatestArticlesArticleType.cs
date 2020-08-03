using Espresso.Application.CQRS.Articles.Queries.GetLatestArticles;
using GraphQL.Types;

namespace Espresso.Application.GraphQl.ApplicationTypes.ArticleTypes.GetLatestArticlesTypes
{
    public class GetLatestArticlesArticleType : ObjectGraphType<GetLatestArticlesArticle>
    {
        public GetLatestArticlesArticleType()
        {
            Name = nameof(GetLatestArticlesArticle);
            Field<NonNullGraphType<IdGraphType>>(
                name: nameof(GetLatestArticlesArticle.Id)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetLatestArticlesArticle.Title)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetLatestArticlesArticle.Url)
            );
            Field<StringGraphType>(
                name: nameof(GetLatestArticlesArticle.ImageUrl)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetLatestArticlesArticle.PublishDateTime)
            );
            Field<NonNullGraphType<GetLatestArticlesNewsPortalType>>(
                name: nameof(GetLatestArticlesArticle.NewsPortal)
            );
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<GetLatestArticlesCategoryType>>>>(
                name: nameof(GetLatestArticlesArticle.Categories)
            );
        }
    }
}
