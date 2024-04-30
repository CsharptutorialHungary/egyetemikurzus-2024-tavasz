using CommunitySite.Data.ViewModels;
using CommunitySite.Services.GroupServices;
using CommunitySite.Services.UserServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace CommunitySite.Components.Pages
{
    public partial class GroupPage
    {
        [Inject] private IUserService UserService { get; set; } = default!;
        [Inject] private IGroupService GroupService { get; set; } = default!;
        [CascadingParameter] private Task<AuthenticationState> authenticationStateTask { get; set; } = default!;
        [Parameter] public string GroupTechnicalName { get; set; } = default!;

        private UserViewModel loggedUserViewModel = new();
        private GroupViewModel currentGroup = new();
        private string authUsername = string.Empty;
        private bool isMemberOfGroup = false;

        protected override async Task OnInitializedAsync()
        {
            var authState = await authenticationStateTask!;
            authUsername = authState.User.Identity?.Name ?? "";
            loggedUserViewModel = await UserService.GetUser(authUsername);
            isMemberOfGroup = await GroupService.IsUsermemberOfGroup(GroupTechnicalName, loggedUserViewModel);
            currentGroup = await GroupService.GetGroupByTechnicalId(GroupTechnicalName);
        }

        protected override async Task OnParametersSetAsync()
        {
            isMemberOfGroup = await GroupService.IsUsermemberOfGroup(GroupTechnicalName, loggedUserViewModel);
            currentGroup = await GroupService.GetGroupByTechnicalId(GroupTechnicalName);
        }



        //TODO :
        //Csoportokba posztolás
        //Csoportokba csatlakozás
        //Barátnak jelölés
        //Más felhasználó profiljának megtekintése
        //Üzenet küldés az ismerősnek
    }
}
