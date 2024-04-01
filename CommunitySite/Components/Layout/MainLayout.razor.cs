
using CommunitySite.Data.ViewModels;

namespace CommunitySite.Components.Layout
{
    public partial class MainLayout
    {
        private UserViewModel? userViewModel;
        protected override void OnInitialized()
        {
            userViewModel = (UserViewModel)ProtectedSessionStore.GetAsync<UserViewModel>("LoggedUser");
        }
    }
}
