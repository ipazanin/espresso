using System.Threading;
using System.Threading.Tasks;
using Espresso.Common.Constants;
using Espresso.Dashboard.Application.Account.Login;
using Espresso.Dashboard.DataTransferObjects;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Espresso.Dashboard.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        #region Fields

        private readonly ISender _sender;

        #endregion

        #region Constructors

        public AccountController(ISender sender)
        {
            _sender = sender;
        }

        #endregion

        #region Methods

        /// <summary>
        /// </summary>
        /// <returns>Response object user login response</returns>
        /// <response code="200">Response object user login response</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginQueryResponse))]
        [HttpPost]
        [AllowAnonymous]
        [Route("api/account/login")]
        public async Task<IActionResult> Login(
            UserLoginDto userLoginDto,
            CancellationToken cancellationToken
        )
        {
            var request = new LoginQuery(
                email: userLoginDto.Username,
                password: userLoginDto.Password,
                isPersistent: true
            );

            var response = await _sender.Send(
                request: request,
                cancellationToken: cancellationToken
            );

            return Ok(response);
        }

        #endregion
    }
}
