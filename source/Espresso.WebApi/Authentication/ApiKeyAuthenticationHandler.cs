using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using Espresso.Common.Constants;
using Espresso.WebApi.DataTransferObjects;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Espresso.WebApi.Authentication
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
    {
        private const string ProblemDetailsContentType = "application/problem+json";
        private const string UnAuthenticatedApiKeyMessage = "Given API Key is Invalid";
        private const string ForbiddenMessage = "Given API Key is Forbidden from requested resource";
        private const string ApiKeyHeaderName_1_2 = "Espresso-Api-Key";

        private readonly IApiKeyProvider _apiKeyProvider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="logger"></param>
        /// <param name="encoder"></param>
        /// <param name="clock"></param>
        /// <param name="apiKeyProvider"></param>
        public ApiKeyAuthenticationHandler(
            IOptionsMonitor<ApiKeyAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IApiKeyProvider apiKeyProvider
        ) : base(
            options: options,
            logger: logger,
            encoder: encoder,
            clock: clock
        )
        {
            _apiKeyProvider = apiKeyProvider;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue(HttpHeaderConstants.ApiKeyHeaderName, out var apiKeyHeaderValues) &&
                !Request.Headers.TryGetValue(ApiKeyHeaderName_1_2, out apiKeyHeaderValues)
            )
            {
                return AuthenticateResult.NoResult();
            }

            var providedApiKey = apiKeyHeaderValues.FirstOrDefault();

            if (apiKeyHeaderValues.Count == 0 || string.IsNullOrWhiteSpace(providedApiKey))
            {
                return AuthenticateResult.NoResult();
            }

            var existingApiKey = await _apiKeyProvider.GetApiKey(providedApiKey);

            if (existingApiKey != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, existingApiKey.Role)
                };

                var identity = new ClaimsIdentity(claims, Options.AuthenticationType);
                var identities = new List<ClaimsIdentity> { identity };
                var principal = new ClaimsPrincipal(identities);
                var ticket = new AuthenticationTicket(principal, Options.Scheme);

                return AuthenticateResult.Success(ticket);
            }

            return AuthenticateResult.Fail(UnAuthenticatedApiKeyMessage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            Response.ContentType = ProblemDetailsContentType;
            var exceptionDto = new ExceptionDto(
                exceptionMessage: UnAuthenticatedApiKeyMessage,
                innerExceptionMessage: null,
                exceptionStackTrace: null,
                innerExceptionStackTrace: null,
                errors: null
            );

            await Response.WriteAsync(JsonSerializer.Serialize(exceptionDto));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        protected override async Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            Response.StatusCode = (int)HttpStatusCode.Forbidden;
            Response.ContentType = ProblemDetailsContentType;
            var exceptionDto = new ExceptionDto(
                exceptionMessage: ForbiddenMessage,
                innerExceptionMessage: null,
                exceptionStackTrace: null,
                innerExceptionStackTrace: null,
                errors: null
            );

            await Response.WriteAsync(JsonSerializer.Serialize(exceptionDto));
        }
    }
}
