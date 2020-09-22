using Espresso.WebApi.Application.CQRS.Articles.Queries.GetTrendingArticles;
using GraphQL.Types;

namespace Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.GetTrendingArticlesTypes
{
    /// <summary>
    /// 
    /// </summary>
    public class GetTrendingArticlesArticleType : ObjectGraphType<GetTrendingArticlesArticle>
    {
        /// <summary>
        /// 
        /// </summary>
        public GetTrendingArticlesArticleType()
        {
            Name = nameof(GetTrendingArticlesArticle);
            Field<NonNullGraphType<IdGraphType>>(
                name: nameof(GetTrendingArticlesArticle.Id)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetTrendingArticlesArticle.Title)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetTrendingArticlesArticle.Url)
            );
            Field<StringGraphType>(
                name: nameof(GetTrendingArticlesArticle.ImageUrl)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetTrendingArticlesArticle.PublishDateTime)
            );
            Field<NonNullGraphType<IntGraphType>>(
                name: nameof(GetTrendingArticlesArticle.TrendingScore)
            );
            Field<NonNullGraphType<GetTrendingArticlesNewsPortalType>>(
                name: nameof(GetTrendingArticlesArticle.NewsPortal)
            );
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<GetTrendingArticlesCategoryType>>>>(
                name: nameof(GetTrendingArticlesArticle.Categories)
            );
        }
    }
}
