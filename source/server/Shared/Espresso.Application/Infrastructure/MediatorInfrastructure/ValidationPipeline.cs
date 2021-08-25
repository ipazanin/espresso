// ValidationPipeline.cs
//
// © 2021 Espresso News. All rights reserved.

using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Espresso.Application.Infrastructure.MediatorInfrastructure
{
    /// <summary>
    /// <see cref="Mediator"/> pipeline component that validates <typeparamref name="TRequest"/>.
    /// </summary>
    /// <typeparam name="TRequest">Mediator request.</typeparam>
    /// <typeparam name="TResponse">mediator response.</typeparam>
    public class ValidationPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationPipeline{TRequest, TResponse}"/> class.
        /// </summary>
        /// <param name="validators">Validators.</param>
        public ValidationPipeline(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        /// <inheritdoc/>
        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext<TRequest>(request);
            var failures = _validators
                .Select(validator => validator.Validate(context))
                .SelectMany(validationResult => validationResult.Errors)
                .Where(validationFailure => validationFailure != null)
                .ToList();

            return failures.Count != 0 ?
                throw new ValidationException(failures) :
                next();
        }
    }
}
