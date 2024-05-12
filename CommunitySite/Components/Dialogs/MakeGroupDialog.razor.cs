using CommunitySite.Components.Pages;
using CommunitySite.Data.ViewModels;
using CommunitySite.Extensions.Validators;
using CommunitySite.Services.GroupServices;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CommunitySite.Components.Dialogs
{
    public partial class MakeGroupDialog
    {
        [Inject] private IGroupService GroupService { get; set; } = default!;
        [Inject] private ISnackbar Snackbar { get; set; } = default!;
        [CascadingParameter] MudDialogInstance MudDialog { get; set; } = default!;
        [CascadingParameter] private Error CommuitySiteError { get; set; } = default!;
        [Parameter] public UserViewModel userViewModel { get; set; } = new();
        private GroupValidator validationRules = new();
        private MudForm groupForm = default!;
        private GroupViewModel groupViewModel = new();

        private async Task Submit()
        {
            await groupForm!.Validate();

            if (groupForm.IsValid)
            {
                try
                {
                    groupViewModel.Ownerid = userViewModel.Userid;
                    await GroupService.CreateGroupAsync(groupViewModel);
                    Snackbar.Add("Successfully created!", Severity.Success);
                    MudDialog.Close(DialogResult.Ok(true));
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
