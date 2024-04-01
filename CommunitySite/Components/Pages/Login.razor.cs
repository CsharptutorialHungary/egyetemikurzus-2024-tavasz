using CommunitySite.Data.ViewModels;
using CommunitySite.Services.UserServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MudBlazor;

namespace CommunitySite.Components.Pages
{
    public partial class Login
    {
        private Snackbar? _snackbar;
        [Inject] public IUserService userService { get; set; }
        [Parameter] public string Email {  get; set; }
        [Parameter] public string Password {  get; set; }
        [Parameter] public UserViewModel _userViewModel { get; set; } = new();

        private async Task Submit()
        {
            if (await userService.ExistUserEmailAndPasswordInDatabase(Email, Password))
            {
                var user = await userService.GetUser(Email);
                await ProtectedSessionStore.SetAsync("LoggedUser", user);
                navmanager.NavigateTo("/MainPage");
            }
            else
            {
                _snackbar = Snackbar.Add("Email or password don't exist", Severity.Error, config =>
                {
                    config.VisibleStateDuration = 3000;
                    config.HideTransitionDuration = 200;
                    config.ShowTransitionDuration = 200;
                    config.ShowCloseIcon = true;
                });
                return;
            }
            //var asd = await ProtectedSessionStore.GetAsync<UserViewModel>("LoggedUser");
        }
    }
}
