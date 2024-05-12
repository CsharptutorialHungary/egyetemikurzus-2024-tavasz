using CommunitySite.Data.ViewModels;

namespace CommunitySite.Services.UserViewServices
{
    public interface IUserViewService
    {
        /// <summary>
        ///     felhasználó módosításához szükséges dialógusablak megnyitása
        /// </summary>
        /// <param name="user">A módosítandó felhasználó</param>
        Task UpdateUserDialog(UserViewModel user);
    }
}
