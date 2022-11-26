// NewsPortalSetting.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Domain.Entities;
using Espresso.Domain.Infrastructure;

namespace Espresso.Domain.ValueObjects.SettingsValueObjects;

/// <summary>
/// <see cref="NewsPortal"/> setting.
/// </summary>
public class NewsPortalSetting : ValueObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NewsPortalSetting"/> class.
    /// </summary>
    /// <param name="maxAgeOfNewNewsPortalInMiliseconds">Max age of <see cref="NewsPortal"/> that can be considered new in miliseconds.</param>
    /// <param name="newNewsPortalsPosition">Position of new <see cref="NewsPortal"/> widget in applications.</param>
    public NewsPortalSetting(long maxAgeOfNewNewsPortalInMiliseconds, int newNewsPortalsPosition)
    {
        MaxAgeOfNewNewsPortalInMiliseconds = maxAgeOfNewNewsPortalInMiliseconds;
        NewNewsPortalsPosition = newNewsPortalsPosition;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NewsPortalSetting"/> class.
    /// ORM Constructor.
    /// </summary>
    private NewsPortalSetting()
    {
    }

#pragma warning disable RCS1170 // Use read-only auto-implemented property.
    /// <summary>
    /// Gets max age of <see cref="NewsPortal"/> that can be considered new in miliseconds.
    /// </summary>
    public long MaxAgeOfNewNewsPortalInMiliseconds { get; private set; }

    /// <summary>
    /// Gets position of new <see cref="NewsPortal"/> widget in applications.
    /// </summary>
    public int NewNewsPortalsPosition { get; private set; }
#pragma warning restore RCS1170 // Use read-only auto-implemented property.

    public TimeSpan MaxAgeOfNewNewsPortal => TimeSpan.FromMilliseconds(MaxAgeOfNewNewsPortalInMiliseconds);

    /// <inheritdoc/>
    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return MaxAgeOfNewNewsPortalInMiliseconds;
        yield return NewNewsPortalsPosition;
    }
}
