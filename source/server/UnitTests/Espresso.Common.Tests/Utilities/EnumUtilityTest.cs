// EnumUtilityTest.cs
//
// © 2021 Espresso News. All rights reserved.

using System.Collections.Generic;
using Espresso.Common.Enums;
using Espresso.Common.Extensions;
using Espresso.Domain.Utilities;
using Xunit;

namespace Espresso.Common.Tests.Utilities
{
    public class EnumUtilityTest
    {
        [Fact]
        public void TryParseEnum_WithIntegerValueNotDefinedBySelectedEnum_ReturnsFalse()
        {
            const int notDefinedEnumIntegerValue = -1;

            var isConversionSuccessfull = EnumUtility.TryParseEnum<AppEnvironment>(
                enumValue: notDefinedEnumIntegerValue,
                value: out _
            );

            Assert.False(isConversionSuccessfull);
        }

        [Fact]
        public void TryParseEnum_WithIntegerValueDefinedBySelectedEnum_ReturnsTrue()
        {
            const int definedIntegerEnumValue = (int)AppEnvironment.Local;

            var isConversionSuccessfull = EnumUtility.TryParseEnum<AppEnvironment>(
                enumValue: definedIntegerEnumValue,
                value: out _
            );

            Assert.True(isConversionSuccessfull);
        }

        [Fact]
        public void TryParseEnum_WithIntegerValueDefinedBySelectedEnum_OutputsRightEnumValue()
        {
            const int definedIntegerEnumValue = (int)AppEnvironment.Local;

            _ = EnumUtility.TryParseEnum<AppEnvironment>(
                enumValue: definedIntegerEnumValue,
                value: out var parsedEnum
            );

            Assert.Equal(
                expected: AppEnvironment.Local,
                actual: parsedEnum
            );
        }

        [Fact]
        public void TryParseEnum_WithStringIntegerValueNotDefinedBySelectedEnum_ReturnsFalse()
        {
            const string? undefinedIntegerStringEnumValue = "-1";

            var isConversionSuccessfull = EnumUtility.TryParseEnum<AppEnvironment>(
                enumValue: undefinedIntegerStringEnumValue,
                value: out _
            );

            Assert.False(isConversionSuccessfull);
        }

        [Fact]
        public void TryParseEnum_WithStringIntegerValueDefinedBySelectedEnum_ReturnsTrue()
        {
            var integerStringEnumValue = ((int)AppEnvironment.Local).ToString();

            var isConversionSuccessfull = EnumUtility.TryParseEnum<AppEnvironment>(
                enumValue: integerStringEnumValue,
                value: out _
            );

            Assert.True(isConversionSuccessfull);
        }

        [Fact]
        public void TryParseEnum_WithStringIntegerValueDefinedBySelectedEnum_OutputsRightEnumValue()
        {
            var integerStringEnumValue = ((int)AppEnvironment.Local).ToString();

            _ = EnumUtility.TryParseEnum<AppEnvironment>(
                enumValue: integerStringEnumValue,
                value: out var parsedEnum
            );

            Assert.Equal(
                expected: AppEnvironment.Local,
                actual: parsedEnum
            );
        }

        [Fact]
        public void TryParseEnum_WithStringValueNotDefinedBySelectedEnum_ReturnsFalse()
        {
            const string? invalidEnumStringValue = "invalidValue";

            var isConversionSuccessfull = EnumUtility.TryParseEnum<AppEnvironment>(
                enumValue: invalidEnumStringValue,
                value: out _
            );

            Assert.False(isConversionSuccessfull);
        }

        [Fact]
        public void TryParseEnum_WithStringValueDefinedBySelectedEnum_ReturnsTrue()
        {
            var enumStringValue = AppEnvironment.Local.ToString();

            var isConversionSuccessfull = EnumUtility.TryParseEnum<AppEnvironment>(
                enumValue: enumStringValue,
                value: out _
            );

            Assert.True(isConversionSuccessfull);
        }

