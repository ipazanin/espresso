using System;
using System.Collections.Generic;

using Espresso.Domain.Entities;
using Espresso.Domain.IValidators;

namespace Espresso.Domain.Validators
{
    public class ArticleValidator : Validator, IArticleValidator
    {
        #region Methods
        public Article Validate(
            Guid id,
            string? articleId,
            string? url,
            string? summary,
            string? title,
            string? imageUrl,
            DateTime createDateTime,
            DateTime updateDateTime,
            DateTime? publishDateTime,
            int numberOfClicks,
            decimal trendingScore,
            int newsPortalId,
            int rssFeedId,
            IEnumerable<ArticleCategory> articleCategories,
            NewsPortal? newsPortal,
            RssFeed rssFeed
        )
        {
            NotEmpty(id, nameof(Article.Id));

            NotEmpty(articleId!, nameof(Article.ArticleId));
            MaxLength(articleId!, nameof(Article.ArticleId), Article.ArticleIdMaxLength);
            MustBeUrl(articleId!, nameof(Article.ArticleId));

            NotEmpty(url!, nameof(Article.Url));
            MaxLength(url!, nameof(Article.Url), Article.UrlMaxLength);
            MustBeUrl(url!, nameof(Article.Url));

            NotEmpty(summary!, nameof(Article.Summary));
            MaxLength(summary!, nameof(Article.Summary), Article.SummaryMaxLength);

            NotEmpty(title!, nameof(Article.Title));
            MaxLength(title!, nameof(Article.Title), Article.TitleMaxLength);

            if (imageUrl != null)
            {
                MaxLength(imageUrl, nameof(Article.ImageUrl), Article.ImageUrlMaxLength);
                MustBeUrl(imageUrl, nameof(Article.ImageUrl));
            }

            NotEmpty(publishDateTime, nameof(publishDateTime));

            NotEmpty(newsPortalId, nameof(newsPortalId));

            NotEmpty(rssFeedId, nameof(rssFeedId));

            NotEmpty(articleCategories, nameof(articleCategories));

            NotEmpty(newsPortal, nameof(newsPortal));

            return new Article(
                id: id,
                articleId: articleId!,
                url: url!,
                summary: summary!,
                title: title!,
                imageUrl: imageUrl,
                createDateTime: createDateTime,
                updateDateTime: updateDateTime,
                publishDateTime: publishDateTime!.Value,
                numberOfClicks: numberOfClicks,
                trendingScore: trendingScore,
                isHidden: Article.IsHiddenDefaultValue,
                isFeatured: Article.IsFeaturedDefaultValue,
                newsPortalId: newsPortalId,
                rssFeedId: rssFeedId,
                articleCategories: articleCategories,
                newsPortal: newsPortal,
                rssFeed: rssFeed
            );
        }
        #endregion
    }
}
