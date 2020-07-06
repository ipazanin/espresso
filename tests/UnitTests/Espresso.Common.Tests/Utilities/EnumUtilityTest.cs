using System.Collections.Generic;
using Espresso.Common.Enums;
using Espresso.Domain.Extensions;
using Espresso.Domain.Utilities;
using Xunit;

namespace Espresso.Common.Tests.Utilities
{
    public class EnumUtilityTest
    {

        #region TryParseEnum
        [Fact]
        public void TryParseEnum_WithIntegerValueNotDefinedBySelectedEnum_ReturnsFalse()
        {
            #region Arrange
            var notDefinedEnumIntegereValue = -1;
            #endregion

            #region  Act
            var isConversionSuccessfull = EnumUtility.TryParseEnum<AppEnvironment>(
                enumValue: notDefinedEnumIntegereValue,
                value: out _
            );
            #endregion

            #region Assert
            Assert.False(isConversionSuccessfull);
            #endregion
        }

        [Fact]
        public void TryParseEnum_WithIntegerValueDefinedBySelectedEnum_ReturnsTrue()
        {
            #region Arrange
            var definedIntegerEnumValue = (int)AppEnvironment.Local;
            #endregion

            #region  Act
            var isConversionSuccessfull = EnumUtility.TryParseEnum<AppEnvironment>(
                enumValue: definedIntegerEnumValue,
                value: out _
            );
            #endregion

            #region Assert
            Assert.True(isConversionSuccessfull);
            #endregion
        }

        [Fact]
        public void TryParseEnum_WithIntegerValueDefinedBySelectedEnum_OutputsRightEnumValue()
        {
            #region Arrange
            var definedIntegerEnumValue = (int)AppEnvironment.Local;
            #endregion

            #region  Act
            _ = EnumUtility.TryParseEnum<AppEnvironment>(
                enumValue: definedIntegerEnumValue,
                value: out var parsedEnum
            );
            #endregion

            #region Assert
            Assert.Equal(
                expected: AppEnvironment.Local,
                actual: parsedEnum
            );
            #endregion
        }

        [Fact]
        public void TryParseEnum_WithStringIntegerValueNotDefinedBySelectedEnum_ReturnsFalse()
        {
            #region Arrange
            var undefinedIntegerStringEnumValue = "-1";
            #endregion

            #region  Act
            var isConversionSuccessfull = EnumUtility.TryParseEnum<AppEnvironment>(
                enumValue: undefinedIntegerStringEnumValue,
                value: out _
            );
            #endregion

            #region Assert
            Assert.False(isConversionSuccessfull);
            #endregion
        }

        [Fact]
        public void TryParseEnum_WithStringIntegerValueDefinedBySelectedEnum_ReturnsTrue()
        {
            #region Arrange
            var integerStringEnumValue = ((int)AppEnvironment.Local).ToString();
            #endregion

            #region  Act
            var isConversionSuccessfull = EnumUtility.TryParseEnum<AppEnvironment>(
                enumValue: integerStringEnumValue,
                value: out _
            );
            #endregion

            #region Assert
            Assert.True(isConversionSuccessfull);
            #endregion
        }

        [Fact]
        public void TryParseEnum_WithStringIntegerValueDefinedBySelectedEnum_OutputsRightEnumValue()
        {
            #region Arrange
            var integerStringEnumValue = ((int)AppEnvironment.Local).ToString();
            #endregion

            #region  Act
            _ = EnumUtility.TryParseEnum<AppEnvironment>(
                enumValue: integerStringEnumValue,
                value: out var parsedEnum
            );
            #endregion

            #region Assert
            Assert.Equal(
                expected: AppEnvironment.Local,
                actual: parsedEnum
            );
            #endregion
        }

        [Fact]
        public void TryParseEnum_WithStringValueNotDefinedBySelectedEnum_ReturnsFalse()
        {
            #region Arrange
            var invalidEnumStringValue = "invalidValue";
            #endregion

            #region  Act
            var isConversionSuccessfull = EnumUtility.TryParseEnum<AppEnvironment>(
                enumValue: invalidEnumStringValue,
                value: out _
            );
            #endregion

            #region Assert
            Assert.False(isConversionSuccessfull);
            #endregion
        }

