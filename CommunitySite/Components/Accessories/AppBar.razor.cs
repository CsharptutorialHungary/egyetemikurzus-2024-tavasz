using CommunitySite.Data.ViewModels;
using CommunitySite.Services.UserServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace CommunitySite.Components.Accessories
{
    public partial class AppBar
    {
        [Inject] IUserService userService {  get; set; }
        [Inject] IUserViewService userViewService {  get; set; }
        [CascadingParameter] private Task<AuthenticationState>? authenticationStateTask { get; set; }

        private string shortUserName = "";
        private string userName = "";
        private MudTheme Theme = new MudTheme();

        protected override async Task OnInitializedAsync()
        {
            var authState = await authenticationStateTask!;
            userName = authState.User.Identity?.Name ?? "";
            shortUserName = authState.User.Identity?.Name?.Split('\\').Last() ?? "";
        }

        private async Task OpenDialog()
        {
            var user = await userService.GetUser(userName);
            await userViewService.CreateUserDialog(user);
        }

        private void GoToProfilePage()
        {
            navmanager.NavigateTo("/Profile");
        }
    }
}
