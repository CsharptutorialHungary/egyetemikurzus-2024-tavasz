using CommunitySite.Data.ViewModels;
using CommunitySite.Services.GroupPostUploadServices;
using CommunitySite.Services.GroupServices;
using CommunitySite.Services.PostServices;
using CommunitySite.Services.UserServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace CommunitySite.Components.Pages
{
    public partial class GroupPage
    {
        [Inject] private IGroupPostUploadService GroupPostUploadServices { get; set; } = default!;
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;
        [Inject] private IUserService UserService { get; set; } = default!;
        [Inject] private IGroupService GroupService { get; set; } = default!;
        [Inject] private IPostService PostService { get; set; } = default!;
        [CascadingParameter] private Task<AuthenticationState> authenticationStateTask { get; set; } = default!;
        [Parameter] public string GroupTechnicalName { get; set; } = default!;

        private UserViewModel loggedUserViewModel = new();
        private GroupViewModel currentGroup = new();
        private string authUsername = string.Empty;
        private bool isMemberOfGroup = false;
        private bool _expanded = false;
        private List<PostViewModel> posts = new();
        private int groupMembers = 0;

        protected override async Task OnInitializedAsync()
        {
            var authState = await authenticationStateTask!;
            authUsername = authState.User.Identity?.Name ?? "";
            loggedUserViewModel = await UserService.GetUser(authUsername);
            isMemberOfGroup = await GroupService.IsUsermemberOfGroup(GroupTechnicalName, loggedUserViewModel);
            currentGroup = await GroupService.GetGroupByTechnicalId(GroupTechnicalName);
            await CountGroupMembers();
        }

        protected override async Task OnParametersSetAsync()
        {
            isMemberOfGroup = await GroupService.IsUsermemberOfGroup(GroupTechnicalName, loggedUserViewModel);
            currentGroup = await GroupService.GetGroupByTechnicalId(GroupTechnicalName);
            await CountGroupMembers();
        }

        private async Task CountGroupMembers()
        {
            groupMembers = await GroupService.CountGroupMembersAsync(currentGroup);
        }

        private async Task JoingroupAsync()
        {
            await GroupService.AddUserToGroupAsync(loggedUserViewModel, currentGroup);
            NavigationManager.NavigateTo(NavigationManager.Uri, true);
        }

        private async Task OpenPostUploadToGroupDialog()
        {
            await GroupPostUploadServices.UploadPostToGroupDialog(loggedUserViewModel, currentGroup);
        }

        private async void OnExpandCollapseClick()
        {
            _expanded = !_expanded;
            await GetGroupPosts();
        }

        private async Task GetGroupPosts()
        {
            posts = await PostService.GetPostsInGroup(currentGroup);
        }

        //TODO :
        //Barátnak jelölés
        //Más felhasználó profiljának megtekintése
        //Üzenet küldés az ismerősnek
    }
}