        [Fact]
        public void TryParseEnum_WithStringValueDefinedBySelectedEnum_ReturnsTrue()
        {
            #region Arrange
            var enumStringValue = AppEnvironment.Local.ToString();
            #endregion

            #region  Act
            var isConversionSuccessfull = EnumUtility.TryParseEnum<AppEnvironment>(
                enumValue: enumStringValue,
                value: out _
            );
            #endregion

            #region Assert
            Assert.True(isConversionSuccessfull);
            #endregion
        }

        [Fact]
        public void TryParseEnum_WithStringValueDefinedBySelectedEnum_OutputsRightEnumValue()
        {
            #region Arrange
            var enumStringValue = AppEnvironment.Local.ToString();
            #endregion

            #region  Act
            _ = EnumUtility.TryParseEnum<AppEnvironment>(
                enumValue: enumStringValue,
                value: out var parsedEnum
            );
            #endregion

            #region Assert
            Assert.Equal(
                expected: AppEnvironment.Local,
                actual: parsedEnum
            );
            #endregion
        }

        [Fact]
        public void TryParseEnum_WithDisplayStringValueDefinedBySelectedEnum_ReturnsTrue()
        {
            #region Arrange
            var enumDisplayNameString = AppEnvironment.Dev.GetDisplayName();
            #endregion

            #region  Act
            var isConversionSuccessfull = EnumUtility.TryParseEnum<AppEnvironment>(
                enumValue: enumDisplayNameString,
                value: out _
            );
            #endregion

            #region Assert
            Assert.True(isConversionSuccessfull);
            #endregion
        }

        [Fact]
        public void TryParseEnum_WithDisplayStringValueDefinedBySelectedEnum_OutputsRightEnumValue()
        {
            #region Arrange
            var enumDisplayNameString = AppEnvironment.Dev.GetDisplayName();
            #endregion

            #region  Act
            _ = EnumUtility.TryParseEnum<AppEnvironment>(
                enumValue: enumDisplayNameString,
                value: out var parsedEnum
            );
            #endregion

            #region Assert
            Assert.Equal(
                expected: AppEnvironment.Dev,
                actual: parsedEnum
            );
            #endregion
        }
        #endregion

        #region GetEnumOrDefault
        [Fact]
        public void GetEnumOrDefault_WithUndefinedIntegerEnumValue_ReturnsDefaultEnumValue()
        {
            #region Arrange
            var undefinedIntegerEnumValue = -1;
            var defaultEnumValue = AppEnvironment.Undefined;
            #endregion

            #region  Act
            var processedEnum = EnumUtility.GetEnumOrDefault(
                enumValue: undefinedIntegerEnumValue,
                defaultValue: defaultEnumValue
            );
            #endregion

            #region Assert
            Assert.Equal(
                expected: defaultEnumValue,
                actual: processedEnum
            );
            #endregion
        }

        [Fact]
        public void GetEnumOrDefault_WithDefinedIntegerEnumValue_ReturnsDefinedEnumValue()
        {
            #region Arrange
            var expectedEnumvalue = AppEnvironment.Dev;
            var definedIntegerEnumvalue = (int)expectedEnumvalue;
            var defaultEnumValue = AppEnvironment.Undefined;
            #endregion

            #region  Act
            var processedEnum = EnumUtility.GetEnumOrDefault(
                enumValue: definedIntegerEnumvalue,
                defaultValue: defaultEnumValue
            );
            #endregion

            #region Assert
            Assert.Equal(
                expected: expectedEnumvalue,
                actual: processedEnum
            );
            #endregion
        }

        [Fact]
        public void GetEnumOrDefault_WithUndefinedIntegerStringEnumValue_ReturnsDefaultEnumValue()
        {
            #region Arrange
            var undefinedIntegerStringEnumValue = "-1";
            var defaultEnumValue = AppEnvironment.Undefined;
            #endregion

            #region  Act
            var processedEnum = EnumUtility.GetEnumOrDefault(
                enumValue: undefinedIntegerStringEnumValue,
                defaultValue: defaultEnumValue
            );
            #endregion

            #region Assert
            Assert.Equal(
                expected: defaultEnumValue,
                actual: processedEnum
            );
            #endregion
        }

