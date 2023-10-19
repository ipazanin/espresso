// IEntity.cs
//
// © 2022 Espresso News. All rights reserved.

namespace Espresso.Domain.Infrastructure;

public interface IEntity<out TKey>
    where TKey : notnull
{
    public TKey Id { get; }
}
