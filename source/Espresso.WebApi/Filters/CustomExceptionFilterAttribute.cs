using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Espresso.WebApi.Application.Exceptions;
using Espresso.Application.IServices;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.Common.Utilities;
using Espresso.Domain.Extensions;
using Espresso.WebApi.Configuration;
using Espresso.WebApi.DataTransferObjects;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Espresso.WebApi.Filters
{
    /// <summary>
    /// Custom Exception Filter
    /// Filters thrown exceptions and sets appropriate HTTP Status Code
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        #region Fields
        private readonly IWebApiConfiguration _webApiConfiguration;
        private readonly ISlackService _slackService;
        private readonly ILogger<CustomExceptionFilterAttribute> _logger;
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="webApiConfiguration"></param>
        /// <param name="slackService"></param>
        /// <param name="loggerFactory"></param>
        public CustomExceptionFilterAttribute(
            IWebApiConfiguration webApiConfiguration,
            ISlackService slackService,
            ILoggerFactory loggerFactory
        )
        {
            _webApiConfiguration = webApiConfiguration;
            _slackService = slackService;
            _logger = loggerFactory.CreateLogger<CustomExceptionFilterAttribute>();
        }
        #endregion 

        #region Methods
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

            var exceptionModel = new UnhandledExceptionDto(
                exceptionMessage: context.Exception.Message,
                exceptionStackTrace: context.Exception.StackTrace,
                innerExceptionMessage: context.Exception.InnerException?.Message,
                innerExceptionStackTrace: context.Exception.InnerException?.StackTrace,
                errors: errors
            );

            var unhandledExceptionModel = _webApiConfiguration.AppConfiguration.AppEnvironment switch
            {
                AppEnvironment.Prod => new UnhandledExceptionDto(
                    exceptionMessage: FormatConstants.UnhandledExceptionMessage,
                    innerExceptionMessage: null,
                    exceptionStackTrace: null,
                    innerExceptionStackTrace: null,
                    errors: errors
                ),
                AppEnvironment.Undefined => exceptionModel,
                AppEnvironment.Local => exceptionModel,
                AppEnvironment.Dev => exceptionModel,
                _ => exceptionModel,
            };

            context.Result = new JsonResult(
                value: unhandledExceptionModel
            );

            var eventName = Event.CustomExceptionFilterAttribute.GetDisplayName();
            var eventId = (int)Event.CustomExceptionFilterAttribute;
            var version = _webApiConfiguration.AppVersionConfiguration.Version;
            var message = context.Exception.Message;
            var exceptionMessage = context.Exception.Message;
            var innerExceptionMessage = context.Exception.InnerException?.Message ?? "";

            _logger.LogError(
                eventId: new EventId(
                    id: eventId,
                    name: eventName
                ),
                exception: context.Exception,
                message: $"{AnsiUtility.EncodeEventName("{0}")}\n\t" +
                    $"{AnsiUtility.EncodeParameterName(nameof(version))}: " +
                    $"{AnsiUtility.EncodeVersion("{1}")}\n\t" +
                    $"{AnsiUtility.EncodeParameterName(nameof(exceptionMessage))}: " +
                    $"{AnsiUtility.EncodeErrorMessage("{2}")}\n\t" +
                    $"{AnsiUtility.EncodeParameterName(nameof(innerExceptionMessage))}: " +
                    $"{AnsiUtility.EncodeErrorMessage("{3}")}",
                args: new object[]
                {
                    eventName,
                    version,
                    exceptionMessage,
                    innerExceptionMessage,
                }
            );

            return _slackService.LogError(
                    eventName: eventName,
                    version: version,
                    message: message,
                    exception: context.Exception,
                    appEnvironment: _webApiConfiguration.AppConfiguration.AppEnvironment,
                    cancellationToken: default
            );
        }
        #endregion
    }
}
