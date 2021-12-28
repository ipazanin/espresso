// ExampleClassWithPublicSetters.cs
//
// © 2021 Espresso News. All rights reserved.

namespace Espresso.Common.Tests.TestUtilities.Models;

/// <summary>
/// ExampleClass Model
/// </summary>
public record ExampleClassWithPublicSetters
{
    public string? FirstProperty { get; set; }
    public int SecondProperty { get; set; }
    public bool ThirdProperty { get; set; }
    public ExampleSubClassWithPublicSetters? FourthProperty { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExampleClassWithPublicSetters"/> class.
    /// ExampleClass Constructor.
    /// </summary>
    public ExampleClassWithPublicSetters()
    {
    }
}
