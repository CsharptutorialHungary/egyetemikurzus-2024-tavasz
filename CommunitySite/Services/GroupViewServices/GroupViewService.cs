using CommunitySite.Components.Dialogs;
using CommunitySite.Data.ViewModels;
using MudBlazor;

namespace CommunitySite.Services.GroupViewServices
{
    public class GroupViewService : IGroupViewService
    {
        private readonly IDialogService _dialogService;

        public GroupViewService(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public async Task CreateGroupDialog(UserViewModel user)
        {
            var parameters = new DialogParameters
            {
                ["UserViewModel"] = user,
            };

            var dialog = await _dialogService.ShowAsync<MakeGroupDialog>("Make group", parameters);
            await dialog.Result;
        }
    }
}