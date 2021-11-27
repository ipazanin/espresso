// AnsiUtilityTest.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Common.Utilities;
using Xunit;

namespace Espresso.Common.Tests.Utilities;

public class AnsiUtilityTest
{
    [Fact]
    public void EncodeRequestName_EncodesStringWithYellowColorBoldAndUnderline()
    {
        const int value = 0;
        var expectedAnsivalue =
            "\u001b[" +
            "33;1;4" +
            "m" +
            $"{{{value}}}" +
            "\u001b[0m";

        var actualAnsivalue = AnsiUtility.EncodeEventName(value);

        Assert.Equal(
            expected: expectedAnsivalue,
            actual: actualAnsivalue);
    }

    [Fact]
    public void EncodeParameterName_EncodesStringWithGreenColorAndBold()
    {
        const string? parameterName = "parameterName";
        var expectedAnsivalue =
            "\u001b[" +
            "32;1" +
            "m" +
            parameterName +
            "\u001b[0m";

        var actualAnsivalue = AnsiUtility.EncodeParameterName(parameterName);

        Assert.Equal(
            expected: expectedAnsivalue,
            actual: actualAnsivalue);
    }

    [Fact]
    public void EncodeDuration_EncodesStringWithCyanColor()
    {
        const int value = 0;
        var expectedAnsivalue =
            "\u001b[" +
            "36" +
            "m" +
            $"{{{value}}}" +
            "\u001b[0m";

        var actualAnsivalue = AnsiUtility.EncodeTimespan(value);

        Assert.Equal(
            expected: expectedAnsivalue,
            actual: actualAnsivalue);
    }

    [Fact]
    public void EncodeDateTime_EncodesStringWithBlueColor()
    {
        const int value = 0;
        var expectedAnsivalue =
            "\u001b[" +
            "34" +
            "m" +
            $"{{{value}}}" +
            "\u001b[0m";

        var actualAnsivalue = AnsiUtility.EncodeDateTime(value);

        Assert.Equal(
            expected: expectedAnsivalue,
            actual: actualAnsivalue);
    }

    [Fact]
    public void EncodeString_EncodesStringWithWhiteColorAndItalic()
    {
        const int value = 0;
        var expectedAnsivalue =
            "\u001b[" +
            "37" +
            ";3" +
            "m" +
            $"{{{value}}}" +
            "\u001b[0m";

        var actualAnsivalue = AnsiUtility.EncodeString(value);

        Assert.Equal(
            expected: expectedAnsivalue,
            actual: actualAnsivalue);
    }

    [Fact]
    public void EncodeDeviceType_EncodesStringWithYellowColor()
    {
        const int value = 0;
        var expectedAnsivalue =
            "\u001b[" +
            "33" +
            "m" +
            $"{{{value}}}" +
            "\u001b[0m";

        var actualAnsivalue = AnsiUtility.EncodeEnum(value);

        Assert.Equal(
            expected: expectedAnsivalue,
            actual: actualAnsivalue);
    }

    [Fact]
    public void EncodeErrorMessage_EncodesStringWithRedColor()
    {
        const int value = 0;
        var expectedAnsivalue =
            "\u001b[" +
            "31" +
            "m" +
            $"{{{value}}}" +
            "\u001b[0m";

        var actualAnsivalue = AnsiUtility.EncodeErrorMessage(value);

        Assert.Equal(
            expected: expectedAnsivalue,
            actual: actualAnsivalue);
    }

    [Fact]
    public void EncodeNumber_EncodesStringWithMagentaColor()
    {
        const int value = 0;
        var expectedAnsivalue =
            "\u001b[" +
            "35" +
            "m" +
            $"{{{value}}}" +
            "\u001b[0m";

        var actualAnsivalue = AnsiUtility.EncodeNumber(value);

        Assert.Equal(
            expected: expectedAnsivalue,
            actual: actualAnsivalue);
    }

    [Fact]
    public void EncodeObject_EncodesStringWithMagentaColor()
    {
        const int value = 0;
        var expectedAnsivalue =
            "\u001b[" +
            "35" +
            "m" +
            $"{{{value}}}" +
            "\u001b[0m";

        var actualAnsivalue = AnsiUtility.EncodeObject(value);

        Assert.Equal(
            expected: expectedAnsivalue,
            actual: actualAnsivalue);
    }
}
