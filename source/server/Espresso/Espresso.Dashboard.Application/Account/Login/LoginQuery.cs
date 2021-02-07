using Espresso.Application.Infrastructure.MediatorInfrastructure;
using MediatR;

namespace Espresso.Dashboard.Application.Account.Login
{
    /// <summary>
    /// LoginQuery
    /// </summary>
    public record LoginQuery : Request<LoginQueryResponse>
    {
        #region Properties
        public string Email { get; }

        public string Password { get; }

        public bool IsPersistent { get; }
        #endregion Properties

        #region Constructors
        /// <summary>
        /// LoginQuery Constructor
        /// </summary>
        public LoginQuery(
            string email,
            string password,
            bool isPersistent
        )
        {
            Email = email;
            Password = password;
            IsPersistent = isPersistent;
        }
        #endregion Constructors
    }
}
