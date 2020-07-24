using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.Domain.Extensions;
using Xunit;

namespace Espresso.Common.Tests.Extensions
{
    public class EnumExtensionsTest
    {

        #region  Methods

        #region GetDisplayName
        [Fact]
        public void GetDisplayName_WithUndefinedEnum_ReturnsUndefinedString()
        {
            #region Arrange
            var undefinedEnum = (AppEnvironment)(-1);
            var expectedDisplayName = FormatConstants.Undefined;
            #endregion

            #region Act
            var actualDisplayName = undefinedEnum.GetDisplayName();
            #endregion

            #region Assert
            Assert.Equal(
                expected: expectedDisplayName,
                actual: actualDisplayName
            );
            #endregion
        }

        [Fact]
        public void GetDisplayName_WithEnumValueWithoutDisplayNameAttribute_ReturnsEnumStringValue()
        {
            #region Arrange
            var enumValueWithoutDisplayAttribute = AppEnvironment.Local;
            var expectedDisplayName = enumValueWithoutDisplayAttribute.ToString();
            #endregion

            #region Act
            var actualDisplayName = enumValueWithoutDisplayAttribute.GetDisplayName();
            #endregion

            #region Assert
            Assert.Equal(
                expected: expectedDisplayName,
                actual: actualDisplayName
            );
            #endregion
        }

        [Fact]
        public void GetDisplayName_WithEnumValueWithDisplayNameAttribute_ReturnsDisplayNameValue()
        {
            #region Arrange
            var enumValueWithDisplayAttribute = AppEnvironment.Prod;
            var expectedDisplayName = "Production";
            #endregion

            #region Act
            var actualDisplayName = enumValueWithDisplayAttribute.GetDisplayName();
            #endregion

            #region Assert
            Assert.Equal(
                expected: expectedDisplayName,
                actual: actualDisplayName
            );
            #endregion
        }
        #endregion

        #endregion

    }
}
