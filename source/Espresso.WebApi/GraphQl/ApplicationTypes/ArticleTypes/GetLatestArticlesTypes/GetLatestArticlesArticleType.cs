using Espresso.Application.CQRS.Articles.Queries.GetLatestArticles;
using GraphQL.Types;

namespace Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.GetLatestArticlesTypes
{
    /// <summary>
    /// 
    /// </summary>
    public class GetLatestArticlesArticleType : ObjectGraphType<GetLatestArticlesArticle>
    {
        /// <summary>
        /// 
        /// </summary>
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
