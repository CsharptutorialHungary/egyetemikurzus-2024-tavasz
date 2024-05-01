using CommunitySite.Data.ViewModels;
using CommunitySite.Services.ImageServices;
using CommunitySite.Services.PostServices;
using CommunitySite.Services.UserServices;
using CommunitySite.Services.UserViewServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;

namespace CommunitySite.Components.Pages
{
    public partial class Profile
    {
        [Inject] IUserService UserService { get; set; } = default!;
        [Inject] IImageService ImageService { get; set; } = default!;
        [Inject] IPostService PostService { get; set; } = default!;
        [CascadingParameter] private Task<AuthenticationState> authenticationStateTask { get; set; } = default!;
        [Parameter] public string UserTechnicalId { get; set; } = default!;

        private PhotoViewModel profilePhoto = new();
        private UserViewModel loggedUserViewModel = new();
        private UserViewModel currentUserViewModel = new();
        private bool _expanded = false;
        private string imageDataUrl = string.Empty;
        private string authUsername = string.Empty;
        private List<PostViewModel> posts = new();

        protected override async Task OnInitializedAsync()
        {
            var authState = await authenticationStateTask!;
            authUsername = authState.User.Identity?.Name ?? "";
            loggedUserViewModel = await UserService.GetUser(authUsername);
            currentUserViewModel = await UserService.GetUserByTechnicalId(UserTechnicalId);
            await ShowImages();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            currentUserViewModel = await UserService.GetUserByTechnicalId(UserTechnicalId);
        }

        private async void OnExpandCollapseClick()
        {
            _expanded = !_expanded;
            await GetUserPosts();
        }

        private async Task UploadFiles(IBrowserFile file)
        {
            var imageInByte = await ImageService.ImageToByteArrayConverter(file);
            var image = new PhotoViewModel()
            {
                Userid = currentUserViewModel.Userid,
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
            
            profilePhoto = await ImageService.GetUserProfilePicture(currentUserViewModel);
            if(profilePhoto != null)
            {
                var imagesrc = Convert.ToBase64String(profilePhoto.PhotoInByte!);
                imageDataUrl = string.Format("data:image/jpeg;base64,{0}", imagesrc);
            }
        }

        private async Task GetUserPosts()
        {
            posts = await PostService.GetUserPosts(currentUserViewModel);
        }
    }
}
