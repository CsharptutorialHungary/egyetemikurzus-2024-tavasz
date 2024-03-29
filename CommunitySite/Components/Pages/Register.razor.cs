using CommunitySite.Data.ViewModels;
using CommunitySite.Extensions.Validators;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using static MudBlazor.CategoryTypes;

namespace CommunitySite.Components.Pages
{
    public partial class Register
    {
        private MudForm _form;
        private Snackbar? _snackbar;

        RegisterValidatior validationRules = new RegisterValidatior();
        [Parameter] public UserViewModel _userViewModel { get; set; } = new();

        private async Task Submit()
        {
            await _form.Validate();
            if (_form.IsValid)
            {
                UserViewModel userViewModel = _userViewModel;
                _snackbar = Snackbar.Add("Siker", Severity.Success);
            }
        }
    }
}
