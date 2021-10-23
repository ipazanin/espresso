// EnumExtensionsTest.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.Common.Extensions;
using Xunit;

namespace Espresso.Common.Tests.Extensions
{
    public class EnumExtensionsTest
    {
        [Fact]
        public void GetDisplayName_WithUndefinedEnum_ReturnsUndefinedString()
        {
            const AppEnvironment undefinedEnum = (AppEnvironment)(-1);
            const string? expectedDisplayName = FormatConstants.Undefined;

            var actualDisplayName = undefinedEnum.GetDisplayName();

            Assert.Equal(
                expected: expectedDisplayName,
                actual: actualDisplayName);
        }

        [Fact]
        public void GetDisplayName_WithEnumValueWithoutDisplayNameAttribute_ReturnsEnumStringValue()
        {
            const AppEnvironment enumValueWithoutDisplayAttribute = AppEnvironment.Local;
            var expectedDisplayName = enumValueWithoutDisplayAttribute.ToString();

            var actualDisplayName = enumValueWithoutDisplayAttribute.GetDisplayName();

            Assert.Equal(
                expected: expectedDisplayName,
                actual: actualDisplayName);
        }

        [Fact]
        public void GetDisplayName_WithEnumValueWithDisplayNameAttribute_ReturnsDisplayNameValue()
        {
            const AppEnvironment enumValueWithDisplayAttribute = AppEnvironment.Prod;
            const string? expectedDisplayName = "Production";

            var actualDisplayName = enumValueWithDisplayAttribute.GetDisplayName();

            Assert.Equal(
                expected: expectedDisplayName,
                actual: actualDisplayName);
        }

        [Fact]
        public void GetIntegerValueAsString_WithUndefinedEnum_ReturnsIntegerValueAsString()
        {
            const AppEnvironment undefinedEnum = (AppEnvironment)(-1);
            var expectedValue = ((int)undefinedEnum).ToString();

            var actualValue = undefinedEnum.GetIntegerValueAsString();

            Assert.Equal(
                expected: expectedValue,
                actual: actualValue);
        }

        [Fact]
        public void GetIntegerValueAsString_WithDefinedEnum_ReturnsIntegerValueAsString()
        {
            const AppEnvironment definedEnum = AppEnvironment.Local;
            var expectedValue = ((int)definedEnum).ToString();

            var actualValue = definedEnum.GetIntegerValueAsString();

            Assert.Equal(
                expected: expectedValue,
                actual: actualValue);
        }
    }
}
