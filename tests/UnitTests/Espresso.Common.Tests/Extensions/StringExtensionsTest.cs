using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.Common.Extensions;
using Espresso.Domain.Extensions;
using Xunit;

namespace Espresso.Common.Tests.Extensions
{
    public class StringExtensionsTest
    {

        #region  Methods

        #region RemoveExtraWhiteSpaceCharacters
        [Fact]
        public void RemoveExtraWhiteSpaceCharacters_WithStringWithNoEmptyCharacters_ReturnsUnmodifiedString()
        {
            #region Arrange
            var stringValue = "someValue";
            var expectedStringValue = stringValue;
            #endregion

            #region Act
            var actualStringValue = stringValue.RemoveExtraWhiteSpaceCharacters();
            #endregion

            #region Assert
            Assert.Equal(
                expected: expectedStringValue,
                actual: actualStringValue
            );
            #endregion
        }

        [Theory]
        [InlineData('\u0020')]
        [InlineData('\u00A0')]
        [InlineData('\u1680')]
        [InlineData('\u2000')]
        [InlineData('\u2001')]
        [InlineData('\u2002')]
        [InlineData('\u2003')]
        [InlineData('\u2004')]
        [InlineData('\u2005')]
        [InlineData('\u2006')]
        [InlineData('\u2007')]
        [InlineData('\u2008')]
        [InlineData('\u2009')]
        [InlineData('\u200A')]
        [InlineData('\u202F')]
        [InlineData('\u205F')]
        [InlineData('\u3000')]
        [InlineData('\u2028')]
        [InlineData('\u2029')]
        [InlineData('\u0009')]
        [InlineData('\u000A')]
        [InlineData('\u000B')]
        [InlineData('\u000C')]
        [InlineData('\u000D')]
        [InlineData('\u0085')]
        public void RemoveExtraWhiteSpaceCharacters_WithStringWithEmptyCharacters_ReturnsStringWithoutExtraWhiteSpaceCharacters(char whiteSpaceCharacter)
        {
            #region Arrange
            var stringValue = $"{whiteSpaceCharacter}{whiteSpaceCharacter}{whiteSpaceCharacter}some" +
                $"{whiteSpaceCharacter}{whiteSpaceCharacter}{whiteSpaceCharacter}Value{whiteSpaceCharacter}{whiteSpaceCharacter}{whiteSpaceCharacter}";
            var expectedStringValue = $"{whiteSpaceCharacter}some{whiteSpaceCharacter}Value{whiteSpaceCharacter}";
            #endregion

            #region Act
            var actualStringValue = stringValue.RemoveExtraWhiteSpaceCharacters();
            #endregion

            #region Assert
            Assert.Equal(
                expected: expectedStringValue,
                actual: actualStringValue
            );
            #endregion
        }
        #endregion

        #endregion

    }
}
