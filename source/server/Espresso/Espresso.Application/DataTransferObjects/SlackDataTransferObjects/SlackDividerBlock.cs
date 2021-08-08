// SlackDividerBlock.cs
//
// © 2021 Espresso News. All rights reserved.

namespace Espresso.Application.DataTransferObjects.SlackDataTransferObjects
{
    public record SlackDividerBlock : SlackBlock
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SlackDividerBlock"/> class.
        /// </summary>
        public SlackDividerBlock()
            : base("divider")
        {
        }
    }
}
