using System.Text.Json.Serialization;

namespace Espresso.Dashboard.DataTransferObjects
{
    public class UserLoginDto
    {
        #region Properties

        public string Username { get; }

        public string Password { get; }

        #endregion

        #region Constructors

        [JsonConstructor]
        public UserLoginDto(string username, string password)
        {
            Username = username;
            Password = password;
        }

        #endregion
    }
}
