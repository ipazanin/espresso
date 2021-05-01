using System;
using System.Collections.Generic;
using System.Linq;
using Espresso.Domain.Entities;
using Espresso.Domain.IServices;
using Espresso.Domain.Services;
using Espresso.Domain.Tests.TestUtilities;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Espresso.Domain.Tests.Services
{
    public class GroupSimilarArticlesServiceTest
    {
        #region GroupSimilarArticles
        [InlineData("Very Cool Title", "Very Cool Title")]
        [InlineData("Very Cool Title", "Title Cool Ver Very")]
        [InlineData("Robert Mikac: Od rujna kriza s covidom izmakla kontroli", "Stručnjak za krizna upravljanja: Covid kriza od rujna je izmakla Vladinoj kontroli. Treba uključiti sustav domovinske sigurnost. Predlažem tri koraka")]
        [InlineData("Robert Mikac: Od rujna kriza s covidom izmakla kontroli", "Mikac: Od rujna kriza izazvana koronavirusom izmakla Vladinoj kontroli")]
        [InlineData("Modernino cjepivo pruža imunitet najmanje tri mjeseca", "Najnovija studija pokazala da Modernino cjepivo protiv koronavirusa pruža imunitet najmanje tri mjeseca, u prvoj fazi plasirat će 125 milijuna doza")]
        [InlineData("Biden pozvao Faucija da se pridruži njegovu timu za borbu protiv pandemije", "Biden pozvao Faucija da se pridruži njegovu timu: Zamolio sam ga da mi bude glavni medicinski savjetnik...")]
        [InlineData("Broj umrlih od koronavirusa u Velikoj Britaniji premašio 60 tisuća", "U iščekivanju cjepiva koje stiže idući tjedan, broj umrlih od korone u Velikoj Britaniji premašio 60 tisuća")]
        [InlineData("EL: Još sedam klubova osiguralo prolaz", "Milan, Lille i LASK napravili preokrete, još sedam klubova osiguralo prolaz u europsko proljeće Europa lige")]
        [InlineData("Odron na magistrali, otežan promet prema jugu", "Odron na Jadranskoj magistrali kod Podgore, prema jugu se vozi otežano")]
        [InlineData("Plenković: WHO treba očuvati kao vodećeg autoriteta i čuvara javnog zdravstva", "Andrej Plenković iz izolacije govorio na sjednici UN-a: WHO treba očuvati kao vodeći autoritet i čuvara javnog zdravstva na globalnoj razini")]
        [InlineData("Rijeka vodi protiv Reala!", "[FOTO, VIDEO] Luda utakmica u San Sebastianu; Rijeka dva puta vodila, Real Sociedad dva puta došao do izjednačenja")]
        [InlineData("Dinamo je osigurao europsko proljeće!", "Dinamo osigurao novo europsko proljeće, Rijeka uzela prvi bod!")]
        [Theory]
        public void GroupSimilarArticles_ReturnsSimilarArticleCollectionWithOneSimmilarArticle_WhenArticlesSimmilarityPassesGivenThreshold(
            string firstArticleTitle,
            string secondArticleTitle
        )
        {
            #region Arrange
            var loggerServiceMock = new Mock<ILoggerService<GroupSimilarArticlesService>>(MockBehavior.Strict);

            loggerServiceMock.Setup(loggerService => loggerService.Log(
                It.IsAny<string>(),
                It.IsAny<LogLevel>(),
                It.IsAny<IEnumerable<(string argumentName, object argumentValue)>>()
            ));

            const int ExpectedSimilarArticlesCount = 1;

            var articles = new List<Article>
            {
                ArticleUtility.CreateDefaultArticleWith(
                    newsPortalId: 1,
                    title: firstArticleTitle,
                    publishDateTime: DateTime.UtcNow,
                    createDateTime: DateTime.UtcNow,
                    categoryId: 1,
                    id: Guid.NewGuid()
                ),
                ArticleUtility.CreateDefaultArticleWith(
                    newsPortalId: 2,
                    title: secondArticleTitle,
                    publishDateTime: DateTime.UtcNow,
                    createDateTime: DateTime.UtcNow,
                    categoryId: 2,
                    id: Guid.NewGuid()
                ),
            };
            var service = new GroupSimilarArticlesService(
                similarityScoreThreshold: 0.6,
                articlePublishDateTimeDiferenceThreshold: TimeSpan.FromHours(16),
                loggerService: loggerServiceMock.Object,
                maxAgeOfSimilarArticleChecking: TimeSpan.FromHours(32),
                minimalNumberOfWordsForArticleToBeComparable: 1
            );
            #endregion

            #region Act
            var similarArticles = service.GroupSimilarArticles(articles, new HashSet<Guid>(), default);
            var actualSimilarArticlesCount = similarArticles.Count();
            #endregion

            #region Assert
            Assert.Equal(
                expected: ExpectedSimilarArticlesCount,
                actual: actualSimilarArticlesCount
            );
            #endregion
        }

        [Fact]
        public void GroupSimilarArticles_ReturnsSimilarArticleCollectionWithZeroSimmilarArticles_WhenArticlesListIsEmpty()
        {
            #region Arrange
            var loggerServiceMock = new Mock<ILoggerService<GroupSimilarArticlesService>>(MockBehavior.Strict);

            loggerServiceMock.Setup(loggerService => loggerService.Log(
                It.IsAny<string>(),
                It.IsAny<LogLevel>(),
                It.IsAny<IEnumerable<(string argumentName, object argumentValue)>>()
            ));

            const int ExpectedSimilarArticlesCount = 0;

            var articles = new List<Article>();
            var service = new GroupSimilarArticlesService(
                similarityScoreThreshold: 0.6,
                articlePublishDateTimeDiferenceThreshold: TimeSpan.FromHours(16),
                loggerService: loggerServiceMock.Object,
                maxAgeOfSimilarArticleChecking: TimeSpan.FromHours(32),
                minimalNumberOfWordsForArticleToBeComparable: 1
            );
            #endregion

            #region Act
            var similarArticles = service.GroupSimilarArticles(articles, new HashSet<Guid>(), default);
            var actualSimilarArticlesCount = similarArticles.Count();
            #endregion

            #region Assert
            Assert.Equal(
                expected: ExpectedSimilarArticlesCount,
                actual: actualSimilarArticlesCount
            );
            #endregion
        }

        [Fact]
        public void GroupSimilarArticles_ReturnsSimilarArticleCollectionWithZeroSimmilarArticles_WhenArticlesListContainsOneArticle()
        {
            #region Arrange
            var loggerServiceMock = new Mock<ILoggerService<GroupSimilarArticlesService>>(MockBehavior.Strict);

            loggerServiceMock.Setup(loggerService => loggerService.Log(
                It.IsAny<string>(),
                It.IsAny<LogLevel>(),
                It.IsAny<IEnumerable<(string argumentName, object argumentValue)>>()
            ));

            const int ExpectedSimilarArticlesCount = 0;

            var articles = new List<Article>
            {
                ArticleUtility.CreateDefaultArticleWith(
                    newsPortalId: 1,
                    title: "Very Cool Title One",
                    publishDateTime: DateTime.UtcNow,
                    createDateTime: DateTime.UtcNow,
                    categoryId: 1,
                    id: Guid.NewGuid()
                ),
            };
            var service = new GroupSimilarArticlesService(
                similarityScoreThreshold: 0.6,
                articlePublishDateTimeDiferenceThreshold: TimeSpan.FromHours(16),
                loggerService: loggerServiceMock.Object,
                maxAgeOfSimilarArticleChecking: TimeSpan.FromHours(32),
                minimalNumberOfWordsForArticleToBeComparable: 1
            );
            #endregion

            #region Act
            var similarArticles = service.GroupSimilarArticles(articles, new HashSet<Guid>(), default);
            var actualSimilarArticlesCount = similarArticles.Count();
            #endregion

            #region Assert
            Assert.Equal(
                expected: ExpectedSimilarArticlesCount,
                actual: actualSimilarArticlesCount
            );
            #endregion
        }

        [InlineData("Very Cool Title", "Very Different NotTitle")]
        [InlineData("Something Something", "Otherthing Otherthing")]
        [Theory]
        public void GroupSimilarArticles_ReturnsSimilarArticleCollectionWithZeroSimmilarArticles_WhenArticlesSimmilarityDoesntPassGivenThreshold(
            string firstArticleTitle,
            string secondArticleTitle
        )
        {
            #region Arrange
            var loggerServiceMock = new Mock<ILoggerService<GroupSimilarArticlesService>>(MockBehavior.Strict);

            loggerServiceMock.Setup(loggerService => loggerService.Log(
                It.IsAny<string>(),
                It.IsAny<LogLevel>(),
                It.IsAny<IEnumerable<(string argumentName, object argumentValue)>>()
            ));

            const int ExpectedSimilarArticlesCount = 0;

            var articles = new List<Article>
            {
                ArticleUtility.CreateDefaultArticleWith(
                    newsPortalId: 1,
                    title: firstArticleTitle,
                    publishDateTime: DateTime.UtcNow,
                    createDateTime: DateTime.UtcNow,
                    categoryId: 1,
                    id: Guid.NewGuid()
                ),
                ArticleUtility.CreateDefaultArticleWith(
                    newsPortalId: 2,
                    title: secondArticleTitle,
                    publishDateTime: DateTime.UtcNow,
                    createDateTime: DateTime.UtcNow,
                    categoryId: 2,
                    id: Guid.NewGuid()
                ),
            };
            var service = new GroupSimilarArticlesService(
                similarityScoreThreshold: 0.6,
                articlePublishDateTimeDiferenceThreshold: TimeSpan.FromHours(16),
                loggerService: loggerServiceMock.Object,
                maxAgeOfSimilarArticleChecking: TimeSpan.FromHours(32),
                minimalNumberOfWordsForArticleToBeComparable: 1
            );
            #endregion

            #region Act
            var similarArticles = service.GroupSimilarArticles(articles, new HashSet<Guid>(), default);
            var actualSimilarArticlesCount = similarArticles.Count();
            #endregion

            #region Assert
            Assert.Equal(
                expected: ExpectedSimilarArticlesCount,
                actual: actualSimilarArticlesCount
            );
            #endregion
        }

        [InlineData("Very Cool Title", "Very Cool Title")]
        [Theory]
        public void GroupSimilarArticles_ReturnsSimilarArticleCollectionWithZeroSimmilarArticles_WhenOneOfTwoArticlesIsToOldToBeCheckedForSimilarity(
            string firstArticleTitle,
            string secondArticleTitle
        )
        {
            #region Arrange
            var loggerServiceMock = new Mock<ILoggerService<GroupSimilarArticlesService>>(MockBehavior.Strict);

            loggerServiceMock.Setup(loggerService => loggerService.Log(
                It.IsAny<string>(),
                It.IsAny<LogLevel>(),
                It.IsAny<IEnumerable<(string argumentName, object argumentValue)>>()
            ));

            const int ExpectedSimilarArticlesCount = 0;

            var articles = new List<Article>
            {
                ArticleUtility.CreateDefaultArticleWith(
                    newsPortalId: 1,
                    title: firstArticleTitle,
                    publishDateTime: DateTime.UtcNow,
                    createDateTime: DateTime.UtcNow,
                    categoryId: 1,
                    id: Guid.NewGuid()
                ),
                ArticleUtility.CreateDefaultArticleWith(
                    newsPortalId: 2,
                    title: secondArticleTitle,
                    publishDateTime: DateTime.UtcNow.AddDays(-3),
                    createDateTime: DateTime.UtcNow.AddDays(-3),
                    categoryId: 2,
                    id: Guid.NewGuid()
                ),
            };
            var service = new GroupSimilarArticlesService(
                similarityScoreThreshold: 0.6,
                articlePublishDateTimeDiferenceThreshold: TimeSpan.FromHours(16),
                loggerService: loggerServiceMock.Object,
                maxAgeOfSimilarArticleChecking: TimeSpan.FromHours(32),
                minimalNumberOfWordsForArticleToBeComparable: 1
            );
            #endregion

            #region Act
            var similarArticles = service.GroupSimilarArticles(articles, new HashSet<Guid>(), default);
            var actualSimilarArticlesCount = similarArticles.Count();
            #endregion

            #region Assert
            Assert.Equal(
                expected: ExpectedSimilarArticlesCount,
                actual: actualSimilarArticlesCount
            );
            #endregion
        }
        #endregion
    }
}
