﻿// DateTimeUtilityTest.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Common.Utilities;
using Xunit;

namespace Espresso.Common.Tests.Utilities;

public class DateTimeUtilityTest
{

    [Fact]
    public void YesterdaysDate_ReturnsYesterdaysDate()
    {
        var expectedDate = DateTimeOffset.UtcNow.AddDays(-1).Date;

        var actualDate = DateTimeUtility.YesterdaysDate;

        Assert.Equal(
            expected: expectedDate,
            actual: actualDate);
    }

    [Fact]
    public void TruncateMilliseconds_WithUnixEpochStartTime_ReturnsZeroMilliseconds()
    {
        const long ExpectedMilliseconds = 0L;
        const long MillisecondsToTruncate = 1000L;

        var actualMilliseconds = DateTimeUtility.TruncateMillisecondsToDate(MillisecondsToTruncate);

        Assert.Equal(
            expected: ExpectedMilliseconds,
            actual: actualMilliseconds);
    }
}
