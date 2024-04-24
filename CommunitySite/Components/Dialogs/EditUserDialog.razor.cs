using CommunitySite.Data.Util;
using CommunitySite.Data.ViewModels;
using CommunitySite.Services.AdminServices;
using CommunitySite.Services.UserServices;
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
        [Parameter] public UserViewModel UserViewModel { get; set; } = default!;

        private MudForm MudForm { get; set; } = default!;
        private string userPermission = default!;
        private IEnumerable Roles { get; set; } = default!;
        private PermissionEnum selectedRole { get; set; }

        protected override void OnInitialized()
        {
            Roles = Enum.GetValues(typeof(PermissionEnum));
            selectedRole = (PermissionEnum)UserViewModel.PermissionId;
        }

        private async Task DeleteUserButton()
        {
            var result = await AdminService.DeleteUserAsync(UserViewModel);

            if (result)
            {
                SnackBar.Add("Successfully deleted", Severity.Success);
                MudDialog.Close(DialogResult.Ok(true));
            }
            else
            {
                SnackBar.Add("Something went wrong.", Severity.Error);
            }

        }

        private async Task Submit()
        {
            UserViewModel.PermissionId = (int)selectedRole;
            var result = await AdminService.UpdateUserRoleAsync(UserViewModel);

            if (result)
            {
                SnackBar.Add("Successfully updated", Severity.Success);
            }
            else
            {
                SnackBar.Add("Something went wrong.", Severity.Error);
            }
            
        }


        void Cancel() => MudDialog!.Cancel();
    }
}
