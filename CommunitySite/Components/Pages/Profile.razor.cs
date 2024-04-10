using AutoMapper;
using CommunitySite.Services.UserServices;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using CommunitySite.Data.ViewModels;

namespace CommunitySite.Components.Pages
{
    public partial class Profile
    {
        [CascadingParameter] private Task<AuthenticationState>? authenticationStateTask { get; set; }

        [Inject] private IUserService _userService { get; set; }
        private UserViewModel userViewModel = new UserViewModel();

        protected override async Task OnInitializedAsync()
        {
            var authState = await authenticationStateTask!;
            var userName = authState.User.Identity?.Name ?? "";
            if(userName is null)
            {
                return;
            }
            userViewModel = await _userService.GetUser(userName);
            userViewModel.ShortName = userName.Split('\\').Last();
        }
    }
}
