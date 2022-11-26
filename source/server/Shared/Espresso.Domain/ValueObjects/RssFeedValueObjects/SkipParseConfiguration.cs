// SkipParseConfiguration.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Domain.Infrastructure;

namespace Espresso.Domain.ValueObjects.RssFeedValueObjects;

public class SkipParseConfiguration : ValueObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SkipParseConfiguration"/> class.
    /// </summary>
    /// <param name="numberOfSkips"></param>
    /// <param name="currentSkip"></param>
    public SkipParseConfiguration(int? numberOfSkips, int? currentSkip)
    {
        NumberOfSkips = numberOfSkips;
        CurrentSkip = currentSkip;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SkipParseConfiguration"/> class.
    /// ORM COnstructor.
    /// </summary>
    private SkipParseConfiguration()
    {
    }

    public int? NumberOfSkips { get; private set; }

    public int? CurrentSkip { get; private set; }

    public bool ShouldParse()
    {
        if (NumberOfSkips is null && CurrentSkip is null)
        {
            return true;
        }

        CurrentSkip = CurrentSkip == NumberOfSkips ? 0 : CurrentSkip + 1;

        return CurrentSkip == 1;
    }

    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return NumberOfSkips;
        yield return CurrentSkip;
    }
}
