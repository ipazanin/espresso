using Espresso.Common.Utilities;
using Xunit;

namespace Espresso.Common.Tests.Utilities
{
    public class AnsiUtilityTest
    {

        #region  Methods

        #region EncodeRequestName
        [Fact]
        public void EncodeRequestName_EncodesStringWithYellowColorBoldAndUnderline()
        {
            #region Arrange
            var value = 0;
            var expectedAnsivalue =
                "\u001b[" +
                "33;1;4" +
                "m" +
                $"{{{value}}}" +
                "\u001b[0m";
            #endregion

            #region Act
            var actualAnsivalue = AnsiUtility.EncodeEventName(value);
            #endregion

            #region Assert
            Assert.Equal(
                expected: expectedAnsivalue,
                actual: actualAnsivalue
            );
            #endregion
        }
        #endregion

        #region EncodeParameterName
        [Fact]
        public void EncodeParameterName_EncodesStringWithGreenColorAndBold()
        {
            #region Arrange
            var parameterName = "parameterName";
            var expectedAnsivalue =
                "\u001b[" +
                "32;1" +
                "m" +
                parameterName +
                "\u001b[0m";
            #endregion

            #region Act
            var actualAnsivalue = AnsiUtility.EncodeParameterName(parameterName);
            #endregion

            #region Assert
            Assert.Equal(
                expected: expectedAnsivalue,
                actual: actualAnsivalue
            );
            #endregion
        }
        #endregion

        #region EncodeDuration
        [Fact]
        public void EncodeDuration_EncodesStringWithCyanColor()
        {
            #region Arrange
            var value = 0;
            var expectedAnsivalue =
                "\u001b[" +
                "36" +
                "m" +
                $"{{{value}}}" +
                "\u001b[0m";
            #endregion

            #region Act
            var actualAnsivalue = AnsiUtility.EncodeTimespan(value);
            #endregion

            #region Assert
            Assert.Equal(
                expected: expectedAnsivalue,
                actual: actualAnsivalue
            );
            #endregion
        }
        #endregion

        #region EncodeDateTime
        [Fact]
        public void EncodeDateTime_EncodesStringWithBlueColor()
        {
            #region Arrange
            var value = 0;
            var expectedAnsivalue =
                "\u001b[" +
                "34" +
                "m" +
                $"{{{value}}}" +
                "\u001b[0m";
            #endregion

            #region Act
            var actualAnsivalue = AnsiUtility.EncodeDateTime(value);
            #endregion

            #region Assert
            Assert.Equal(
                expected: expectedAnsivalue,
                actual: actualAnsivalue
            );
            #endregion
        }
        #endregion

        #region EncodeString
        [Fact]
        public void EncodeString_EncodesStringWithWhiteColorAndItalic()
        {
            #region Arrange
            var value = 0;
            var expectedAnsivalue =
                "\u001b[" +
                "37" +
                ";3" +
                "m" +
                $"{{{value}}}" +
                "\u001b[0m";
            #endregion

            #region Act
            var actualAnsivalue = AnsiUtility.EncodeString(value);
            #endregion

            #region Assert
            Assert.Equal(
                expected: expectedAnsivalue,
                actual: actualAnsivalue
            );
            #endregion
        }
        #endregion        

        #region EncodeDeviceType
        [Fact]
        public void EncodeDeviceType_EncodesStringWithYellowColor()
        {
            #region Arrange
            var value = 0;
            var expectedAnsivalue =
                "\u001b[" +
                "33" +
                "m" +
                $"{{{value}}}" +
                "\u001b[0m";
            #endregion

            #region Act
            var actualAnsivalue = AnsiUtility.EncodeEnum(value);
            #endregion

            #region Assert
            Assert.Equal(
                expected: expectedAnsivalue,
                actual: actualAnsivalue
            );
            #endregion
        }
        #endregion

        #region EncodeErrorMessage
        [Fact]
        public void EncodeErrorMessage_EncodesStringWithRedColor()
        {
            #region Arrange
            var value = 0;
            var expectedAnsivalue =
                "\u001b[" +
                "31" +
                "m" +
                $"{{{value}}}" +
                "\u001b[0m";
            #endregion

            #region Act
            var actualAnsivalue = AnsiUtility.EncodeErrorMessage(value);
            #endregion

            #region Assert
            Assert.Equal(
                expected: expectedAnsivalue,
                actual: actualAnsivalue
            );
            #endregion
        }
        #endregion   

        #region EncodeNumber
        [Fact]
        public void EncodeNumber_EncodesStringWithMagentaColor()
        {
            #region Arrange
            var value = 0;
            var expectedAnsivalue =
                "\u001b[" +
                "35" +
                "m" +
                $"{{{value}}}" +
                "\u001b[0m";
            #endregion

            #region Act
            var actualAnsivalue = AnsiUtility.EncodeNumber(value);
            #endregion

            #region Assert
            Assert.Equal(
                expected: expectedAnsivalue,
                actual: actualAnsivalue
            );
            #endregion
        }
        #endregion    

        #region EncodeObject
        [Fact]
        public void EncodeObject_EncodesStringWithMagentaColor()
        {
            #region Arrange
            var value = 0;
            var expectedAnsivalue =
                "\u001b[" +
                "35" +
                "m" +
                $"{{{value}}}" +
                "\u001b[0m";
            #endregion

            #region Act
            var actualAnsivalue = AnsiUtility.EncodeObject(value);
            #endregion

            #region Assert
            Assert.Equal(
                expected: expectedAnsivalue,
                actual: actualAnsivalue
            );
            #endregion
        }
        #endregion                     

        #endregion

    }
}
