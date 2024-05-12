using CommunitySite.Data.ViewModels;

namespace CommunitySite.Services.GroupPostUploadServices
{
    public interface IGroupPostUploadService
    {
        /// <summary>
        ///     Poszt létrehozásáért felelős dialógusablak megnyitása egy csopőorton belül
        /// </summary>
        /// <param name="user">a felhasználó, aki hozzá akar adni</param>
        /// <param name="group">a csoport amihez hozzá akar adni</param>
        Task UploadPostToGroupDialog(UserViewModel user, GroupViewModel group);
    }
}
