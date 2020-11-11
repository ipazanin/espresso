using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Espresso.Application.DataTransferObjects.SlackDataTransferObjects
{
    public abstract record SlackSectionBlock : SlackBlock
    {
        public SlackSectionBlock() : base("section")
        {
        }
    }
}
