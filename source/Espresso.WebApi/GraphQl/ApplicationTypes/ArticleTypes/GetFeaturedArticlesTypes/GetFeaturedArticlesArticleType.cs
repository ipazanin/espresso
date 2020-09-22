using Espresso.WebApi.Application.CQRS.Articles.Queries.GetFeaturedArticles;
using GraphQL.Types;

namespace Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.GetFeaturedArticlesTypes
{
    /// <summary>
    /// 
    /// </summary>
    public class GetFeaturedArticlesArticleType : ObjectGraphType<GetFeaturedArticlesArticle>
    {
        /// <summary>
        /// 
        /// </summary>
        public GetFeaturedArticlesArticleType()
        {
            Name = nameof(GetFeaturedArticlesArticle);
            Field<NonNullGraphType<IdGraphType>>(
                name: nameof(GetFeaturedArticlesArticle.Id)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetFeaturedArticlesArticle.Title)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetFeaturedArticlesArticle.Url)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetFeaturedArticlesArticle.WebUrl)
            );
            Field<StringGraphType>(
                name: nameof(GetFeaturedArticlesArticle.ImageUrl)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetFeaturedArticlesArticle.PublishDateTime)
            );
            Field<NonNullGraphType<GetFeaturedArticlesNewsPortalType>>(
                name: nameof(GetFeaturedArticlesArticle.NewsPortal)
            );
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<GetFeaturedArticlesCategoryType>>>>(
                name: nameof(GetFeaturedArticlesArticle.Categories)
            );
        }
    }
}
