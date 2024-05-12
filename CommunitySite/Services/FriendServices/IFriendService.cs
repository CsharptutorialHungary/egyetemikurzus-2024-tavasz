using CommunitySite.Data.ViewModels;

namespace CommunitySite.Services.FriendServices
{
    public interface IFriendService
    {
        /// <summary>
        ///     Beállítja adatbázsiban, hogy a felhasználó követi a másik felhasználót
        /// </summary>
        /// <param name="loggedUser">a bejelentkjezett felhasznbáló</param>
        /// <param name="currentUser">a felhasználó akit követni szeretne</param>
        Task FollowUserAsync(UserViewModel loggedUser, UserViewModel currentUser);
    }
}
