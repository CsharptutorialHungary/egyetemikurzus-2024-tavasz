using CommunitySite.Components.Dialogs;
using CommunitySite.Data.ViewModels;
using MudBlazor;

namespace CommunitySite.Services.AdminViewServices
{
    public class AdminViewService : IAdminViewService
    {
        private readonly IDialogService _dialogService;

        public AdminViewService(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public async Task CreateAdminDialog(UserViewModel user)
        {
            var parameters = new DialogParameters
            {
                ["UserViewModel"] = user,
            };

            var dialog = await _dialogService.ShowAsync<EditUserDialog>("Edit user", parameters);
            await dialog.Result;
        }
    }
}
