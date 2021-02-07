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
        private readonly UserManager<IdentityUser> _userManager;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// LoginQueryHandler Constructor
        /// </summary>
        public LoginQueryHandler(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager
        )
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        #endregion Constructors

        #region Methods
        public async Task<LoginQueryResponse> Handle(
            LoginQuery request,
            CancellationToken cancellationToken
        )
        {
            var signInResult = await _signInManager.PasswordSignInAsync(
                userName: request.Email,
                password: request.Password,
                isPersistent: request.IsPersistent,
                lockoutOnFailure: true // After 5 (can be changed in Identity Configuration) failed attempts locks account for 5 minutes
            );

            return new LoginQueryResponse(isSuccess: signInResult.Succeeded, errorMessage: signInResult.ToString());
        }
        #endregion Methods
    }
}
