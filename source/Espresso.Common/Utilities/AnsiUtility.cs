namespace Espresso.Common.Utilities
{
    public static class AnsiUtility
    {
        #region Constants

        #region Formaters
        private const string Bold = "1";
        // private const string Faint = "2";
        private const string Italic = "3";
        private const string Underline = "4";
        #endregion

        #region ForegroundColors
        // private const string BlackColor = "30";
        private const string RedColor = "31";
        private const string GreenColor = "32";
        private const string YellowColor = "33";
        private const string BlueColor = "34";
        // private const string MagentaColor = "35";
        private const string CyanColor = "36";
        private const string WhiteColor = "37";
        #endregion

        #region BackgroudColors
        // private const string BackgroundBlackColor = "40";
        // private const string BackgroundRedColor = "41";
        // private const string BackgroundGreenColor = "42";
        // private const string BackgroundYellowColor = "43";
        // private const string BackgroundBlueColor = "44";
        // private const string BackgroundMagentaColor = "45";
        // private const string BackgroundCyanColor = "46";
        // private const string BackgroundWhiteColor = "47";
        #endregion

        #endregion

        #region Methods
        private static string Encode(string value, params object?[] parameters)
        {
            return $"\u001b[{string.Join(";", parameters)}m{value}\u001b[0m";
        }

        public static string EncodeEventName(string value)
        {
            return Encode(value, YellowColor, Bold, Underline);
        }

        public static string EncodeParameterName(string value)
        {
            return Encode(value, GreenColor, Bold);
        }

        public static string EncodeDuration(string value)
        {
            return Encode(value, CyanColor);
        }

        public static string EncodeDateTime(string value)
        {
            return Encode(value, BlueColor);
        }

        public static string EncodeVersion(string value)
        {
            return Encode(value, WhiteColor, Italic);
        }

        public static string EncodeDeviceType(string value)
        {
            return Encode(value, YellowColor);
        }

        public static string EncodeRequestParameters(string value)
        {
            return Encode(value, WhiteColor);
        }

        public static string EncodeResponse(string value)
        {
            return Encode(value, WhiteColor);
        }

        public static string EncodeErrorMessage(string value)
        {
            return Encode(value, RedColor);
        }
        #endregion
    }
}
