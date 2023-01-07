// ParsingErrorMessageDto.cs
//
// © 2022 Espresso News. All rights reserved.

using Microsoft.Extensions.Logging;

namespace Espresso.Application.DataTransferObjects;

public class ParsingErrorMessageDto
{
    public ParsingErrorMessageDto(
        LogLevel logLevel,
        string message,
        int rssFeedId)
    {
        Created = DateTimeOffset.UtcNow;
        LogLevel = logLevel;
        Message = message;
        RssFeedId = rssFeedId;
    }

    public DateTimeOffset Created { get; }

    public LogLevel LogLevel { get; }

    public string Message { get; }

    public int RssFeedId { get; }

    public override bool Equals(object? obj)
    {
        return obj is ParsingErrorMessageDto dto &&
               LogLevel == dto.LogLevel &&
               Message == dto.Message &&
               RssFeedId == dto.RssFeedId;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(LogLevel, Message, RssFeedId);
    }
}
