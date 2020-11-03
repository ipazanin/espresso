using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Espresso.WebApi.Application.Exceptions;
using Espresso.Application.IServices;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
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
        private readonly ILoggerService<CustomExceptionFilterAttribute> _loggerService;
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="webApiConfiguration"></param>
        /// <param name="slackService"></param>
        /// <param name="loggerService"></param>
        public CustomExceptionFilterAttribute(
            IWebApiConfiguration webApiConfiguration,
            ISlackService slackService,
            ILoggerService<CustomExceptionFilterAttribute> loggerService
        )
        {
            _webApiConfiguration = webApiConfiguration;
            _slackService = slackService;
            _loggerService = loggerService;
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

            var exceptionBaseModel = new ExceptionDto(
                exceptionMessage: context.Exception.Message,
                exceptionStackTrace: context.Exception.StackTrace,
                innerExceptionMessage: context.Exception.InnerException?.Message,
                innerExceptionStackTrace: context.Exception.InnerException?.StackTrace,
                errors: errors
            );

            var exceptionModel = _webApiConfiguration.AppConfiguration.AppEnvironment switch
            {
                AppEnvironment.Prod => new ExceptionDto(
                    exceptionMessage: FormatConstants.UnhandledExceptionMessage,
                    innerExceptionMessage: null,
                    exceptionStackTrace: null,
                    innerExceptionStackTrace: null,
                    errors: errors
                ),
                AppEnvironment.Undefined => exceptionBaseModel,
                AppEnvironment.Local => exceptionBaseModel,
                AppEnvironment.Dev => exceptionBaseModel,
                _ => exceptionBaseModel,
            };

            context.Result = new JsonResult(
                value: exceptionModel
            );

            var eventName = Event.CustomExceptionFilterAttribute.GetDisplayName();
            var version = _webApiConfiguration.AppConfiguration.Version;
            var exceptionMessage = context.Exception.Message;
            var innerExceptionMessage = context.Exception.InnerException?.Message ?? "";

            var arguments = new (string, object)[]{
                (nameof(version), version)
            };

            _loggerService.Log(eventName, context.Exception, LogLevel.Error, arguments);

            return _slackService.LogError(
                    eventName: eventName,
                    version: version,
                    message: context.Exception.Message,
                    exception: context.Exception,
                    appEnvironment: _webApiConfiguration.AppConfiguration.AppEnvironment,
                    cancellationToken: default
            );
        }
        #endregion
    }
}
