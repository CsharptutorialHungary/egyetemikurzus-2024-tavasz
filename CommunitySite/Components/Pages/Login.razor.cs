using CommunitySite.Data.ViewModels;
using CommunitySite.Services.UserServices;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CommunitySite.Components.Pages
{
    public partial class Login
    {
        private MudForm _form;
        private Snackbar? _snackbar;
        [Inject] public IUserService userService { get; set; }
        [Parameter] public UserViewModel _userViewModel { get; set; } = new();


    }
}
