using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Espresso.Application.Exceptions;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.Domain.Extensions;
using Espresso.Domain.IServices;
using Espresso.WebApi.Configuration;
using Espresso.WebApi.DataTransferObjects;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

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
        private readonly ILoggerService _loggerService;
        private readonly IWebApiConfiguration _webApiConfiguration;

        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loggerService"></param>
        /// <param name="webApiConfiguration"></param>
        public CustomExceptionFilterAttribute(
            ILoggerService loggerService,
            IWebApiConfiguration webApiConfiguration
        )
        {
            _loggerService = loggerService;
            _webApiConfiguration = webApiConfiguration;
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

            var unhandledExceptionModel = _webApiConfiguration.AppEnvironment switch
            {
                AppEnvironment.Prod => new UnhandledExceptionDto(
                    exceptionMessage: FormatConstants.UnhandledExceptionMessage,
                    innerExceptionMessage: null,
                    exceptionStackTrace: null,
                    innerExceptionStackTrace: null,
                    errors: errors
                ),
                _ => new UnhandledExceptionDto(
                    exceptionMessage: context.Exception.Message,
                    innerExceptionMessage: context.Exception.StackTrace,
                    exceptionStackTrace: context.Exception.InnerException?.Message,
                    innerExceptionStackTrace: context.Exception.InnerException?.StackTrace,
                    errors: errors
                ),
            };

            context.Result = new JsonResult(
                value: unhandledExceptionModel
            );

            return _loggerService.LogError(
                eventId: (int)Event.CustomExceptionFilterAttribute,
                eventName: Event.CustomExceptionFilterAttribute.GetDisplayName(),
                version: _webApiConfiguration.Version,
                message: context.Exception.Message,
                exception: context.Exception,
                cancellationToken: default
            );
        }
        #endregion
    }
}
