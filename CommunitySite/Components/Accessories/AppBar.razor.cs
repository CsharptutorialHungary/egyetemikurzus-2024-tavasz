using CommunitySite.Data.ViewModels;
using CommunitySite.Services.UserServices;
using CommunitySite.Services.UserViewServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace CommunitySite.Components.Accessories
{
    public partial class AppBar
    {
        [Inject] IUserService UserService { get; set; } = default!;
        [Inject] IUserViewService UserViewService {  get; set; } = default!;
        [CascadingParameter] private Task<AuthenticationState> authenticationStateTask { get; set; } = default!;

        private string shortUserName = "";
        private string userName = "";
        private MudTheme Theme = new MudTheme();
        private UserViewModel userViewModel = new();

        protected override async Task OnInitializedAsync()
        {
            var authState = await authenticationStateTask!;
            userName = authState.User.Identity?.Name ?? "";
            userViewModel = await UserService.GetUser(userName);
            shortUserName = authState.User.Identity?.Name?.Split('\\').Last() ?? "";
        }

        private async Task OpenDialog()
        {
            var user = await UserService.GetUser(userName);
            await UserViewService.CreateUserDialog(user);
        }

        private void GoToProfilePage()
        {
            navmanager.NavigateTo($"user/{userViewModel.Usertechnicalname}/profile");
        }
    }
}
