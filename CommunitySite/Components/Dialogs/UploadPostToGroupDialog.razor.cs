using CommunitySite.Components.Pages;
using CommunitySite.Data.ViewModels;
using CommunitySite.Services.ImageServices;
using CommunitySite.Services.PostServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace CommunitySite.Components.Dialogs
{
    public partial class UploadPostToGroupDialog
    {
        [Inject] private ISnackbar Snackbar { get; set; } = default!;
        [Inject] private IPostService PostService { get; set; } = default!;
        [Inject] IImageService ImageService { get; set; } = default!;
        [Parameter] public UserViewModel UserViewModel { get; set; } = default!;
        [Parameter] public GroupViewModel GroupViewModel { get; set; } = default!;
        [CascadingParameter] MudDialogInstance MudDialog { get; set; } = default!;
        [CascadingParameter] private Error CommuitySiteError { get; set; } = default!;

        private string description = string.Empty;
        private PhotoViewModel? photoViewModel { get; set; }
        private string imageDataUrl = string.Empty;

        private async Task Submit()
        {
            if (photoViewModel == null && description == string.Empty)
            {
                Snackbar.Add("you have not uploaded anything", Severity.Info);
                return;
            }
            try
            {
                PostViewModel post = new PostViewModel()
                {
                    Userid = UserViewModel.Userid,
                    PostDate = DateTime.Now.ToString(),
                    PostText = description,
                    Groupid = GroupViewModel.Groupid,
                };

                await PostService.UploadPostAsync(post, photoViewModel);
                Snackbar.Add("Successfully posted!", Severity.Success);
                MudDialog.Close(DialogResult.Ok(true));
            }
            catch (Exception ex)
            {
                CommuitySiteError.ErrorHandler(ex);
            }
        }

        private async Task UploadImage(IBrowserFile file)
        {
            photoViewModel = null;
            var imageInByte = await ImageService.ImageToByteArrayConverter(file);
            photoViewModel = new PhotoViewModel()
            {
                Userid = UserViewModel.Userid,
                PhotoSize = file.Size,
                PhotoName = file.Name,
                PhotoType = "Post",
                PhotoInByte = imageInByte,
            };
            ShowImage();
        }

        private void ShowImage()
        {
            if (photoViewModel != null)
            {
                var imagesrc = Convert.ToBase64String(photoViewModel.PhotoInByte!);
                imageDataUrl = string.Format("data:image/jpeg;base64,{0}", imagesrc);
            }
        }

        void Cancel() => MudDialog!.Cancel();
    }
}
