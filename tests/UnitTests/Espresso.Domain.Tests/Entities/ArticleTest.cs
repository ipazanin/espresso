using Xunit;
using Espresso.Domain.Entities;

namespace Espresso.Domain.Tests
{
    public class AnsiUtilityTest
    {
        #region IncrementNumberOfClicks
        [Fact]
        public void IncrementNumberOfClicks_IncreasesNumberOfClicksByOne()
        {
            #region Arrange
            var numberOfClicks = 0;
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
                rssFeed: default
            );
            #endregion

            #region Act
            article.IncrementNumberOfClicks();
            #endregion

            #region Assert
            var expectedNumberOfClicks = numberOfClicks + 1;
            var actualNumberOfClicks = article.NumberOfClicks;
            Assert.Equal(expectedNumberOfClicks, actualNumberOfClicks);
            #endregion
        }
        #endregion
    }
}