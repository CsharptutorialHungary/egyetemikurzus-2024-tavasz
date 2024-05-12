using CommunitySite.Data.ViewModels;

namespace CommunitySite.Services.UserServices
{
    public interface IUserService
    {
        /// <summary>
        ///     Felhasználó létrehozása adatbázisban
        /// </summary>
        /// <param name="userViewModel">a létrehozandó felhasználó</param>
        Task CreateUser(UserViewModel userViewModel);

        /// <summary>
        ///     Felhasználó adatainak módosítása adatbázisban
        /// </summary>
        /// <param name="userViewModel">a módosítandó felhasználó</param>
        Task UpdateUserAsync(UserViewModel userViewModel);

        /// <summary>
        ///     felhasználók lekérése
        /// </summary>
        /// <returns>a felhjasználókat tartalmazó lista</returns>
        Task<List<UserViewModel>> GetUsers();

        /// <summary>
        ///     felhasználó lekérdezése felhasználónév alapján
        /// </summary>
        /// <param name="userName">felhasználónév</param>
        /// <returns>a felhasználót reprezentáló objektum</returns>
        Task<UserViewModel> GetUser(string userName);

        /// <summary>
        ///     ellenőrzi, hogy a felhasználó szerpeel-e már az adatbázisban
        /// </summary>
        /// <param name="userName">felhasználónév</param>
        /// <returns>
        ///     true: ha létezik
        ///     false: minden más esetben
        /// </returns>
        Task<bool> ExistUser(string userName);

        /// <summary>
        ///     ellenőrzi, hogy létezik-e már a felhasználó
        ///     Ha igen: nem törénik semmi
        ///     Ha nem: létrehozza
        /// </summary>
        /// <param name="userName">felhasználónév</param>
        Task EnsureUserExist(string userName);

        /// <summary>
        ///     kilistázza, hogy a felhasználó melyik csoportoknak a tagja
        /// </summary>
        /// <param name="userViewModel">a felhasználó</param>
        /// <returns>A felhasználó csoportjainak a listája</returns>
        Task<List<GroupViewModel>> ListUserGroupsAsync(UserViewModel userViewModel);

        /// <summary>
        ///     Kilistázza az összes olyan csoportot, aminek a felhasználó nem a tagja
        /// </summary>
        /// <param name="userViewModel"> a felhasználó</param>
        /// <returns>A csoportok listája</returns>
        Task<List<GroupViewModel>> ListAllGroupAsync(UserViewModel userViewModel);

        /// <summary>
        ///     Lekérdezi a felhasználót a technikai azonosítója alapján
        /// </summary>
        /// <param name="userTechnicalId">felhasználó technikai azonosítója</param>
        /// <returns>a felhasználó</returns>
        Task<UserViewModel> GetUserByTechnicalId(string userTechnicalId);
    }
}
