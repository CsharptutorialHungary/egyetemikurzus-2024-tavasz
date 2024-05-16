using CommunitySite.Data.ViewModels;
using CommunitySite.Services.ImageServices;
using CommunitySite.Services.UserServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;

namespace CommunitySite.Components.Pages
{
    public partial class UploadImagePage
    {
        [Inject] IUserService UserService { get; set; } = default!;
        [Inject] IImageService ImageService { get; set; } = default!;
        [CascadingParameter] private Task<AuthenticationState> authenticationStateTask { get; set; } = default!;

        private List<PhotoViewModel> Photos = new();
        private UserViewModel userViewModel = new();
        private List<string> imageDataUrls = new();

        protected override async Task OnInitializedAsync()
        {
            var authState = await authenticationStateTask!;
            var userName = authState.User.Identity?.Name ?? "";
            userViewModel = await UserService.GetUser(userName);
            await ShowImages();
        }

        private async Task UploadFiles2(IBrowserFile file)
        {
            var imageInByte = await ImageService.ImageToByteArrayConverter(file);
            var image = new PhotoViewModel()
            {
                Userid = userViewModel.Userid,
                PhotoSize = file.Size,
                PhotoName = file.Name,
                PhotoType = "Profile",
                PhotoInByte = imageInByte,
            };
            await ImageService.SaveImageInDatabase(image);
            await ShowImages();
        }

        public async Task ShowImages()
        {
            Photos = await ImageService.GetUserImages(userViewModel);
            imageDataUrls.Clear();
            foreach(var image in Photos)
            {
                var imagesrc = Convert.ToBase64String(image.PhotoInByte!);
                imageDataUrls.Add(string.Format("data:image/jpeg;base64,{0}", imagesrc));
            }
        }

    }
}
