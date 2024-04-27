using CommunitySite.Components.Pages;
using CommunitySite.Data.Util;
using CommunitySite.Data.ViewModels;
using CommunitySite.Extensions.Exceptions;
using CommunitySite.Services.AdminServices;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Collections;

namespace CommunitySite.Components.Dialogs
{
    public partial class EditUserDialog
    {
        [Inject] private IAdminService AdminService { get; set; } = default!;
        [Inject] private ISnackbar SnackBar { get; set; } = default!;
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; } = default!;
        [CascadingParameter] private Error CommuitySiteError { get; set; } = default!;
        [Parameter] public UserViewModel UserViewModel { get; set; } = default!;

        private IEnumerable Roles { get; set; } = default!;
        private PermissionEnum selectedRole { get; set; } = default!;

        protected override void OnInitialized()
        {
            Roles = Enum.GetValues(typeof(PermissionEnum));
            selectedRole = (PermissionEnum)UserViewModel.PermissionId;
        }

        private async Task DeleteUserButton()
        {
            try
            {
                await AdminService.DeleteUserAsync(UserViewModel);
                SnackBar.Add("Successfully deleted", Severity.Success);
                MudDialog.Close(DialogResult.Ok(true));
            }
            catch (CommunitySiteException ex)
            {
                CommuitySiteError.ErrorHandler(ex);
            }
        }

        private async Task Submit()
        {
            try
            {
                UserViewModel.PermissionId = (int)selectedRole;
                await AdminService.UpdateUserRoleAsync(UserViewModel);
                SnackBar.Add("Successfully updated", Severity.Success);
            }
            catch (CommunitySiteException ex)
            {
                CommuitySiteError.ErrorHandler(ex);
            }
        }

        void Cancel() => MudDialog!.Cancel();
    }
}
