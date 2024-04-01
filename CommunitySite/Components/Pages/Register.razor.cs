using CommunitySite.Data.ViewModels;
using CommunitySite.Extensions.Validators;
using CommunitySite.Services.UserServices;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.NetworkInformation;
using static MudBlazor.CategoryTypes;

namespace CommunitySite.Components.Pages
{
    public partial class Register
    {
        private MudForm _form;
        private Snackbar? _snackbar;
        [Inject] public IUserService userService { get; set; }
        [Inject] public RegisterValidatior validationRules { get; private set; } = null!;
        [Parameter] public UserViewModel _userViewModel { get; set; } = new();

        private async Task Submit()
        {
            await _form.Validate();
            if (_form.IsValid)
            {
                await userService.SetUserToDatabase(_userViewModel);
                await ProtectedSessionStore.SetAsync("LoggedUser", _userViewModel);
                navmanager.NavigateTo("/MainPage");
            }
            var asd = await ProtectedSessionStore.GetAsync<UserViewModel>("LoggedUser");
        }
    }
}
