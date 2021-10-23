// AnsiUtilityTest.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using Xunit;

namespace Espresso.Domain.Tests.Entities
{
    public class AnsiUtilityTest
    {
        [Fact]
        public void IncrementNumberOfClicks_IncreasesNumberOfClicksByOne()
        {
            const int numberOfClicks = 0;
            var article = new Article(
                id: default,
                url: default!,
                webUrl: default!,
                summary: default!,
                title: default!,
                imageUrl: default,
                createDateTime: default,
                updateDateTime: default,
                publishDateTime: default,
                numberOfClicks: numberOfClicks,
                trendingScore: default,
                editorConfiguration: default!,
                newsPortalId: default,
                rssFeedId: default,
                articleCategories: default,
                newsPortal: default,
                rssFeed: default,
                subordinateArticles: default,
                mainArticle: default);

            article.IncrementNumberOfClicks();

            const int expectedNumberOfClicks = numberOfClicks + 1;
            var actualNumberOfClicks = article.NumberOfClicks;
            Assert.Equal(expectedNumberOfClicks, actualNumberOfClicks);
        }
    }
}
