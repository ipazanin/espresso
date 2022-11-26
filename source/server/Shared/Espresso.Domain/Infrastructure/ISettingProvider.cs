// ISettingProvider.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Domain.Entities;

namespace Espresso.Domain.Infrastructure;

/// <summary>
/// <see cref="Setting"/> provider.
/// </summary>
public interface ISettingProvider
{
    /// <summary>
    /// Gets latest <see cref="Setting"/>.
    /// </summary>
    public Setting LatestSetting { get; }

    /// <summary>
    /// Updates latest <see cref="Setting"/>.
    /// </summary>
    /// <param name="cancellationToken"></param>
    public Task UpdateLatestSetting(CancellationToken cancellationToken);
}
