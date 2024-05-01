using CommunitySite.Data.ViewModels;
using CommunitySite.Services.PostServices;
using CommunitySite.Services.UserServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace CommunitySite.Components.Pages
{
    public partial class Home
    {
        [Inject] private IUserService UserService { get; set; } = default!;
        [Inject] private IPostService PostService { get; set; } = default!;
        [CascadingParameter] private Task<AuthenticationState> authenticationStateTask { get; set; } = default!;

        private bool _postExpanded = false;
        private List<PostViewModel> followedUserPosts = new();
        private UserViewModel loggedUserViewModel { get; set; } = new();
        private string authenticatedUserName = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            var authState = await authenticationStateTask!;
            authenticatedUserName = authState.User.Identity?.Name ?? "";
            loggedUserViewModel = await UserService.GetUser(authenticatedUserName);
        }
        private async void OnPostExpandCollapseClick()
        {
            _postExpanded = !_postExpanded;
            await GetFollowedUsersPost();
        }

        private async Task GetFollowedUsersPost()
        {
            followedUserPosts = await PostService.GetFollowedUserPostsAsync(loggedUserViewModel);
        }
    }
}
