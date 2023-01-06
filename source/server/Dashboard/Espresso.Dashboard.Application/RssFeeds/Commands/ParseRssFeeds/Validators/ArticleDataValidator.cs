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
        RuleFor(articleData => articleData.Id)
            .NotEmpty();

        RuleFor(articleData => articleData.Url)
            .Must(IsUrl)
            .MaximumLength(Article.UrlMaxLength);

        RuleFor(articleData => articleData.WebUrl)
            .Must(IsUrl)
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
