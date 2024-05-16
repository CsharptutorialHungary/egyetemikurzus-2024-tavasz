using CommunitySite.Data.ViewModels;
using CommunitySite.Services.PostServices;
using CommunitySite.Services.UserServices;
using Microsoft.AspNetCore.Components;

namespace CommunitySite.Components.Accessories
{
    public partial class ListUserProfiles
    {
        [Inject] private IUserService UserService { get; set; } = default!;
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;

        private bool _expanded = false;
        private List<UserViewModel> allusers = new();

        private async void OnExpandCollapseClick()
        {
            _expanded = !_expanded;
            await GetUsers();
        }

        private async Task GetUsers()
        {
            allusers = await UserService.GetUsers();
        }

        private void OpenUserProfile(string technicalName)
        {
            NavigationManager.NavigateTo($"user/{technicalName}/profile");
        }
    }
}
