using CommunitySite.Data.ViewModels;

namespace CommunitySite.Services.PostViewService
{
    public interface IPostViewService
    {
        /// <summary>
        ///     Poszt létrehozásához szükséges dialógusablak megnyitása
        /// </summary>
        /// <param name="user">a felhasználó, aki a posztot létrehozza</param>
        Task CreateUploadPostDialog(UserViewModel user);
    }
}
