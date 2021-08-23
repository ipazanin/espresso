// ExceptionDto.cs
//
// © 2021 Espresso News. All rights reserved.

using System.Collections.Generic;
using Espresso.Common.Constants;

namespace Espresso.WebApi.DataTransferObjects
{
    /// <summary>
    /// Exception data transfer object.
    /// </summary>
    public class ExceptionDto
    {
        /// <summary>
        /// Gets exception message.
        /// </summary>
        public string ExceptionMessage { get; }

        /// <summary>
        /// Gets inner exception message.
        /// </summary>
        public string InnerExceptionMessage { get; }

        /// <summary>
        /// Gets exception stack trace.
        /// </summary>
        public string ExceptionStackTrace { get; }

        /// <summary>
        /// Gets inner exception stack trace.
        /// </summary>
        public string InnerExceptionStackTrace { get; }

        /// <summary>
        /// gets errors.
        /// </summary>
        public IEnumerable<string>? Errors { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionDto"/> class.
        /// </summary>
        /// <param name="exceptionMessage">Exception message.</param>
        /// <param name="innerExceptionMessage">Inner exception message.</param>
        /// <param name="exceptionStackTrace">Exception stack trace.</param>
        /// <param name="innerExceptionStackTrace">Inner exception stack trace.</param>
        /// <param name="errors">Errors.</param>
        public ExceptionDto(
            string exceptionMessage,
            string? innerExceptionMessage,
            string? exceptionStackTrace,
            string? innerExceptionStackTrace,
            IEnumerable<string>? errors
        )
        {
            ExceptionMessage = exceptionMessage;
            InnerExceptionMessage = innerExceptionMessage ?? FormatConstants.EmptyValue;
            ExceptionStackTrace = exceptionStackTrace ?? FormatConstants.EmptyValue;
            InnerExceptionStackTrace = innerExceptionStackTrace ?? FormatConstants.EmptyValue;
            Errors = errors;
        }
    }
}
