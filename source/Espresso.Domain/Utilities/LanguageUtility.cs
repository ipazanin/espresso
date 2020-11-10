using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Espresso.Common.Extensions;

namespace Espresso.Domain.Utilities
{
    public static class LanguageUtility
    {
        private const string AllowedCharactersRegex = "([a-z]|[A-Z]|[0-9]|ž|Ž|đ|Đ|ć|Ć|č|Č|š|Š)";
        private const string DelimiterCharactersRegex = "( |\\.|;|:|,|\\n|$)";
        private const string StartOfWordCharactersRegex = "(^| |\n)";
        private const string UnimpactfulCroatianWordsRegex = "(u|i|je|na|se|su|što|zbog|do|te|samo|jer" +
            "|već|za|da|s|od|a|će|iz|koji|ne|kako|o|nije|bi|to|ali|još|sa|kao|koja|sve|biti|po|koje|ga" +
            "|bio|sam|bez|no|dok|mu|pa|li|oko|ako)";

        public static IEnumerable<string> GetSearchTerms(string? searchTerm)
        {
            if (searchTerm is null)
            {
                return Array.Empty<string>();
            }

            var searchTerms = SeparateWords(searchTerm);

            var searchTermsWithoutCroatianCharacters = searchTerms
                .Select(searchTerm => searchTerm.ReplaceCroatianCharacters());

            return searchTermsWithoutCroatianCharacters;
        }

        public static IEnumerable<string> SeparateWords(string sentence)
        {
            var words = Regex
                .Matches(sentence, $"{AllowedCharactersRegex}+")
                .Select(match => match.Value)
                .Where(word => !string.IsNullOrWhiteSpace(word));

            return words;
        }

        public static IEnumerable<string> MatchWordsThatBeginWithTerm(string term, string sentence)
        {
            var termWithCroatianCharacters = term.ReplaceCroatianCharactersRegex();
            var searchRegexPattern = $"{StartOfWordCharactersRegex}{term}{AllowedCharactersRegex}*{DelimiterCharactersRegex}";

            var matchedWords = Regex.Matches(sentence, searchRegexPattern, RegexOptions.IgnoreCase)
                .Select(match => Regex.Replace(match.Value, DelimiterCharactersRegex, ""));

            return matchedWords;
        }

        public static IEnumerable<string> RemoveUnimpactfulCroatianWords(this IEnumerable<string> words)
        {
            return words.Where(word => !Regex.IsMatch(word, $"^{UnimpactfulCroatianWordsRegex}$", RegexOptions.IgnoreCase));
        }

        public static string ReplaceCroatianCharacters(this string input)
        {
            var len = input.Length;
            var index = 0;
            var src = input.ToCharArray();
            char ch;
            for (var i = 0; i < len; i++)
            {
                ch = src[i];
                src[index++] = ch switch
                {
                    'ć' => 'c',
                    'Ć' => 'C',
                    'č' => 'c',
                    'Č' => 'C',
                    'ž' => 'z',
                    'Ž' => 'Z',
                    'š' => 's',
                    'Š' => 'S',
                    'đ' => 'd',
                    'Đ' => 'D',
                    _ => ch,
                };
            }

            return new string(src, 0, index);
        }

        private static string ReplaceCroatianCharactersRegex(this string input)
        {
            var len = input.Length;
            var src = input.ToCharArray();
            char ch;
            var builder = new StringBuilder();
            for (var i = 0; i < len; i++)
            {
                ch = src[i];
                var regexExpression = ch switch
                {
                    'ć' => "(ć|c|č)",
                    'Ć' => "(Ć|C|Č)",
                    'č' => "(č|c|ć)",
                    'Č' => "(Č|C|Ć)",
                    'ž' => "(ž|z)",
                    'Ž' => "(Ž|Z)",
                    'š' => "(š|s)",
                    'Š' => "(Š|S)",
                    'đ' => "(đ|d)",
                    'Đ' => "(Đ|D)",
                    'c' => "(ć|c|č)",
                    'C' => "(Ć|C|Č)",
                    'z' => "(ž|z)",
                    'Z' => "(Ž|Z)",
                    's' => "(š|s)",
                    'S' => "(Š|S)",
                    'd' => "(đ|d)",
                    'D' => "(Đ|D)",
                    _ => ch.ToString(),
                };

                builder.Append(regexExpression);
            }

            return builder.ToString();
        }
    }
}