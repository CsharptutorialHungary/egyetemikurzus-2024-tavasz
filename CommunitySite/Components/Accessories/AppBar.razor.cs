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
    }
}
