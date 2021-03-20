using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Espresso.Dashboard.Application.Account.Login
{
    /// <summary>
    /// LoginQueryHandler
    /// </summary>
    public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginQueryResponse>
    {
        #region Fields
        private readonly SignInManager<IdentityUser> _signInManager;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// LoginQueryHandler Constructor
        /// </summary>
        public LoginQueryHandler(
            SignInManager<IdentityUser> signInManager
        )
        {
            _signInManager = signInManager;
        }
        #endregion Constructors

        #region Methods
        public async Task<LoginQueryResponse> Handle(
            LoginQuery request,
            CancellationToken cancellationToken
        )
        {
            try
            {
                var signInResult = await _signInManager.PasswordSignInAsync(
                    userName: request.Email,
                    password: request.Password,
                    isPersistent: request.IsPersistent,
                    lockoutOnFailure: true // After N failed attempts locks account for M minutes
                );


                return new LoginQueryResponse(
                    isSuccess: signInResult.Succeeded,
                    errorMessage: signInResult.Succeeded ? null : signInResult.ToString()
                );

            }
            catch (Exception e)
            {
                throw;
            }
        }
        #endregion Methods
    }
}
