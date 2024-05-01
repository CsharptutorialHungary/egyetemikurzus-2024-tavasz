using CommunitySite.Components.Pages;
using CommunitySite.Data.ViewModels;
using CommunitySite.Services.FriendServices;
using CommunitySite.Services.GroupViewServices;
using CommunitySite.Services.PostServices;
using CommunitySite.Services.PostViewService;
using CommunitySite.Services.UserServices;
using CommunitySite.Services.UserViewServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace CommunitySite.Components.Accessories
{
    public partial class ProfileDetails
    {
        [Inject] private IUserService UserService { get; set; } = default!;
        [Inject] private ISnackbar Snackbar { get; set; } = default!;
        [Inject] private IUserViewService UserViewService { get; set; } = default!;
        [Inject] private IPostViewService PostViewService { get; set; } = default!;
        [Inject] private IGroupViewService GroupViewService { get; set; } = default!;
        [Inject] private IFriendService FriendService { get; set; } = default!;
        [Parameter] public string CurrentTechnicalId { get; set; } = default!;
        [CascadingParameter] private Task<AuthenticationState> authenticationStateTask { get; set; } = default!;
        [CascadingParameter] private Error CommuitySiteError { get; set; } = default!;

        private UserViewModel loggedUserViewModel { get; set; } = new();
        private UserViewModel currentUserViewModel { get; set; } = new();
        private string authenticatedUserName = string.Empty;


        protected override async Task OnInitializedAsync()
        {
            var authState = await authenticationStateTask!;
            authenticatedUserName = authState.User.Identity?.Name ?? "";
            currentUserViewModel = await UserService.GetUserByTechnicalId(CurrentTechnicalId);
            loggedUserViewModel = await UserService.GetUser(authenticatedUserName);
        }

        private async Task<UserViewModel> GetUser(string userName)
        {
            return await UserService.GetUser(userName);
        }

        private async Task OpenProfileEditDialog()
        {
            await UserViewService.CreateUserDialog(currentUserViewModel);
            currentUserViewModel = await GetUser(currentUserViewModel.Username);
        }

        private async Task OpenPostUploadDialog()
        {
            await PostViewService.CreateUploadPostDialog(currentUserViewModel);
        }

        private async Task OpenGroupAddDialog()
        {
            await GroupViewService.CreateGroupDialog(currentUserViewModel);
        }

        private async Task FollowUser()
        {
            try
            {
                await FriendService.FollowUserAsync(loggedUserViewModel, currentUserViewModel);
                Snackbar.Add("Successfully followed!");
            }
            catch (Exception ex)
            {
                CommuitySiteError.ErrorHandler(ex);
            }
        }
    }
}
