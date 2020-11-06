using Xunit;
using Espresso.Domain.Utilities;

namespace Espresso.Domain.Tests.Utilities
{
    public class LanguageUtilityTest
    {
        #region SeparateWords
        [Fact]
        public void SeparateWords_SeparatesWordsByRecognisingDelimiterCharacters()
        {
            #region Arrange
            var sentence = "word1,word2:word3;word4 word5\nword6.word7";
            var expectedWords = new string[]
            {
                "word1",
                "word2",
                "word3",
                "word4",
                "word5",
                "word6",
                "word7",
            };
            #endregion

            #region Act
            var actualWords = LanguageUtility.SeparateWords(sentence);
            #endregion

            #region Assert
            Assert.Equal(expected: expectedWords, actual: actualWords);
            #endregion
        }
        #endregion

        #region RemoveUnimpactfulCroatianWords
        [Fact]
        public void RemoveUnimpactfulCroatianWords_RemovesWordsFromCroatianLanguageWhichDoNotHaveImpact()
        {
            #region Arrange
            var words = new string[]
            {
                "Very",
                "Cool",
                "Title",
                "ako"
            };
            var expectedWords = new string[]
            {
                "Very",
                "Cool",
                "Title"
            };
            #endregion

            #region Act
            var actualWords = LanguageUtility.RemoveUnimpactfulCroatianWords(words);
            #endregion

            #region Assert
            Assert.Equal(expected: expectedWords, actual: actualWords);
            #endregion
        }
        #endregion
    }
}