// StringExtensions.cs
//
// © 2022 Espresso News. All rights reserved.

namespace Espresso.Common.Extensions;

/// <summary>
/// <see cref="string"/> extensions.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Removes extra white space characters from <paramref name="input"/>.
    /// </summary>
    /// <param name="input">Input <see cref="string"/>.</param>
    /// <returns>Resulting <see cref="string"/>.</returns>
    public static string RemoveExtraWhiteSpaceCharacters(this string input)
    {
        var len = input.Length;
        var index = 0;
        var src = input.ToCharArray();
        var skip = false;
        char ch;
        for (var i = 0; i < len; i++)
        {
            ch = src[i];
            switch (ch)
            {
                case '\u0020':
                case '\u00A0':
                case '\u1680':
                case '\u2000':
                case '\u2001':
                case '\u2002':
                case '\u2003':
                case '\u2004':
                case '\u2005':
                case '\u2006':
                case '\u2007':
                case '\u2008':
                case '\u2009':
                case '\u200A':
                case '\u202F':
                case '\u205F':
                case '\u3000':
                case '\u2028':
                case '\u2029':
                case '\u0009':
                case '\u000A':
                case '\u000B':
                case '\u000C':
                case '\u000D':
                case '\u0085':
                    if (skip)
                    {
                        continue;
                    }

                    src[index++] = ch;
                    skip = true;
                    continue;
                default:
                    skip = false;
                    src[index++] = ch;
                    continue;
            }
        }

        return new string(src, 0, index);
    }
}
