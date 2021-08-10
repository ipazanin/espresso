// SlackSectionBlock.cs
//
// © 2021 Espresso News. All rights reserved.

namespace Espresso.Application.DataTransferObjects.SlackDataTransferObjects
{
    public abstract record SlackSectionBlock : SlackBlock
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SlackSectionBlock"/> class.
        /// </summary>
        protected SlackSectionBlock()
            : base("section")
        {
        }
    }
}
