using System.ComponentModel.DataAnnotations;

namespace Espresso.Dashboard.Pages.Account.Login
{
    public class UserLoginModel
    {
        [EmailAddress, Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
