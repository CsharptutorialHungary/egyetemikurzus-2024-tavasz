using CommunitySite.Data.ViewModels;
using Microsoft.AspNetCore.Components.Forms;

namespace CommunitySite.Services.ImageServices
{
    public interface IImageService
    {
        /// <summary>
        ///     Byte tömbbé konvertálja a fotót.
        /// </summary>
        /// <param name="imageFile">a feltöltött kép</param>
        /// <returns>byte tömb, ami a fotót reprezentálja</returns>
        Task<byte[]> ImageToByteArrayConverter(IBrowserFile imageFile);

        /// <summary>
        ///     fotó mentése adatbázisban
        /// </summary>
        /// <param name="photoViewModel">a mentendő fotó</param>
        Task SaveImageInDatabase(PhotoViewModel photoViewModel);

        /// <summary>
        ///     Lekéri a felhasználó fotóit
        /// </summary>
        /// <param name="userViewModel">a felhasználó</param>
        /// <returns>a fotókat tartalmazó lista</returns>
        Task<List<PhotoViewModel>> GetUserImages(UserViewModel userViewModel);

        /// <summary>
        ///     Lekéri a felhasználó profil képét
        /// </summary>
        /// <param name="userViewModel">a felhasználó</param>
        /// <returns>a felhasználó profil képe</returns>
        Task<PhotoViewModel> GetUserProfilePicture(UserViewModel userViewModel);
    }
}
