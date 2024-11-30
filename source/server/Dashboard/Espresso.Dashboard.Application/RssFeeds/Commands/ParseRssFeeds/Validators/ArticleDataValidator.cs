// ArticleDataValidator.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using Espresso.Domain.Records;
using FluentValidation;

namespace Espresso.Dashboard.Application.RssFeeds.Commands.ParseRssFeeds.Validators;

public class ArticleDataValidator : AbstractValidator<ArticleData>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ArticleDataValidator"/> class.
    /// </summary>
    public ArticleDataValidator()
    {
        _ = RuleFor(articleData => articleData.Id)
            .NotEmpty();

        _ = RuleFor(articleData => articleData.Url)
            .Must(IsUrl)
            .MaximumLength(Article.UrlMaxLength);

        _ = RuleFor(articleData => articleData.WebUrl)
            .Must(IsUrl)
            .MaximumLength(Article.WebUrlMaxLength);

        _ = RuleFor(articleData => articleData.Summary)
            .NotEmpty()
            .MaximumLength(Article.SummaryMaxLength);

        _ = RuleFor(articleData => articleData.Title)
            .NotEmpty()
            .MaximumLength(Article.TitleMaxLength);

        _ = RuleFor(articleData => articleData.WebUrl)
          .Must(webUrl => webUrl is null || IsUrl(webUrl))
          .MaximumLength(Article.ImageUrlMaxLength);

        _ = RuleFor(articleData => articleData.PublishDateTime)
            .NotEmpty();

        _ = RuleFor(articleData => articleData.CreateDateTime)
            .NotEmpty();

        _ = RuleFor(articleData => articleData.UpdateDateTime)
            .NotEmpty();

        _ = RuleFor(articleData => articleData.NumberOfClicks)
            .GreaterThanOrEqualTo(0);

        _ = RuleFor(articleData => articleData.ArticleCategories)
            .NotEmpty();
    }

    protected static bool IsUrl(string? value)
    {
        return Uri.TryCreate(value, UriKind.Absolute, out var uriResult) &&
            (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}
