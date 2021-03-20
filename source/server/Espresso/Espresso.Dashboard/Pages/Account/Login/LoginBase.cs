using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Espresso.Common.Services;
using Espresso.Dashboard.Application.Account.Login;
using Espresso.Dashboard.DataTransferObjects;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace Espresso.Dashboard.Pages.Account.Login
{
    /// <summary>
    /// LoginBase
    /// </summary>
    // [Route("/account/login")]
    public class LoginBase : ComponentBase
    {
        #region Properties

        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        [Inject]
        public ISender Sender { get; set; } = null!;

        [Inject]
        public IHttpClientFactory HttpClientFactory { get; set; } = null!;

        protected string Email { get; set; } = "";

        protected string Password { get; set; } = "";

        protected bool Loading { get; set; } = false;

        protected string? ErrorMessage { get; set; }

        #endregion

        protected async Task OnValidSubmit()
        {
            Loading = true;
            try
            {
                var httpClient = HttpClientFactory.CreateClient();
                var response = await httpClient.PostAsJsonAsync(
                    requestUri: $"{NavigationManager.BaseUri}api/account/login",
                    value: new UserLoginDto(
                        username: Email,
                        password: Password
                    )
                );

                var loginResponse = await response.Content.ReadFromJsonAsync<LoginQueryResponse>();

                ErrorMessage = loginResponse?.ErrorMessage;

                if (ErrorMessage is not null)
                {
                    NavigationManager.NavigateTo("/");
                }
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
