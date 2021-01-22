using System;
using Espresso.Domain.Entities;
using Espresso.Domain.Records;
using FluentValidation;

namespace Espresso.Dashboard.ParseRssFeeds.Validators
{
    public class ArticleDataValidator : AbstractValidator<ArticleData>
    {
        public ArticleDataValidator()
        {
            RuleFor(articleData => articleData.Id)
                .NotEmpty();

            RuleFor(articleData => articleData.Url)
                .Must(url => IsUrl(url))
                .MaximumLength(Article.UrlMaxLength);

            RuleFor(articleData => articleData.WebUrl)
                .Must(webUrl => IsUrl(webUrl))
                .MaximumLength(Article.WebUrlMaxLength);

            RuleFor(articleData => articleData.Summary)
                .NotEmpty()
                .MaximumLength(Article.SummaryMaxLength);

            RuleFor(articleData => articleData.Title)
                .NotEmpty()
                .MaximumLength(Article.TitleMaxLength);

            RuleFor(articleData => articleData.WebUrl)
              .Must(webUrl => webUrl is null || IsUrl(webUrl))
              .MaximumLength(Article.ImageUrlMaxLength);

            RuleFor(articleData => articleData.PublishDateTime)
                .NotEmpty();

            RuleFor(articleData => articleData.CreateDateTime)
                .NotEmpty();

            RuleFor(articleData => articleData.UpdateDateTime)
                .NotEmpty();

            RuleFor(articleData => articleData.NumberOfClicks)
                .GreaterThanOrEqualTo(0);

            RuleFor(articleData => articleData.ArticleCategories)
                .NotEmpty();
        }

        protected static bool IsUrl(string? value)
        {
            return Uri.TryCreate(value, UriKind.Absolute, out var uriResult) &&
                (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
