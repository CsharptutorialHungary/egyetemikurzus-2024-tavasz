using CommunitySite.Components.Pages;
using CommunitySite.Data.ViewModels;
using CommunitySite.Extensions.Validators;
using CommunitySite.Services.UserServices;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CommunitySite.Components.Dialogs
{
    public partial class ProfileDialog
    {
        [Inject] private ISnackbar Snackbar { get; set; } = default!;
        [Inject] private IUserService userService { get; set; } = default!;
        [CascadingParameter] MudDialogInstance MudDialog { get; set; } = default!;
        [CascadingParameter] private Error CommuitySiteError { get; set; } = default!;
        [Parameter] public UserViewModel userViewModel { get; set; } = new();

        private UserValidator validationRules = new();
        private MudForm userForm = default!;

        private async Task Submit()
        {
            await userForm!.Validate();

            if(userForm.IsValid)
            {
                try
                {
                    await userService.UpdateUserAsync(userViewModel);
                    Snackbar!.Add("Your profile information has been updated successfully.", Severity.Success);
                }
                catch (Exception ex)
                {
                    CommuitySiteError.ErrorHandler(ex);
                }
            }
        }

        void Cancel() => MudDialog!.Cancel();
    }
}
