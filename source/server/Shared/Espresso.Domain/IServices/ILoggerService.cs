// ILoggerService.cs
//
// © 2022 Espresso News. All rights reserved.

using Microsoft.Extensions.Logging;

namespace Espresso.Domain.IServices;

#pragma warning disable S2326 // Unused type parameters should be removed
public interface ILoggerService<TCaller>
#pragma warning restore S2326 // Unused type parameters should be removed
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
