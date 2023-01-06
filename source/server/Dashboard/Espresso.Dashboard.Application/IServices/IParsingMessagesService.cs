// IParsingMessagesService.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects;

namespace Espresso.Dashboard.Application.IServices;

public interface IParsingMessagesService
{
    public void PushMessage(ParsingErrorMessageDto parsingErrorMessage);

    public IEnumerable<ParsingErrorMessageDto> GetMessages(int rssFeedId);

    public IEnumerable<ParsingErrorMessageDto> GetMessages();
}
