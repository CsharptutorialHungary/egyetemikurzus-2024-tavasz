using CommunitySite.Components.Dialogs;
using CommunitySite.Data.ViewModels;
using MudBlazor;

namespace CommunitySite.Services.GroupPostUploadServices
{
    public class GroupPostUploadService : IGroupPostUploadService
    {
        private readonly IDialogService _dialogService;

        public GroupPostUploadService(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public async Task UploadPostToGroupDialog(UserViewModel user, GroupViewModel group)
        {
            var parameters = new DialogParameters
            {
                ["UserViewModel"] = user,
                ["GroupViewModel"] = group
            };

            var dialog = await _dialogService.ShowAsync<UploadPostToGroupDialog>("Upload post", parameters);
            await dialog.Result;
        }
    }
}
