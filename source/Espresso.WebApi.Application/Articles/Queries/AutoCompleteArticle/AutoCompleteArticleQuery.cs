using Espresso.Application.Infrastructure.MediatorInfrastructure;
using MediatR;

namespace Espresso.WebApi.Application.Articles.AutoCompleteArticle
{
    public record AutoCompleteArticleQuery : Request<AutoCompleteArticleQueryResponse>
    {
        public string? TitleSearchQuery { get; init; }

        public int Take { get; init; }
    }
}