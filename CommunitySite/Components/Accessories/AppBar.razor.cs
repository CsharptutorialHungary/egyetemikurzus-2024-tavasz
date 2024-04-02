using CommunitySite.Data.ViewModels;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace CommunitySite.Components.Accessories
{
    public partial class AppBar
    {
        private ProtectedBrowserStorageResult<UserViewModel> _loggedUser;

        private void NavigateToRegisterPage()
        {
            navmanager.NavigateTo("/Register");
        }

        private void NavigateToLoginPage()
        {
            navmanager.NavigateTo("");
        }

        //TODO: nem hívódik meg minden betöltésnél.

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            _loggedUser = await ProtectedSessionStore.GetAsync<UserViewModel>("LoggedUser");
        }
    }
}
