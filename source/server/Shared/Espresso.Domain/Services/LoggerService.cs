// LoggerService.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Common.Utilities;
using Espresso.Domain.IServices;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace Espresso.Domain.Services;

public class LoggerService<TCaller> : ILoggerService<TCaller>
    where TCaller : class
{
    private readonly ILogger<TCaller> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoggerService{TCaller}"/> class.
    /// </summary>
    /// <param name="loggerFactory"></param>
    public LoggerService(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<TCaller>();
    }

    public void Log(
        string eventName,
        LogLevel logLevel,
        IReadOnlyList<(string argumentName, object argumentValue)>? namedArguments = null)
    {
        var count = 0;
        var messageBuilder = new StringBuilder(
            $"{AnsiUtility.EncodeEventName(count++)}\n\t");
        var defaultArguments = new List<object>
            {
                eventName,
            };

        var (message, loggerArguments) = GetMessageAndArguments(
            messageBuilder,
            namedArguments,
            defaultArguments,
            count);

        Log(message, loggerArguments, logLevel, null);
    }

    public void Log(
        string eventName,
        string errorMessage,
        LogLevel logLevel,
        IReadOnlyList<(string argumentName, object argumentValue)>? namedArguments = null)
    {
        var count = 0;
        var messageBuilder = new StringBuilder(
            $"{AnsiUtility.EncodeEventName(count++)}\n\t" +
            $"{AnsiUtility.EncodeParameterName("Exception Message")}: {AnsiUtility.EncodeErrorMessage(count++)}\n\t");
        var defaultArguments = new List<object>
            {
                eventName,
                errorMessage,
            };

        var (message, loggerArguments) = GetMessageAndArguments(
            messageBuilder,
            namedArguments,
            defaultArguments,
            count);

        Log(message, loggerArguments, logLevel, null);
    }

    public void Log(
        string eventName,
        Exception exception,
        LogLevel logLevel,
        IReadOnlyList<(string argumentName, object argumentValue)>? namedArguments = null)
    {
        var count = 0;
        var messageBuilder = new StringBuilder(
            $"{AnsiUtility.EncodeEventName(count++)}\n\t" +
            $"{AnsiUtility.EncodeParameterName("Exception Message")}: {AnsiUtility.EncodeErrorMessage(count++)}\n\t" +
            $"{AnsiUtility.EncodeParameterName("Inner Exception Message")}: {AnsiUtility.EncodeErrorMessage(count++)}\n\t");
        var defaultArguments = new List<object>
            {
                eventName,
                exception.Message,
                exception.InnerException?.Message ?? string.Empty,
            };

        var (message, loggerArguments) = GetMessageAndArguments(
            messageBuilder,
            namedArguments,
            defaultArguments,
            count);

        Log(message, loggerArguments, logLevel, exception);
    }

    private static (string message, object[] args) GetMessageAndArguments(
        StringBuilder messageBuilder,
        IReadOnlyList<(string argumentName, object argumentValue)>? arguments,
        List<object> args,
        int count)
    {
        if (arguments is not null)
        {
            foreach (var (argumentName, argumentValue) in arguments)
            {
                var argumentMessage = GetArgumentEncoding(argumentName, argumentValue, ref count);
                _ = messageBuilder.Append(argumentMessage);
                args.Add(argumentValue);
            }
        }

        return (messageBuilder.ToString(), args.ToArray());
    }

    private static string GetArgumentEncoding(string argumentName, object argumentValue, ref int count)
    {
        return argumentValue switch
        {
            int or
            uint or
            long or
            ulong or
            float or
            double or
            decimal => $"{AnsiUtility.EncodeParameterName(argumentName)}: {AnsiUtility.EncodeNumber(count++)}\n\t",
            Enum => $"{AnsiUtility.EncodeParameterName(argumentName)}: {AnsiUtility.EncodeEnum(count++)}\n\t",
            TimeSpan => $"{AnsiUtility.EncodeParameterName(argumentName)}: {AnsiUtility.EncodeTimespan(count++)}\n\t",
            DateTime or
            DateTimeOffset => $"{AnsiUtility.EncodeParameterName(argumentName)}: {AnsiUtility.EncodeDateTime(count++)}\n\t",
            string or
            StringBuilder or
            StringValues => $"{AnsiUtility.EncodeParameterName(argumentName)}: {AnsiUtility.EncodeString(count++)}\n\t",
            _ => $"{AnsiUtility.EncodeParameterName(argumentName)}: {AnsiUtility.EncodeObject(count++)}\n\t",
        };
    }

    // The logging message template should not vary between calls
#pragma warning disable CA2254
    private void Log(string message, object[] args, LogLevel logLevel, Exception? exception)
    {
        switch (logLevel)
        {
            case LogLevel.Trace:
                _logger.LogTrace(exception: exception, message: message, args: args);
                break;
            case LogLevel.Debug:
                _logger.LogDebug(exception: exception, message: message, args: args);
                break;
            case LogLevel.Information:
                _logger.LogInformation(exception: exception, message: message, args: args);
                break;
            case LogLevel.Warning:
                _logger.LogWarning(exception: exception, message: message, args: args);
                break;
            case LogLevel.Error:
                _logger.LogError(exception: exception, message: message, args: args);
                break;
            case LogLevel.Critical:
                _logger.LogCritical(exception: exception, message: message, args: args);
                break;
            case LogLevel.None:
                break;
            default:
                break;
        }
    }
}

// The logging message template should not vary between calls
#pragma warning restore CA2254
