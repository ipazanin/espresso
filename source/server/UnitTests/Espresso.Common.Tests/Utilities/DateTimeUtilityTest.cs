// DateTimeUtilityTest.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using Espresso.Domain.Utilities;
using Xunit;

namespace Espresso.Common.Tests.Utilities
{
    public class DateTimeUtilityTest
    {
        [Fact]
        public void CurrentMilliseconds_ReturnsCurrentUnixUtxMilliseconds()
        {
            const int MillisecondsThreshold = 2;
            var expectedMilliseconds = (long)(DateTime.UtcNow - DateTimeConstants.UnixEpochStartTime).TotalMilliseconds;

            var actualMilliseconds = DateTimeUtility.CurrentMilliseconds;

            Assert.InRange(
                actual: actualMilliseconds,
                low: expectedMilliseconds - MillisecondsThreshold,
                high: expectedMilliseconds + MillisecondsThreshold);
        }

        [Fact]
        public void YesterdaysDate_ReturnsYesterdaysDate()
        {
            var expectedDate = DateTime.UtcNow.AddDays(-1).Date;

            var actualDate = DateTimeUtility.YesterdaysDate;

            Assert.Equal(
                expected: expectedDate,
                actual: actualDate);
        }

        [Fact]
        public void GetDateTime_WithMaxLongValue_ThrowsArgumentOutOfRangeException()
        {
            const long Milliseconds = long.MaxValue;

            static void TestAction()
            {
                _ = DateTimeUtility.GetDateTime(Milliseconds);
            }

            Assert.Throws<ArgumentOutOfRangeException>(TestAction);
        }

        [Fact]
        public void GetDateTime_WithMinLongValue_ThrowsArgumentOutOfRangeException()
        {
            const long Milliseconds = long.MinValue;

            static void TestAction()
            {
                _ = DateTimeUtility.GetDateTime(Milliseconds);
            }

            Assert.Throws<ArgumentOutOfRangeException>(TestAction);
        }

        [Fact]
        public void GetDateTime_WithMaxLongValue_ThrowsArgumentOutOfRangeExceptionWithMessage()
        {
            const long Milliseconds = long.MaxValue;
            const string? ExpectedExceptionMessage = "Value to add was out of range. (Parameter 'value')";

            static void TestAction()
            {
                var actualDateTime = DateTimeUtility.GetDateTime(Milliseconds);
            }

            var exception = Assert.Throws<ArgumentOutOfRangeException>(TestAction);
            Assert.Equal(
                expected: ExpectedExceptionMessage,
                actual: exception.Message);
        }

        [Fact]
        public void GetDateTime_WithMinLongValue_ThrowsArgumentOutOfRangeExceptionWithMessage()
        {
            const long Milliseconds = long.MinValue;
            const string? ExpectedExceptionMessage = "Value to add was out of range. (Parameter 'value')";

            static void TestAction()
            {
                var actualDateTime = DateTimeUtility.GetDateTime(Milliseconds);
            }

            var exception = Assert.Throws<ArgumentOutOfRangeException>(TestAction);
            Assert.Equal(
                expected: ExpectedExceptionMessage,
                actual: exception.Message);
        }

        [Fact]
        public void GetDateTime_WithZeroLongValue_ReturnsUnixEpochStart()
        {
            const long Milliseconds = 0L;

            var actualDateTime = DateTimeUtility.GetDateTime(Milliseconds);

            Assert.Equal(
                expected: actualDateTime,
                actual: DateTimeConstants.UnixEpochStartTime);
        }

        [Fact]
        public void GetMilliseconds_WithUnixEpochStartTime_ReturnsZeroMilliseconds()
        {
            const long ExpectedMilliseconds = 0L;

            var actualMilliseconds = DateTimeUtility.GetMilliseconds(DateTimeConstants.UnixEpochStartTime);

            Assert.Equal(
                expected: actualMilliseconds,
                actual: ExpectedMilliseconds);
        }

        [Fact]
        public void TruncateMilliseconds_WithUnixEpochStartTime_ReturnsZeroMilliseconds()
        {
            const long ExpectedMilliseconds = 0L;
            const long MillisecondsToTruncate = 1000L;

            var actualMilliseconds = DateTimeUtility.TruncateMillisecondsToDate(MillisecondsToTruncate);

            Assert.Equal(
                expected: actualMilliseconds,
                actual: ExpectedMilliseconds);
        }
    }
}
