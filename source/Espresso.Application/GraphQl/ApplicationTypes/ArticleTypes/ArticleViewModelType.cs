using Espresso.Application.ViewModels.ArticleViewModels;
using GraphQL.Types;

namespace Espresso.Application.GraphQl.ApplicationTypes.ArticleTypes
{
    public class ArticleViewModelType : ObjectGraphType<ArticleViewModel>
    {
        public ArticleViewModelType()
        {
            Name = nameof(ArticleViewModel);
            Field<NonNullGraphType<IdGraphType>>(
                name: nameof(ArticleViewModel.Id)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(ArticleViewModel.Title)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(ArticleViewModel.Url)
            );
            Field<StringGraphType>(
                name: nameof(ArticleViewModel.ImageUrl)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(ArticleViewModel.PublishDateTime)
            );
            Field<NonNullGraphType<NewsPortalViewModelType>>(
                name: nameof(ArticleViewModel.NewsPortal)
            );
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<CategoryViewModelType>>>>(
                name: nameof(ArticleViewModel.Categories)
            );
        }
    }
}
