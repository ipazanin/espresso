using Espresso.Common.Enums;
using Espresso.Domain.Utilities;
using Xunit;

namespace Espresso.Common.Tests.Utilities
{
    public class EnumUtilityTest
    {
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
            var notDefinedEnumIntegereValue = (int)AppEnvironment.Local;
            #endregion

            #region  Act
            var isConversionSuccessfull = EnumUtility.TryParseEnum<AppEnvironment>(
                enumValue: notDefinedEnumIntegereValue,
                value: out _
            );
            #endregion

            #region Assert
            Assert.True(isConversionSuccessfull);
            #endregion
        }

    }
}
