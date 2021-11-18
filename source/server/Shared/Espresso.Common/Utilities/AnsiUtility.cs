// AnsiUtility.cs
//
// © 2021 Espresso News. All rights reserved.

namespace Espresso.Common.Utilities
{
    /// <summary>
    /// ANSI text format utility.
    /// </summary>
    public static class AnsiUtility
    {
#pragma warning disable S125 // Sections of code should not be commented out
        private const string Bold = "1";

        // private const string Faint = "2";
        private const string Italic = "3";
        private const string Underline = "4";

        // private const string BlackColor = "30";
        private const string RedColor = "31";
        private const string GreenColor = "32";
        private const string YellowColor = "33";
        private const string BlueColor = "34";
        private const string MagentaColor = "35";
        private const string CyanColor = "36";
        private const string WhiteColor = "37";

        // private const string BackgroundBlackColor = "40";
        // private const string BackgroundRedColor = "41";
        // private const string BackgroundGreenColor = "42";
        // private const string BackgroundYellowColor = "43";
        // private const string BackgroundBlueColor = "44";
        // private const string BackgroundMagentaColor = "45";
        // private const string BackgroundCyanColor = "46";
        // private const string BackgroundWhiteColor = "47";
#pragma warning restore S125 // Sections of code should not be commented out

        /// <summary>
        /// Encodes <paramref name="value"/> as parameter name.
        /// </summary>
        /// <param name="value">Value to encode.</param>
        /// <returns>Encoded <paramref name="value"/>.</returns>
        public static string EncodeParameterName(string value)
        {
            return Encode(value, GreenColor, Bold);
        }

        /// <summary>
        /// Encodes <paramref name="parameterNumber"/> as event name.
        /// </summary>
        /// <param name="parameterNumber">Value to encode.</param>
        /// <returns>Encoded <paramref name="parameterNumber"/>.</returns>
        public static string EncodeEventName(int parameterNumber)
        {
            return Encode($"{{{parameterNumber}}}", YellowColor, Bold, Underline);
        }

        /// <summary>
        /// Encodes <paramref name="parameterNumber"/> as error message.
        /// </summary>
        /// <param name="parameterNumber">Value to encode.</param>
        /// <returns>Encoded <paramref name="parameterNumber"/>.</returns>
        public static string EncodeErrorMessage(int parameterNumber)
        {
            return Encode($"{{{parameterNumber}}}", RedColor);
        }

        /// <summary>
        /// Encodes <paramref name="parameterNumber"/> as <see cref="TimeSpan"/>.
        /// </summary>
        /// <param name="parameterNumber">Value to encode.</param>
        /// <returns>Encoded <paramref name="parameterNumber"/>.</returns>
        public static string EncodeTimespan(int parameterNumber)
        {
            return Encode($"{{{parameterNumber}}}", CyanColor);
        }

        /// <summary>
        /// Encodes <paramref name="parameterNumber"/> as <see cref="DateTime"/>.
        /// </summary>
        /// <param name="parameterNumber">Value to encode.</param>
        /// <returns>Encoded <paramref name="parameterNumber"/>.</returns>
        public static string EncodeDateTime(int parameterNumber)
        {
            return Encode($"{{{parameterNumber}}}", BlueColor);
        }

        /// <summary>
        /// Encodes <paramref name="parameterNumber"/> as <see cref="string"/>.
        /// </summary>
        /// <param name="parameterNumber">Value to encode.</param>
        /// <returns>Encoded <paramref name="parameterNumber"/>.</returns>
        public static string EncodeString(int parameterNumber)
        {
            return Encode($"{{{parameterNumber}}}", WhiteColor, Italic);
        }

        /// <summary>
        /// Encodes <paramref name="parameterNumber"/> as <see cref="Enum"/>.
        /// </summary>
        /// <param name="parameterNumber">Value to encode.</param>
        /// <returns>Encoded <paramref name="parameterNumber"/>.</returns>
        public static string EncodeEnum(int parameterNumber)
        {
            return Encode($"{{{parameterNumber}}}", YellowColor);
        }

        /// <summary>
        /// Encodes <paramref name="parameterNumber"/> as number.
        /// </summary>
        /// <param name="parameterNumber">Value to encode.</param>
        /// <returns>Encoded <paramref name="parameterNumber"/>.</returns>
        public static string EncodeNumber(int parameterNumber)
        {
            return Encode($"{{{parameterNumber}}}", MagentaColor);
        }

        /// <summary>
        /// Encodes <paramref name="parameterNumber"/> as <see cref="object"/>.
        /// </summary>
        /// <param name="parameterNumber">Value to encode.</param>
        /// <returns>Encoded <paramref name="parameterNumber"/>.</returns>
        public static string EncodeObject(int parameterNumber)
        {
            return Encode($"{{{parameterNumber}}}", MagentaColor);
        }

        private static string Encode(string value, params object?[] parameters)
        {
            return $"\u001b[{string.Join(";", parameters)}m{value}\u001b[0m";
        }
    }
}
