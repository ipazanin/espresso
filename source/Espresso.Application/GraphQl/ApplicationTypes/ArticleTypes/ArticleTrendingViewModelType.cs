using Espresso.Application.ViewModels.ArticleViewModels;
using GraphQL.Types;

namespace Espresso.Application.GraphQl.ApplicationTypes.ArticleTypes
{
    public class ArticleTrendingViewModelType : ObjectGraphType<ArticleTrendingViewModel>
    {
        public ArticleTrendingViewModelType()
        {
            Name = nameof(ArticleTrendingViewModel);
            Field<NonNullGraphType<IdGraphType>>(
                name: nameof(ArticleTrendingViewModel.Id)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(ArticleTrendingViewModel.Title)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(ArticleTrendingViewModel.Url)
            );
            Field<StringGraphType>(
                name: nameof(ArticleTrendingViewModel.ImageUrl)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(ArticleTrendingViewModel.PublishDateTime)
            );
            Field<NonNullGraphType<IntGraphType>>(
                name: nameof(ArticleTrendingViewModel.TrendingScore)
            );
            Field<NonNullGraphType<NewsPortalViewModelType>>(
                name: nameof(ArticleTrendingViewModel.NewsPortal)
            );
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<CategoryViewModelType>>>>(
                name: nameof(ArticleTrendingViewModel.Categories)
            );
        }
    }
}
