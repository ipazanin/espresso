﻿// LanguageUtilityTest.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Domain.Utilities;
using Xunit;

namespace Espresso.Domain.Tests.Utilities;

public class LanguageUtilityTest
{
    [Fact]
    public void GetSearchTerms_ReturnsEmptyArray_WhenSearchTermIsNull()
    {
        var searchTerm = null as string;
        var expectedSearchTerms = Array.Empty<string>();

        var actualSearchTerms = LanguageUtility.GetSearchTerms(searchTerm);

        Assert.Equal(expected: expectedSearchTerms, actual: actualSearchTerms);
    }

    [Fact]
    public void GetSearchTerms_ReturnsSearchTerms_WhenDelimiterCharacterIsPresent()
    {
        const string? SearchTerm = "word1 word2";
        var expectedSearchTerms = new string[]
        {
                "word1",
                "word2",
        };

        var actualSearchTerms = LanguageUtility.GetSearchTerms(SearchTerm);

        Assert.Equal(expected: expectedSearchTerms, actual: actualSearchTerms);
    }

    [Fact]
    public void GetSearchTerms_ReturnsSearchTermsWithCroatianCharactersReplaced_WhenCroatianCharactersArePresent()
    {
        const string? SearchTerm = "word1ž word2č";
        var expectedSearchTerms = new string[]
        {
                "word1z",
                "word2c",
        };

        var actualSearchTerms = LanguageUtility.GetSearchTerms(SearchTerm);

        Assert.Equal(expected: expectedSearchTerms, actual: actualSearchTerms);
    }

    [Theory]
    [InlineData("word1,word2")]
    [InlineData("word1:word2")]
    [InlineData("word1;word2")]
    [InlineData("word1 word2")]
    [InlineData("word1\nword2")]
    [InlineData("word1.word2")]
    public void SeparateWords_SeparatesWordsByRecognizingDelimiterCharacters(string sentence)
    {
        var expectedWords = new string[]
        {
                "word1",
                "word2",
        };

        var actualWords = LanguageUtility.SeparateWords(sentence);

        Assert.Equal(expected: expectedWords, actual: actualWords);
    }

    [Theory]
    [InlineData("+/=")]
    [InlineData("+/=^|\\-")]
    public void SeparateWords_WithAcceptedCharacters_ReturnsEmptyArray(string sentence)
    {
        var expectedWords = Array.Empty<string>();

        var actualWords = LanguageUtility.SeparateWords(sentence);

        Assert.Equal(expected: expectedWords, actual: actualWords);
    }

    [Theory]
    [InlineData(",")]
    [InlineData(".")]
    [InlineData("\n")]
    [InlineData("..")]
    public void SeparateWords_WithDelimiterCharactersOnly_ReturnsEmptyArray(string sentence)
    {
        var expectedWords = Array.Empty<string>();

        var actualWords = LanguageUtility.SeparateWords(sentence);

        Assert.Equal(expected: expectedWords, actual: actualWords);
    }

    [Theory]
    [InlineData("ako")]
    [InlineData("ne")]
    [InlineData("u")]
    [InlineData("i")]
    [InlineData("je")]
    public void RemoveImpactfulCroatianWords_ReturnsWordsWithoutImpactfulCroatianWords_WhenImpactfulCroatianWordsArePresent(string notImpactfulCroatianWord)
    {
        var words = new List<string>
        {
            "Very",
            "Cool",
            "Title",
            notImpactfulCroatianWord
        };

        var expectedWords = new string[]
        {
                "Very",
                "Cool",
                "Title",
        };

        var actualWords = words.RemoveUnImpactfulCroatianWords();

        Assert.Equal(expected: expectedWords, actual: actualWords);
    }

    [Fact]
    public void RemoveUnImpactfulCroatianWords_ReturnsAllWords_WhenThereAreNoUnImpactfulWords()
    {
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

        var actualWords = words.RemoveUnImpactfulCroatianWords();

        Assert.Equal(expected: expectedWords, actual: actualWords);
    }

    [Fact]
    public void RemoveUnImpactfulCroatianWords_ReturnsNoWords_WhenThereAreOnlyUnImpactfulWords()
    {
        var words = new List<string>
            {
                "ako",
                "ne",
                "ili",
            };

        var expectedWords = Array.Empty<string>();

        var actualWords = words.RemoveUnImpactfulCroatianWords();

        Assert.Equal(expected: expectedWords, actual: actualWords);
    }
}
