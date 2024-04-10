using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace CommunitySite.Components.Accessories
{
    public partial class AppBar
    {
        [CascadingParameter] private Task<AuthenticationState>? authenticationStateTask { get; set; }

        private string userName = "";
        private MudTheme Theme = new MudTheme();

        protected override async Task OnInitializedAsync()
        {
            var authState = await authenticationStateTask!;
            userName = authState.User.Identity?.Name?.Split('\\').Last() ?? "";
        }

        private void GoToProfilePage()
        {
            navmanager.NavigateTo("/Profile");
        }
    }
}
