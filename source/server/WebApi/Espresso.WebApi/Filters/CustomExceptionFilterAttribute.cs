﻿// CustomExceptionFilterAttribute.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Net;
using Espresso.Application.Services.Contracts;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.Common.Extensions;
using Espresso.Domain.IServices;
using Espresso.WebApi.Application.Exceptions;
using Espresso.WebApi.Configuration;
using Espresso.WebApi.DataTransferObjects;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Espresso.WebApi.Filters;

/// <summary>
/// Custom Exception Filter
/// Filters thrown exceptions and sets appropriate HTTP Status Code.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class CustomExceptionFilterAttribute : ExceptionFilterAttribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CustomExceptionFilterAttribute"/> class.
    /// </summary>
    /// <param name="webApiConfiguration"></param>
    /// <param name="slackService"></param>
    /// <param name="loggerService"></param>
    public CustomExceptionFilterAttribute(
        IWebApiConfiguration webApiConfiguration,
        ISlackService slackService,
        ILoggerService<CustomExceptionFilterAttribute> loggerService)
    {
        WebApiConfiguration = webApiConfiguration;
        SlackService = slackService;
        LoggerService = loggerService;
    }

    public IWebApiConfiguration WebApiConfiguration { get; }

    public ISlackService SlackService { get; }

    public ILoggerService<CustomExceptionFilterAttribute> LoggerService { get; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="context"></param>
    public override Task OnExceptionAsync(ExceptionContext context)
    {
        var code = HttpStatusCode.InternalServerError;
        IEnumerable<string>? errors = null;

        if (context.Exception is ValidationException validationException)
        {
            code = HttpStatusCode.BadRequest;
            errors = validationException.Errors.Select(validationFailure => validationFailure.ErrorMessage);
        }

        if (context.Exception is NotFoundException)
        {
            code = HttpStatusCode.NotFound;
        }

        context.HttpContext.Response.ContentType = MimeTypeConstants.Json;
        context.HttpContext.Response.StatusCode = (int)code;

        var exceptionBaseModel = new ExceptionDto(
            exceptionMessage: context.Exception.Message,
            innerExceptionMessage: context.Exception.InnerException?.Message,
            exceptionStackTrace: context.Exception.StackTrace,
            innerExceptionStackTrace: context.Exception.InnerException?.StackTrace,
            errors: errors);

        var exceptionModel = WebApiConfiguration.AppConfiguration.AppEnvironment switch
        {
            AppEnvironment.Prod => new ExceptionDto(
                exceptionMessage: FormatConstants.UnhandledExceptionMessage,
                innerExceptionMessage: null,
                exceptionStackTrace: null,
                innerExceptionStackTrace: null,
                errors: errors),
            AppEnvironment.Undefined => exceptionBaseModel,
            AppEnvironment.Local => exceptionBaseModel,
            AppEnvironment.Dev => exceptionBaseModel,
            _ => exceptionBaseModel,
        };

        context.Result = new JsonResult(
            value: exceptionModel);

        var eventName = LoggingEvent.CustomExceptionFilterAttribute.GetDisplayName();
        var version = WebApiConfiguration.AppConfiguration.Version;

        var arguments = new (string, object)[]
        {
                (nameof(version), version),
        };

        LoggerService.Log(eventName, context.Exception, LogLevel.Error, arguments);

        return SlackService.LogError(
                eventName: eventName,
                message: context.Exception.Message,
                exception: context.Exception,
                cancellationToken: default);
    }
}
