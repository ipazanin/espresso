using System.Text;

namespace Espresso.Common.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Removes extra white space characters
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
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

        public static string ReplaceCroatianCharactersRegex(this string input)
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
