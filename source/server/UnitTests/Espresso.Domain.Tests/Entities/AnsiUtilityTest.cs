// AnsiUtilityTest.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using Xunit;

namespace Espresso.Domain.Tests.Entities;

public class AnsiUtilityTest
{
    [Fact]
    public void IncrementNumberOfClicks_IncreasesNumberOfClicksByOne()
    {
        const int NumberOfClicks = 0;
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
            numberOfClicks: NumberOfClicks,
            trendingScore: default,
            editorConfiguration: default!,
            newsPortalId: default,
            rssFeedId: default,
            articleCategories: default,
            newsPortal: default,
            rssFeed: default,
            firstSimilarArticles: null,
            secondSimilarArticles: null);

        article.IncrementNumberOfClicks();

        const int ExpectedNumberOfClicks = NumberOfClicks + 1;
        var actualNumberOfClicks = article.NumberOfClicks;
        Assert.Equal(ExpectedNumberOfClicks, actualNumberOfClicks);
    }
}
