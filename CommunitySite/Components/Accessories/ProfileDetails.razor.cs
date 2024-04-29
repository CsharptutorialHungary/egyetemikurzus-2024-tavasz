using CommunitySite.Data.ViewModels;
using CommunitySite.Services.PostServices;
using CommunitySite.Services.PostViewService;
using CommunitySite.Services.UserServices;
using CommunitySite.Services.UserViewServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace CommunitySite.Components.Accessories
{
    public partial class ProfileDetails
    {
        [Inject] private IUserService UserService { get; set; } = default!;
        [Inject] IUserViewService UserViewService { get; set; } = default!;
        [Inject] IPostViewService PostViewService { get; set; } = default!;
        [CascadingParameter] private Task<AuthenticationState> authenticationStateTask { get; set; } = default!;

        private UserViewModel userViewModel { get; set; } = new();
        private string authenticatedUserName = string.Empty;


        protected override async Task OnInitializedAsync()
        {
            var authState = await authenticationStateTask!;
            authenticatedUserName = authState.User.Identity?.Name ?? "";
            userViewModel = await GetUser(authenticatedUserName);
        }

        private async Task<UserViewModel> GetUser(string userName)
        {
            return await UserService.GetUser(userName);
        }

        private async Task OpenProfileEditDialog()
        {
            await UserViewService.CreateUserDialog(userViewModel);
            userViewModel = await GetUser(authenticatedUserName);
        }

        private async Task OpenPostUploadDialog()
        {
            await PostViewService.CreateUploadPostDialog(userViewModel);
        }
    }
}
