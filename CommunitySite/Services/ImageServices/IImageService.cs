using CommunitySite.Data.ViewModels;
using Microsoft.AspNetCore.Components.Forms;

namespace CommunitySite.Services.ImageServices
{
    public interface IImageService
    {
        Task<byte[]> ImageToByteArrayConverter(IBrowserFile imageFile);
        Task SaveImageInDatabase(PhotoViewModel photoViewModel);
        Task<List<PhotoViewModel>> GetUserImages(UserViewModel userViewModel);
    }
}