        [Fact]
        public void TryParseEnum_WithStringValueDefinedBySelectedEnum_OutputsRightEnumValue()
        {
            var enumStringValue = AppEnvironment.Local.ToString();

            _ = EnumUtility.TryParseEnum<AppEnvironment>(
                enumValue: enumStringValue,
                value: out var parsedEnum
            );

            Assert.Equal(
                expected: AppEnvironment.Local,
                actual: parsedEnum
            );
        }

        [Fact]
        public void TryParseEnum_WithDisplayStringValueDefinedBySelectedEnum_ReturnsTrue()
        {
            var enumDisplayNameString = AppEnvironment.Dev.GetDisplayName();

            var isConversionSuccessfull = EnumUtility.TryParseEnum<AppEnvironment>(
                enumValue: enumDisplayNameString,
                value: out _
            );

            Assert.True(isConversionSuccessfull);
        }

        [Fact]
        public void TryParseEnum_WithDisplayStringValueDefinedBySelectedEnum_OutputsRightEnumValue()
        {
            var enumDisplayNameString = AppEnvironment.Dev.GetDisplayName();

            _ = EnumUtility.TryParseEnum<AppEnvironment>(
                enumValue: enumDisplayNameString,
                value: out var parsedEnum
            );

            Assert.Equal(
                expected: AppEnvironment.Dev,
                actual: parsedEnum
            );
        }

        [Fact]
        public void GetEnumOrDefault_WithUndefinedIntegerEnumValue_ReturnsDefaultEnumValue()
        {
            const int undefinedIntegerEnumValue = -1;
            const AppEnvironment defaultEnumValue = AppEnvironment.Undefined;

            var processedEnum = EnumUtility.GetEnumOrDefault(
                enumValue: undefinedIntegerEnumValue,
                defaultValue: defaultEnumValue
            );

            Assert.Equal(
                expected: defaultEnumValue,
                actual: processedEnum
            );
        }

        [Fact]
        public void GetEnumOrDefault_WithDefinedIntegerEnumValue_ReturnsDefinedEnumValue()
        {
            const AppEnvironment expectedEnumvalue = AppEnvironment.Dev;
            var definedIntegerEnumvalue = (int)expectedEnumvalue;
            const AppEnvironment defaultEnumValue = AppEnvironment.Undefined;

            var processedEnum = EnumUtility.GetEnumOrDefault(
                enumValue: definedIntegerEnumvalue,
                defaultValue: defaultEnumValue
            );

            Assert.Equal(
                expected: expectedEnumvalue,
                actual: processedEnum
            );
        }

        [Fact]
        public void GetEnumOrDefault_WithUndefinedIntegerStringEnumValue_ReturnsDefaultEnumValue()
        {
            const string? undefinedIntegerStringEnumValue = "-1";
            const AppEnvironment defaultEnumValue = AppEnvironment.Undefined;

            var processedEnum = EnumUtility.GetEnumOrDefault(
                enumValue: undefinedIntegerStringEnumValue,
                defaultValue: defaultEnumValue
            );

            Assert.Equal(
                expected: defaultEnumValue,
                actual: processedEnum
            );
        }

        [Fact]
        public void GetEnumOrDefault_WithDefinedIntegerStringEnumValue_ReturnsDefinedEnumValue()
        {
            const AppEnvironment expectedEnumvalue = AppEnvironment.Dev;
            var definedIntegerStringEnumvalue = ((int)expectedEnumvalue).ToString();
            const AppEnvironment defaultEnumValue = AppEnvironment.Undefined;

            var processedEnum = EnumUtility.GetEnumOrDefault(
                enumValue: definedIntegerStringEnumvalue,
                defaultValue: defaultEnumValue
            );

            Assert.Equal(
                expected: expectedEnumvalue,
                actual: processedEnum
            );
        }

        [Fact]
        public void GetEnumOrDefault_WithDefinedStringDisplayEnumValue_ReturnsDefinedEnumValue()
        {
            const AppEnvironment expectedEnumvalue = AppEnvironment.Dev;
            var definedStringDisplayEnumValue = expectedEnumvalue.GetDisplayName();
            const AppEnvironment defaultEnumValue = AppEnvironment.Undefined;

            var processedEnum = EnumUtility.GetEnumOrDefault(
                enumValue: definedStringDisplayEnumValue,
                defaultValue: defaultEnumValue
            );

            Assert.Equal(
                expected: expectedEnumvalue,
                actual: processedEnum
            );
        }

