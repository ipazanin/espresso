using System;
using System.Threading.Tasks;
using Espresso.Dashboard.Application.Account.Login;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace Espresso.Dashboard.Pages.Account.Login
{
    /// <summary>
    /// LoginBase
    /// </summary>
    public class LoginBase : ComponentBase
    {
        #region Properties

        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        [Inject]
        public ISender Sender { get; set; } = null!;

        protected UserLoginModel User { get; } = new();

        protected bool Loading { get; set; } = false;

        protected string? ErrorMessage { get; set; }

        #endregion

        protected async Task OnValidSubmit()
        {
            Loading = true;
            try
            {
                await Sender.Send(
                    new LoginQuery(
                        email: User.Email ?? string.Empty,
                        password: User.Password ?? string.Empty,
                        isPersistent: false
                    )
                );
                NavigationManager.NavigateTo("/");
            }
            catch (Exception exception)
            {
                ErrorMessage = exception.Message;
                StateHasChanged();
            }
            finally
            {
                Loading = false;
            }
        }
    }
}
