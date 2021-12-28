// ExampleSubClassWithPublicSetters.cs
//
// © 2021 Espresso News. All rights reserved.

namespace Espresso.Common.Tests.TestUtilities.Models;

/// <summary>
/// ExampleClass Model
/// </summary>
public record ExampleSubClassWithPublicSetters
{
    public int FourthPropertyFirstProperty { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExampleSubClassWithPublicSetters"/> class.
    /// ExampleClass Constructor.
    /// </summary>
    public ExampleSubClassWithPublicSetters()
    {
    }
}