        [Fact]
        public void GetEnumOrDefault_WithDefinedIntegerStringEnumValue_ReturnsDefinedEnumValue()
        {
            #region Arrange
            var expectedEnumvalue = AppEnvironment.Dev;
            var definedIntegerStringEnumvalue = ((int)expectedEnumvalue).ToString();
            var defaultEnumValue = AppEnvironment.Undefined;
            #endregion

            #region  Act
            var processedEnum = EnumUtility.GetEnumOrDefault(
                enumValue: definedIntegerStringEnumvalue,
                defaultValue: defaultEnumValue
            );
            #endregion

            #region Assert
            Assert.Equal(
                expected: expectedEnumvalue,
                actual: processedEnum
            );
            #endregion
        }

        [Fact]
        public void GetEnumOrDefault_WithDefinedStringDisplayEnumValue_ReturnsDefinedEnumValue()
        {
            #region Arrange
            var expectedEnumvalue = AppEnvironment.Dev;
            var definedStringDisplayEnumValue = expectedEnumvalue.GetDisplayName();
            var defaultEnumValue = AppEnvironment.Undefined;
            #endregion

            #region  Act
            var processedEnum = EnumUtility.GetEnumOrDefault(
                enumValue: definedStringDisplayEnumValue,
                defaultValue: defaultEnumValue
            );
            #endregion

            #region Assert
            Assert.Equal(
                expected: expectedEnumvalue,
                actual: processedEnum
            );
            #endregion
        }
        #endregion

        #region GetAllValues
        [Fact]
        public void GetAllValues__ReturnsAllEnumValues()
        {
            #region Arrange
            var allExpectedAppEnvironmentEnumValues = new List<AppEnvironment>
            {
                AppEnvironment.Dev,
                AppEnvironment.Local,
                AppEnvironment.Prod,
                AppEnvironment.Undefined,
            };
            #endregion

            #region  Act
            var allAppEnvironmentEnumValues = EnumUtility.GetAllValues<AppEnvironment>();
            #endregion

            #region Assert
            Assert.All(
                collection: allAppEnvironmentEnumValues,
                appEnvironment => allExpectedAppEnvironmentEnumValues.Contains(appEnvironment)
            );
            #endregion
        }
        #endregion

        #region GetAllValuesExcept
        [Fact]
        public void GetAllValuesExcept_WithNullListOfEnums_ReturnsAllEnumValues()
        {
            #region Arrange
            var expectedAppEnvironmentEnumValues = new List<AppEnvironment>
            {
                AppEnvironment.Dev,
                AppEnvironment.Local,
                AppEnvironment.Prod,
                AppEnvironment.Undefined,
            };
            #endregion

            #region  Act
            var appEnvironmentEnumValues = EnumUtility.GetAllValuesExcept<AppEnvironment>(null);
            #endregion

            #region Assert
            Assert.All(
                collection: appEnvironmentEnumValues,
                appEnvironment => expectedAppEnvironmentEnumValues.Contains(appEnvironment)
            );
        }

        [Fact]
        public void GetAllValuesExcept_WithEmptyListOfEnums_ReturnsAllEnumValues()
        {
            #region Arrange
            var expectedAppEnvironmentEnumValues = new List<AppEnvironment>
            {
                AppEnvironment.Dev,
                AppEnvironment.Local,
                AppEnvironment.Prod,
                AppEnvironment.Undefined,
            };
            var emptyAppEnvironmentEnumList = new List<AppEnvironment>();
            #endregion

            #region  Act
            var appEnvironmentEnumValues = EnumUtility.GetAllValuesExcept(emptyAppEnvironmentEnumList);
            #endregion

            #region Assert
            Assert.All(
                collection: appEnvironmentEnumValues,
                appEnvironment => expectedAppEnvironmentEnumValues.Contains(appEnvironment)
            );
            #endregion
        }

        [Fact]
        public void GetAllValuesExcept_WithSomeListOfEnums_ReturnsAllEnumValuesExceptEnumsDefinedInListOfEnums()
        {
            #region Arrange
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
            #endregion

            #region  Act
            var appEnvironmentEnumValues = EnumUtility.GetAllValuesExcept(emptyAppEnvironmentEnumList);
            #endregion

            #region Assert
            Assert.All(
                collection: appEnvironmentEnumValues,
                appEnvironment => expectedAppEnvironmentEnumValues.Contains(appEnvironment)
            );
            #endregion
        }
        #endregion

        #endregion
    }
}
