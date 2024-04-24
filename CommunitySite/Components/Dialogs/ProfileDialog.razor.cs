using CommunitySite.Data.ViewModels;
using CommunitySite.Extensions.Validators;
using CommunitySite.Services.UserServices;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CommunitySite.Components.Dialogs
{
    public partial class ProfileDialog
    {
        [Inject] ISnackbar? Snackbar { get; set; }
        [Inject] IUserService? userService { get; set; }
        [CascadingParameter] MudDialogInstance? MudDialog { get; set; }
        [Parameter] public UserViewModel userViewModel { get; set; } = new();

        private UserValidator validationRules = new UserValidator();
        private MudForm? userForm;

        private async Task Submit()
        {
            await userForm!.Validate();

            if(userForm.IsValid)
            {
                bool result = await userService!.UpdateUserAsync(userViewModel);
                if(result)
                {
                    Snackbar!.Add("Your profile information has been updated successfully.", Severity.Success);
                }
                else
                {
                    Snackbar!.Add("Something went wrong.", Severity.Success);
                }
            }
        }
        void Cancel() => MudDialog!.Cancel();
    }
}
