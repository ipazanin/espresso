// PersonalData.cshtml.cs
//
// © 2021 Espresso News. All rights reserved.

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Espresso.Dashboard.Areas.Identity.Pages.Account.Manage
{
    public class PersonalDataModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonalDataModel"/> class.
        /// </summary>
        /// <param name="userManager"></param>
        public PersonalDataModel(
            UserManager<IdentityUser> userManager
        )
        {
            _userManager = userManager;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            return Page();
        }
    }
}
