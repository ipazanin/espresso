using System;
using Espresso.Common.Constants;
using Espresso.Domain.Utilities;
using Xunit;

namespace Espresso.Common.Tests.Utilities
{
    public class DateTimeUtilityTest
    {
        #region Properties

        #region CurrentMilliseconds
        [Fact]
        public void CurrentMilliseconds_ReturnsCurrentUnixUtxMilliseconds()
        {
            #region Arrange
            var millisecondsThreshold = 2;
            var expectedMilliseconds = (long)(DateTime.UtcNow - DateTimeConstants.UnixEpochStartTime).TotalMilliseconds;
            #endregion

            #region Act
            var actualMilliseconds = DateTimeUtility.CurrentMilliseconds;
            #endregion

            #region Assert
            Assert.InRange(
                actual: actualMilliseconds,
                low: expectedMilliseconds - millisecondsThreshold,
                high: expectedMilliseconds + millisecondsThreshold
            );
            #endregion
        }
        #endregion

        #region YesterdaysDate
        [Fact]
        public void YesterdaysDate_ReturnsYesterdaysDate()
        {
            #region Arrange
            var expectedDate = DateTime.UtcNow.AddDays(-1).Date;
            #endregion

            #region Act
            var actualDate = DateTimeUtility.YesterdaysDate;
            #endregion

            #region Assert
            Assert.Equal(
                expected: expectedDate,
                actual: actualDate
            );
            #endregion
        }
        #endregion    

        #endregion

        #region  Methods

        #region GetDateTime
        [Fact]
        public void GetDateTime_WithMaxLongValue_ThrowsArgumentOutOfRangeException()
        {
            #region Arrange
            var milliseconds = long.MaxValue;
            #endregion

            #region Act
            void TestAction()
            {
                var actualDateTime = DateTimeUtility.GetDateTime(milliseconds);
            }
            #endregion

            #region Assert
            Assert.Throws<ArgumentOutOfRangeException>(TestAction);
            #endregion
        }

        [Fact]
        public void GetDateTime_WithMinLongValue_ThrowsArgumentOutOfRangeException()
        {
            #region Arrange
            var milliseconds = long.MinValue;
            #endregion

            #region Act
            void TestAction()
            {
                var actualDateTime = DateTimeUtility.GetDateTime(milliseconds);
            }
            #endregion

            #region Assert
            Assert.Throws<ArgumentOutOfRangeException>(TestAction);
            #endregion
        }

        [Fact]
        public void GetDateTime_WithMaxLongValue_ThrowsArgumentOutOfRangeExceptionWithMessage()
        {
            #region Arrange
            var milliseconds = long.MaxValue;
            var expectedExceptionMessage = "Value to add was out of range. (Parameter 'value')";
            #endregion

            #region Act
            void TestAction()
            {
                var actualDateTime = DateTimeUtility.GetDateTime(milliseconds);
            }
            #endregion

            #region Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(TestAction);
            Assert.Equal(
                expected: expectedExceptionMessage,
                actual: exception.Message
            );
            #endregion
        }

        [Fact]
        public void GetDateTime_WithMinLongValue_ThrowsArgumentOutOfRangeExceptionWithMessage()
        {
            #region Arrange
            var milliseconds = long.MinValue;
            var expectedExceptionMessage = "Value to add was out of range. (Parameter 'value')";
            #endregion

            #region Act
            void TestAction()
            {
                var actualDateTime = DateTimeUtility.GetDateTime(milliseconds);
            }
            #endregion

            #region Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(TestAction);
            Assert.Equal(
                expected: expectedExceptionMessage,
                actual: exception.Message
            );
            #endregion
        }

        [Fact]
        public void GetDateTime_WithZeroLongValue_ReturnsUnixEpochStart()
        {
            #region Arrange
            var milliseconds = 0L;
            #endregion

            #region Act
            var actualDateTime = DateTimeUtility.GetDateTime(milliseconds);
            #endregion

            #region Assert
            Assert.Equal(
                expected: actualDateTime,
                actual: DateTimeConstants.UnixEpochStartTime
            );
            #endregion
        }
        #endregion

        #region GetMilliseconds
        [Fact]
        public void GetMilliseconds_WithUnixEpochStartTime_ReturnsZeroMilliseconds()
        {
            #region Arrange
            var expectedMilliseconds = 0L;
            #endregion

            #region Act
            var actualMilliseconds = DateTimeUtility.GetMilliseconds(DateTimeConstants.UnixEpochStartTime);
            #endregion

            #region Assert
            Assert.Equal(
                expected: actualMilliseconds,
                actual: expectedMilliseconds
            );
            #endregion
        }
        #endregion

        #region TruncateMilliseconds
        [Fact]
        public void TruncateMilliseconds_WithUnixEpochStartTime_ReturnsZeroMilliseconds()
        {
            #region Arrange
            var expectedMilliseconds = 0L;
            var millisecondsToTruncate = 1000L;
            #endregion

            #region Act
            var actualMilliseconds = DateTimeUtility.TruncateMillisecondsToDate(millisecondsToTruncate);
            #endregion

            #region Assert
            Assert.Equal(
                expected: actualMilliseconds,
                actual: expectedMilliseconds
            );
            #endregion
        }
        #endregion

        #endregion

    }
}
