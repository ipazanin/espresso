// ValueObject.cs
//
// © 2022 Espresso News. All rights reserved.

namespace Espresso.Domain.Infrastructure;

/// <summary>
/// Source: https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/microservice-ddd-cqrs-patterns/implement-value-objects.
/// </summary>
public abstract class ValueObject
{
    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
        {
            return false;
        }

        var other = (ValueObject)obj;
        var thisValues = GetAtomicValues().GetEnumerator();
        var otherValues = other.GetAtomicValues().GetEnumerator();

        while (thisValues.MoveNext() && otherValues.MoveNext())
        {
            if (thisValues.Current is null ^ otherValues.Current is null)
            {
                return false;
            }

            if (thisValues.Current?.Equals(otherValues.Current) == false)
            {
                return false;
            }
        }

        return !thisValues.MoveNext() && !otherValues.MoveNext();
    }

    public override int GetHashCode()
    {
        return GetAtomicValues()
            .Select(x => x?.GetHashCode() ?? 0)
            .Aggregate((x, y) => x ^ y);
    }

    protected static bool EqualOperator(ValueObject left, ValueObject right)
    {
        // Bitwise XOR
        return !(left is null ^ right is null) &&
            right != null &&
            left?.Equals(right) != false;
    }

    protected static bool NotEqualOperator(ValueObject left, ValueObject right)
    {
        return !EqualOperator(left, right);
    }

    /// <summary>
    /// Returns value objects properties.
    /// </summary>
    /// <returns>Value objects properties.</returns>
    protected abstract IEnumerable<object?> GetAtomicValues();
}
