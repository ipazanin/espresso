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
        const int Value = 0;
        var expectedAnsivalue =
            "\u001b[" +
            "33;1;4" +
            "m" +
            $"{{{Value}}}" +
            "\u001b[0m";

        var actualAnsivalue = AnsiUtility.EncodeEventName(Value);

        Assert.Equal(
            expected: expectedAnsivalue,
            actual: actualAnsivalue);
    }

    [Fact]
    public void EncodeParameterName_EncodesStringWithGreenColorAndBold()
    {
        const string? ParameterName = "parameterName";
        var expectedAnsivalue =
            "\u001b[" +
            "32;1" +
            "m" +
            ParameterName +
            "\u001b[0m";

        var actualAnsivalue = AnsiUtility.EncodeParameterName(ParameterName);

        Assert.Equal(
            expected: expectedAnsivalue,
            actual: actualAnsivalue);
    }

    [Fact]
    public void EncodeDuration_EncodesStringWithCyanColor()
    {
        const int Value = 0;
        var expectedAnsivalue =
            "\u001b[" +
            "36" +
            "m" +
            $"{{{Value}}}" +
            "\u001b[0m";

        var actualAnsivalue = AnsiUtility.EncodeTimespan(Value);

        Assert.Equal(
            expected: expectedAnsivalue,
            actual: actualAnsivalue);
    }

    [Fact]
    public void EncodeDateTime_EncodesStringWithBlueColor()
    {
        const int Value = 0;
        var expectedAnsivalue =
            "\u001b[" +
            "34" +
            "m" +
            $"{{{Value}}}" +
            "\u001b[0m";

        var actualAnsivalue = AnsiUtility.EncodeDateTime(Value);

        Assert.Equal(
            expected: expectedAnsivalue,
            actual: actualAnsivalue);
    }

    [Fact]
    public void EncodeString_EncodesStringWithWhiteColorAndItalic()
    {
        const int Value = 0;
        var expectedAnsivalue =
            "\u001b[" +
            "37" +
            ";3" +
            "m" +
            $"{{{Value}}}" +
            "\u001b[0m";

        var actualAnsivalue = AnsiUtility.EncodeString(Value);

        Assert.Equal(
            expected: expectedAnsivalue,
            actual: actualAnsivalue);
    }

    [Fact]
    public void EncodeDeviceType_EncodesStringWithYellowColor()
    {
        const int Value = 0;
        var expectedAnsivalue =
            "\u001b[" +
            "33" +
            "m" +
            $"{{{Value}}}" +
            "\u001b[0m";

        var actualAnsivalue = AnsiUtility.EncodeEnum(Value);

        Assert.Equal(
            expected: expectedAnsivalue,
            actual: actualAnsivalue);
    }

    [Fact]
    public void EncodeErrorMessage_EncodesStringWithRedColor()
    {
        const int Value = 0;
        var expectedAnsivalue =
            "\u001b[" +
            "31" +
            "m" +
            $"{{{Value}}}" +
            "\u001b[0m";

        var actualAnsivalue = AnsiUtility.EncodeErrorMessage(Value);

        Assert.Equal(
            expected: expectedAnsivalue,
            actual: actualAnsivalue);
    }

    [Fact]
    public void EncodeNumber_EncodesStringWithMagentaColor()
    {
        const int Value = 0;
        var expectedAnsivalue =
            "\u001b[" +
            "35" +
            "m" +
            $"{{{Value}}}" +
            "\u001b[0m";

        var actualAnsivalue = AnsiUtility.EncodeNumber(Value);

        Assert.Equal(
            expected: expectedAnsivalue,
            actual: actualAnsivalue);
    }

    [Fact]
    public void EncodeObject_EncodesStringWithMagentaColor()
    {
        const int Value = 0;
        var expectedAnsivalue =
            "\u001b[" +
            "35" +
            "m" +
            $"{{{Value}}}" +
            "\u001b[0m";

        var actualAnsivalue = AnsiUtility.EncodeObject(Value);

        Assert.Equal(
            expected: expectedAnsivalue,
            actual: actualAnsivalue);
    }
}