        [Fact]
        public void GetEnumOrDefault_ReturnsProvidedEnumValue_WhenProvidedEnumValueIsDefined()
        {
            const AppEnvironment expectedEnumvalue = AppEnvironment.Dev;

            var actualEnum = EnumUtility.GetEnumOrDefault(
                enumValue: AppEnvironment.Dev,
                defaultValue: AppEnvironment.Local
            );

            Assert.Equal(
                expected: expectedEnumvalue,
                actual: actualEnum
            );
        }

        [Fact]
        public void GetEnumOrDefault_ReturnsDefaultEnumValue_WhenProvidedEnumValueIsUndefined()
        {
            const AppEnvironment expectedEnumvalue = AppEnvironment.Local;

            var actualEnum = EnumUtility.GetEnumOrDefault(
                enumValue: (AppEnvironment)(-1),
                defaultValue: AppEnvironment.Local
            );

            Assert.Equal(
                expected: expectedEnumvalue,
                actual: actualEnum
            );
        }

        [Fact]
        public void GetAllValues__ReturnsAllEnumValues()
        {
            var allExpectedAppEnvironmentEnumValues = new List<AppEnvironment>
            {
                AppEnvironment.Dev,
                AppEnvironment.Local,
                AppEnvironment.Prod,
                AppEnvironment.Undefined,
            };

            var allAppEnvironmentEnumValues = EnumUtility.GetAllValues<AppEnvironment>();

            Assert.All(
                collection: allAppEnvironmentEnumValues,
                appEnvironment => allExpectedAppEnvironmentEnumValues.Contains(appEnvironment)
            );
        }

        [Fact]
        public void GetAllValuesExcept_WithNullListOfEnums_ReturnsAllEnumValues()
        {
            var expectedAppEnvironmentEnumValues = new List<AppEnvironment>
            {
                AppEnvironment.Dev,
                AppEnvironment.Local,
                AppEnvironment.Prod,
                AppEnvironment.Undefined,
            };

            var appEnvironmentEnumValues = EnumUtility.GetAllValuesExcept<AppEnvironment>(null);

            Assert.All(
                collection: appEnvironmentEnumValues,
                appEnvironment => expectedAppEnvironmentEnumValues.Contains(appEnvironment)
            );
        }

        [Fact]
        public void GetAllValuesExcept_WithEmptyListOfEnums_ReturnsAllEnumValues()
        {
            var expectedAppEnvironmentEnumValues = new List<AppEnvironment>
            {
                AppEnvironment.Dev,
                AppEnvironment.Local,
                AppEnvironment.Prod,
                AppEnvironment.Undefined,
            };
            var emptyAppEnvironmentEnumList = new List<AppEnvironment>();

            var appEnvironmentEnumValues = EnumUtility.GetAllValuesExcept(emptyAppEnvironmentEnumList);

            Assert.All(
                collection: appEnvironmentEnumValues,
                appEnvironment => expectedAppEnvironmentEnumValues.Contains(appEnvironment)
            );
        }

        [Fact]
        public void GetAllValuesExcept_WithSomeListOfEnums_ReturnsAllEnumValuesExceptEnumsDefinedInListOfEnums()
        {
            var expectedAppEnvironmentEnumValues = new List<AppEnvironment>
            {
                AppEnvironment.Dev,
                AppEnvironment.Prod,
                AppEnvironment.Undefined,
            };
            var emptyAppEnvironmentEnumList = new List<AppEnvironment>
            {
                AppEnvironment.Local,
            };

            var appEnvironmentEnumValues = EnumUtility.GetAllValuesExcept(emptyAppEnvironmentEnumList);

            Assert.All(
                collection: appEnvironmentEnumValues,
                appEnvironment => expectedAppEnvironmentEnumValues.Contains(appEnvironment)
            );
        }
    }
}
