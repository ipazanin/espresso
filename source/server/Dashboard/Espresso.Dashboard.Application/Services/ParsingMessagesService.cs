// ParsingMessagesService.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects;
using Espresso.Common.Collections;
using Espresso.Dashboard.Application.IServices;

namespace Espresso.Dashboard.Application.Services;

public class ParsingMessagesService : IParsingMessagesService
{
    private const int MessagesCapacity = 100;

    private readonly IDictionary<int, CircularBuffer<ParsingErrorMessageDto>> _parseMessages = new Dictionary<int, CircularBuffer<ParsingErrorMessageDto>>();

    private readonly object _messagesLock = new();

    public void PushMessage(ParsingErrorMessageDto parsingErrorMessage)
    {
        lock (_messagesLock)
        {
            if (!_parseMessages.TryGetValue(parsingErrorMessage.RssFeedId, out var existingMessages))
            {
                var newWarnings = new CircularBuffer<ParsingErrorMessageDto>(MessagesCapacity);
                newWarnings.PushFront(parsingErrorMessage);
                _ = _parseMessages.TryAdd(parsingErrorMessage.RssFeedId, newWarnings);
            }
            else
            {
                var existingMessage = existingMessages.FirstOrDefault(message => message.Equals(parsingErrorMessage));

                if (existingMessage is null)
                {
                    existingMessages.PushFront(parsingErrorMessage);
                    return;
                }

                existingMessage.Created = parsingErrorMessage.Created;
            }
        }
    }

    public IEnumerable<ParsingErrorMessageDto> GetMessages(int rssFeedId)
    {
        lock (_messagesLock)
        {
            if (!_parseMessages.TryGetValue(rssFeedId, out var warnings))
            {
                return Enumerable.Empty<ParsingErrorMessageDto>();
            }

            return warnings;
        }
    }

    public IEnumerable<ParsingErrorMessageDto> GetMessages()
    {
        lock (_messagesLock)
        {
            return _parseMessages
                .Values
                .SelectMany(buffer => buffer);
        }
    }

    public void ClearMessages()
    {
        lock (_messagesLock)
        {
            _parseMessages.Clear();
        }
    }
}
