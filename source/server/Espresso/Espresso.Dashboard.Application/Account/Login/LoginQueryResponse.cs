namespace Espresso.Dashboard.Application.Account.Login
{
    /// <summary>
    /// LoginResponse
    /// </summary>
    public class LoginQueryResponse
    {
        #region Properties
        public bool IsSuccess { get; }
        public string? ErrorMessage { get; }
        #endregion Properties

        #region Constructors
        /// <summary>
        /// LoginResponse Constructor
        /// </summary>
        public LoginQueryResponse(
            bool isSuccess,
            string? errorMessage
        )
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }
        #endregion Constructors
    }
}
