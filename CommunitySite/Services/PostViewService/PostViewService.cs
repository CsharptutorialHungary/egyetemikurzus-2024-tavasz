using CommunitySite.Components.Dialogs;
using CommunitySite.Data.ViewModels;
using MudBlazor;

namespace CommunitySite.Services.PostViewService
{
    public class PostViewService : IPostViewService
    {
        private readonly IDialogService _dialogService;

        public PostViewService(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public async Task CreateUploadPostDialog(UserViewModel user)
        {
            var parameters = new DialogParameters
            {
                ["userViewModel"] = user
            };

            var dialog = await _dialogService.ShowAsync<UploadPostDialog>("Upload post", parameters);
            await dialog.Result;
        }
    }
}
