// EnumExtensionsTest.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Globalization;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.Common.Extensions;
using Xunit;

namespace Espresso.Common.Tests.Extensions;

public class EnumExtensionsTest
{
    [Fact]
    public void GetDisplayName_WithUndefinedEnum_ReturnsUndefinedString()
    {
        const AppEnvironment UndefinedEnum = (AppEnvironment)(-1);
        const string? ExpectedDisplayName = FormatConstants.Undefined;

        var actualDisplayName = UndefinedEnum.GetDisplayName();

        Assert.Equal(
            expected: ExpectedDisplayName,
            actual: actualDisplayName);
    }

    [Fact]
    public void GetDisplayName_WithEnumValueWithoutDisplayNameAttribute_ReturnsEnumStringValue()
    {
        const AppEnvironment EnumValueWithoutDisplayAttribute = AppEnvironment.Local;
        var expectedDisplayName = EnumValueWithoutDisplayAttribute.ToString();

        var actualDisplayName = EnumValueWithoutDisplayAttribute.GetDisplayName();

        Assert.Equal(
            expected: expectedDisplayName,
            actual: actualDisplayName);
    }

    [Fact]
    public void GetDisplayName_WithEnumValueWithDisplayNameAttribute_ReturnsDisplayNameValue()
    {
        const AppEnvironment EnumValueWithDisplayAttribute = AppEnvironment.Prod;
        const string? ExpectedDisplayName = "Production";

        var actualDisplayName = EnumValueWithDisplayAttribute.GetDisplayName();

        Assert.Equal(
            expected: ExpectedDisplayName,
            actual: actualDisplayName);
    }

    [Fact]
    public void GetIntegerValueAsString_WithUndefinedEnum_ReturnsIntegerValueAsString()
    {
        const AppEnvironment UndefinedEnum = (AppEnvironment)(-1);
        var expectedValue = ((int)UndefinedEnum).ToString(CultureInfo.InvariantCulture);

        var actualValue = UndefinedEnum.GetIntegerValueAsString();

        Assert.Equal(
            expected: expectedValue,
            actual: actualValue);
    }

    [Fact]
    public void GetIntegerValueAsString_WithDefinedEnum_ReturnsIntegerValueAsString()
    {
        const AppEnvironment DefinedEnum = AppEnvironment.Local;
        var expectedValue = ((int)DefinedEnum).ToString(CultureInfo.InvariantCulture);

        var actualValue = DefinedEnum.GetIntegerValueAsString();

        Assert.Equal(
            expected: expectedValue,
            actual: actualValue);
    }
}
