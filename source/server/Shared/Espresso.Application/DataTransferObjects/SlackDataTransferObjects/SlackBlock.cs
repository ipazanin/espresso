// SlackBlock.cs
//
// © 2021 Espresso News. All rights reserved.

namespace Espresso.Application.DataTransferObjects.SlackDataTransferObjects;

/// <summary>
/// Abstract record representing block
/// </summary>
public abstract record SlackBlock
{
    /// <summary>
    /// Gets slack block type.
    /// </summary>
    public string Type { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SlackBlock"/> class.
    /// </summary>
    /// <param name="type">Slack block type.</param>
    protected SlackBlock(string type)
    {
        Type = type;
    }
}
