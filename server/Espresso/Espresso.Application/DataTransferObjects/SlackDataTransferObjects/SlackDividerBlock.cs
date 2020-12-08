using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Espresso.Application.DataTransferObjects.SlackDataTransferObjects
{
    public record SlackDividerBlock : SlackBlock
    {
        public SlackDividerBlock() : base("divider")
        {
        }
    }
}
