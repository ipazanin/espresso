// ILoggerService.cs
//
// © 2022 Espresso News. All rights reserved.

using Microsoft.Extensions.Logging;

namespace Espresso.Domain.IServices;

public interface ILoggerService<TCaller>
    where TCaller : class
{
    public void Log(
        string eventName,
        LogLevel logLevel,
        IEnumerable<(string argumentName, object argumentValue)>? namedArguments = null);

    public void Log(
        string eventName,
        string errorMessage,
        LogLevel logLevel,
        IEnumerable<(string argumentName, object argumentValue)>? namedArguments = null);

    public void Log(
        string eventName,
        Exception exception,
        LogLevel logLevel,
        IEnumerable<(string argumentName, object argumentValue)>? namedArguments = null);
}
