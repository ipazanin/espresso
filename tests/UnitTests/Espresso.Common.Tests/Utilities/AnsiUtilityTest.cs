using System;
using Espresso.Common.Constants;
using Espresso.Common.Utilities;
using Espresso.Domain.Utilities;
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
            var stringValue = "value";
            var expectedAnsiStringValue =
                "\u001b[" +
                "33;1;4" +
                "m" +
                stringValue +
                "\u001b[0m";
            #endregion

            #region Act
            var actualAnsiStringValue = AnsiUtility.EncodeRequestName(stringValue);
            #endregion

            #region Assert
            Assert.Equal(
                expected: expectedAnsiStringValue,
                actual: actualAnsiStringValue
            );
            #endregion
        }
        #endregion

        #region EncodeParameterName
        [Fact]
        public void EncodeParameterName_EncodesStringWithGreenColorAndBold()
        {
            #region Arrange
            var stringValue = "value";
            var expectedAnsiStringValue =
                "\u001b[" +
                "32;1" +
                "m" +
                stringValue +
                "\u001b[0m";
            #endregion

            #region Act
            var actualAnsiStringValue = AnsiUtility.EncodeParameterName(stringValue);
            #endregion

            #region Assert
            Assert.Equal(
                expected: expectedAnsiStringValue,
                actual: actualAnsiStringValue
            );
            #endregion
        }
        #endregion

        #region EncodeDuration
        [Fact]
        public void EncodeDuration_EncodesStringWithCyanColor()
        {
            #region Arrange
            var stringValue = "value";
            var expectedAnsiStringValue =
                "\u001b[" +
                "36" +
                "m" +
                stringValue +
                "\u001b[0m";
            #endregion

            #region Act
            var actualAnsiStringValue = AnsiUtility.EncodeDuration(stringValue);
            #endregion

            #region Assert
            Assert.Equal(
                expected: expectedAnsiStringValue,
                actual: actualAnsiStringValue
            );
            #endregion
        }
        #endregion

        #region EncodeDateTime
        [Fact]
        public void EncodeDateTime_EncodesStringWithBlueColor()
        {
            #region Arrange
            var stringValue = "value";
            var expectedAnsiStringValue =
                "\u001b[" +
                "34" +
                "m" +
                stringValue +
                "\u001b[0m";
            #endregion

            #region Act
            var actualAnsiStringValue = AnsiUtility.EncodeDateTime(stringValue);
            #endregion

            #region Assert
            Assert.Equal(
                expected: expectedAnsiStringValue,
                actual: actualAnsiStringValue
            );
            #endregion
        }
        #endregion

        #region EncodeVersion
        [Fact]
        public void EncodeVersion_EncodesStringWithWhiteColorAndItalic()
        {
            #region Arrange
            var stringValue = "value";
            var expectedAnsiStringValue =
                "\u001b[" +
                "37;" +
                "3" +
                "m" +
                stringValue +
                "\u001b[0m";
            #endregion

            #region Act
            var actualAnsiStringValue = AnsiUtility.EncodeVersion(stringValue);
            #endregion

            #region Assert
            Assert.Equal(
                expected: expectedAnsiStringValue,
                actual: actualAnsiStringValue
            );
            #endregion
        }
        #endregion

        #region EncodeDeviceType
        [Fact]
        public void EncodeDeviceType_EncodesStringWithYellowColor()
        {
            #region Arrange
            var stringValue = "value";
            var expectedAnsiStringValue =
                "\u001b[" +
                "33" +
                "m" +
                stringValue +
                "\u001b[0m";
            #endregion

            #region Act
            var actualAnsiStringValue = AnsiUtility.EncodeDeviceType(stringValue);
            #endregion

            #region Assert
            Assert.Equal(
                expected: expectedAnsiStringValue,
                actual: actualAnsiStringValue
            );
            #endregion
        }
        #endregion

        #region EncodeRequestParameters
        [Fact]
        public void EncodeRequestParameters_EncodesStringWithWhiteColor()
        {
            #region Arrange
            var stringValue = "value";
            var expectedAnsiStringValue =
                "\u001b[" +
                "37" +
                "m" +
                stringValue +
                "\u001b[0m";
            #endregion

            #region Act
            var actualAnsiStringValue = AnsiUtility.EncodeRequestParameters(stringValue);
            #endregion

            #region Assert
            Assert.Equal(
                expected: expectedAnsiStringValue,
                actual: actualAnsiStringValue
            );
            #endregion
        }
        #endregion

        #region EncodeResponse
        [Fact]
        public void EncodeResponse_EncodesStringWithWhiteColor()
        {
            #region Arrange
            var stringValue = "value";
            var expectedAnsiStringValue =
                "\u001b[" +
                "37" +
                "m" +
                stringValue +
                "\u001b[0m";
            #endregion

            #region Act
            var actualAnsiStringValue = AnsiUtility.EncodeResponse(stringValue);
            #endregion

            #region Assert
            Assert.Equal(
                expected: expectedAnsiStringValue,
                actual: actualAnsiStringValue
            );
            #endregion
        }
        #endregion

        #region EncodeErrorMessage
        [Fact]
        public void EncodeErrorMessage_EncodesStringWithRedColor()
        {
            #region Arrange
            var stringValue = "value";
            var expectedAnsiStringValue =
                "\u001b[" +
                "31" +
                "m" +
                stringValue +
                "\u001b[0m";
            #endregion

            #region Act
            var actualAnsiStringValue = AnsiUtility.EncodeErrorMessage(stringValue);
            #endregion

            #region Assert
            Assert.Equal(
                expected: expectedAnsiStringValue,
                actual: actualAnsiStringValue
            );
            #endregion
        }
        #endregion

        #endregion

    }
}
