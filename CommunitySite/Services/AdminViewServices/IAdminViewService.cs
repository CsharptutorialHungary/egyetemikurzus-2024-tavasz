using CommunitySite.Data.ViewModels;

namespace CommunitySite.Services.AdminViewServices
{
    public interface IAdminViewService
    {
        /// <summary>
        ///     Az admin dialógusablak megnyitását végzi el.
        /// </summary>
        /// <param name="user"> a felhasználó</param>
        Task CreateAdminDialog(UserViewModel user);
    }
}
