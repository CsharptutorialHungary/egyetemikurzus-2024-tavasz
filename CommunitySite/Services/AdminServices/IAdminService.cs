using CommunitySite.Data.ViewModels;

namespace CommunitySite.Services.AdminServices
{
    public interface IAdminService
    {
        /// <summary>
        ///     A felhasz6náló jogosultságát állítja be
        /// </summary>
        /// <param name="userViewModel">a felhasználó, akinek be szeretnénk állítani</param>
        Task UpdateUserRoleAsync(UserViewModel userViewModel);

        /// <summary>
        ///     A felhasználó törléséért felel.
        /// </summary>
        /// <param name="userViewModel">a törlendő felhasználó</param>
        Task DeleteUserAsync(UserViewModel userViewModel);
    }
}
