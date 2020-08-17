using System;
using Espresso.Common.Constants;
using Espresso.Domain.Utilities;
using Xunit;

namespace Espresso.Common.Tests.Utilities
{
    public class DateTimeUtilityTest
    {

        #region  Methods

        #region GetDateTime
        [Fact]
        public void GetDateTime_WithMaxLongValue_ThrowsArgumentOutOfRangeException()
        {
            #region Arrange
            var miliseconds = long.MaxValue;
            #endregion

            #region Act
            void TestAction()
            {
                var actualDateTime = DateTimeUtility.GetDateTime(miliseconds);
            }
            #endregion

            #region Assert
            _ = Assert.Throws<ArgumentOutOfRangeException>(TestAction);
            #endregion
        }

        [Fact]
        public void GetDateTime_WithMinLongValue_ThrowsArgumentOutOfRangeException()
        {
            #region Arrange
            var miliseconds = long.MinValue;
            #endregion

            #region Act
            void TestAction()
            {
                var actualDateTime = DateTimeUtility.GetDateTime(miliseconds);
            }
            #endregion

            #region Assert
            _ = Assert.Throws<ArgumentOutOfRangeException>(TestAction);
            #endregion
        }

        [Fact]
        public void GetDateTime_WithMaxLongValue_ThrowsArgumentOutOfRangeExceptionWithMessage()
        {
            #region Arrange
            var miliseconds = long.MaxValue;
            var expectedExceptionMessge = "Value to add was out of range. (Parameter 'value')";
            #endregion

            #region Act
            void TestAction()
            {
                var actualDateTime = DateTimeUtility.GetDateTime(miliseconds);
            }
            #endregion

            #region Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(TestAction);
            Assert.Equal(
                expected: expectedExceptionMessge,
                actual: exception.Message
            );
            #endregion
        }

        [Fact]
        public void GetDateTime_WithMinLongValue_ThrowsArgumentOutOfRangeExceptionWithMessage()
        {
            #region Arrange
            var miliseconds = long.MinValue;
            var expectedExceptionMessge = "Value to add was out of range. (Parameter 'value')";
            #endregion

            #region Act
            void TestAction()
            {
                var actualDateTime = DateTimeUtility.GetDateTime(miliseconds);
            }
            #endregion

            #region Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(TestAction);
            Assert.Equal(
                expected: expectedExceptionMessge,
                actual: exception.Message
            );
            #endregion
        }

        [Fact]
        public void GetDateTime_WithZeroLongValue_ReturnsUnixEpochStart()
        {
            #region Arrange
            var miliseconds = 0L;
            #endregion

            #region Act
            var actualDateTime = DateTimeUtility.GetDateTime(miliseconds);
            #endregion

            #region Assert
            Assert.Equal(
                expected: actualDateTime,
                actual: DateTimeConstants.UnixEpochStartTime
            );
            #endregion
        }
        #endregion

        #region GetMiliseconds
        [Fact]
        public void GetMiliseconds_WithUnixEpochStartTime_ReturnsZeroMiliseconds()
        {
            #region Arrange
            var expectedMiliseconds = 0L;
            #endregion

            #region Act
            var actualMiliseconds = DateTimeUtility.GetMiliseconds(DateTimeConstants.UnixEpochStartTime);
            #endregion

            #region Assert
            Assert.Equal(
                expected: actualMiliseconds,
                actual: expectedMiliseconds
            );
            #endregion
        }
        #endregion

        #region TruncateMiliseconds
        [Fact]
        public void TruncateMiliseconds_WithUnixEpochStartTime_ReturnsZeroMiliseconds()
        {
            #region Arrange
            var expectedMiliseconds = 0L;
            var milisecondsToTruncate = 1000L;
            #endregion

            #region Act
            var actualMiliseconds = DateTimeUtility.TruncateMilisecondsToDate(milisecondsToTruncate);
            #endregion

            #region Assert
            Assert.Equal(
                expected: actualMiliseconds,
                actual: expectedMiliseconds
            );
            #endregion
        }
        #endregion

        #endregion

    }
}
