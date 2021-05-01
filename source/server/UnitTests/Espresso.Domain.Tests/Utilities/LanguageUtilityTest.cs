using System;
using System.Collections.Generic;
using Espresso.Domain.Utilities;
using Xunit;

namespace Espresso.Domain.Tests.Utilities
{
    public class LanguageUtilityTest
    {
        #region GetSearchTerms
        [Fact]
        public void GetSearchTerms_ReturnsEmptyArray_WhenSearchTermIsNull()
        {
            #region Arrange
            var searchTerm = null as string;
            var expectedSearchTerms = Array.Empty<string>();
            #endregion

            #region Act
            var actualSearchTerms = LanguageUtility.GetSearchTerms(searchTerm);
            #endregion

            #region Assert
            Assert.Equal(expected: expectedSearchTerms, actual: actualSearchTerms);
            #endregion
        }

        [Fact]
        public void GetSearchTerms_ReturnsSearchTerms_WhenDelimiterCharacterIsPresent()
        {
            #region Arrange
            const string? SearchTerm = "word1 word2";
            var expectedSearchTerms = new string[]
            {
                "word1",
                "word2",
            };
            #endregion

            #region Act
            var actualSearchTerms = LanguageUtility.GetSearchTerms(SearchTerm);
            #endregion

            #region Assert
            Assert.Equal(expected: expectedSearchTerms, actual: actualSearchTerms);
            #endregion
        }

        [Fact]
        public void GetSearchTerms_ReturnsSearchTermsWithCroatianCharactersReplaced_WhenCroatianCharactersArePresent()
        {
            #region Arrange
            const string? SearchTerm = "word1ž word2č";
            var expectedSearchTerms = new string[]
            {
                "word1z",
                "word2c",
            };
            #endregion

            #region Act
            var actualSearchTerms = LanguageUtility.GetSearchTerms(SearchTerm);
            #endregion

            #region Assert
            Assert.Equal(expected: expectedSearchTerms, actual: actualSearchTerms);
            #endregion
        }
        #endregion

        #region SeparateWords
        [Theory]
        [InlineData("word1,word2")]
        [InlineData("word1:word2")]
        [InlineData("word1;word2")]
        [InlineData("word1 word2")]
        [InlineData("word1\nword2")]
        [InlineData("word1.word2")]
        public void SeparateWords_SeparatesWordsByRecognisingDelimiterCharacters(string sentence)
        {
            #region Arrange
            var expectedWords = new string[]
            {
                "word1",
                "word2",
            };
            #endregion

            #region Act
            var actualWords = LanguageUtility.SeparateWords(sentence);
            #endregion

            #region Assert
            Assert.Equal(expected: expectedWords, actual: actualWords);
            #endregion
        }

        [Theory]
        [InlineData("+/=")]
        [InlineData("+/=^|\\-")]
        public void SeparateWords_WithAcceptedCharacters_ReturnsEmptyArray(string sentence)
        {
            #region Arrange
            var expectedWords = Array.Empty<string>();
            #endregion

            #region Act
            var actualWords = LanguageUtility.SeparateWords(sentence);
            #endregion

            #region Assert
            Assert.Equal(expected: expectedWords, actual: actualWords);
            #endregion
        }

        [Theory]
        [InlineData(",")]
        [InlineData(".")]
        [InlineData("\n")]
        [InlineData("..")]
        public void SeparateWords_WithDelimiterCharactersOnly_ReturnsEmptyArray(string sentence)
        {
            #region Arrange
            var expectedWords = Array.Empty<string>();
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
        [Theory]
        [InlineData("ako")]
        [InlineData("ne")]
        [InlineData("u")]
        [InlineData("i")]
        [InlineData("je")]
        public void RemoveUnimpactfulCroatianWords_ReturnsWordsWithoutUnimpactfulCroatianWords_WhenUnimpactfulCroatianWordsArePresent(string notImpactfulCroatianWord)
        {
            #region Arrange
            var words = new List<string>
            {
                "Very",
                "Cool",
                "Title",
            };
            words.Add(notImpactfulCroatianWord);

            var expectedWords = new string[]
            {
                "Very",
                "Cool",
                "Title",
            };
            #endregion

            #region Act
            var actualWords = words.RemoveUnImpactfulCroatianWords();
            #endregion

            #region Assert
            Assert.Equal(expected: expectedWords, actual: actualWords);
            #endregion
        }

        [Fact]
        public void RemoveUnimpactfulCroatianWords_ReturnsAllWords_WhenThereAreNoUnimpactfulWords()
        {
            #region Arrange
            var words = new List<string>
            {
                "Very",
                "Cool",
                "Title",
            };

            var expectedWords = new string[]
            {
                "Very",
                "Cool",
                "Title",
            };
            #endregion

            #region Act
            var actualWords = words.RemoveUnImpactfulCroatianWords();
            #endregion

            #region Assert
            Assert.Equal(expected: expectedWords, actual: actualWords);
            #endregion
        }

        [Fact]
        public void RemoveUnimpactfulCroatianWords_ReturnsNoWords_WhenThereAreOnlyUnimpactfulWords()
        {
            #region Arrange
            var words = new List<string>
            {
                "ako",
                "ne",
                "ili",
            };

            var expectedWords = Array.Empty<string>();
            #endregion

            #region Act
            var actualWords = words.RemoveUnImpactfulCroatianWords();
            #endregion

            #region Assert
            Assert.Equal(expected: expectedWords, actual: actualWords);
            #endregion
        }
        #endregion
    }
}
