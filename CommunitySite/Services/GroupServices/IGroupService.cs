using CommunitySite.Data.ViewModels;

namespace CommunitySite.Services.GroupServices
{
    public interface IGroupService
    {
        /// <summary>
        ///     CSoport létrehozása adatbázisban
        /// </summary>
        /// <param name="groupViewModel">a létrehozandó csoport</param>
        Task CreateGroupAsync(GroupViewModel groupViewModel);

        /// <summary>
        ///     Lekéri, hogy az adott felhasználó tagja-e a csoportnak
        /// </summary>
        /// <param name="technicalId">A csoport technikai azonosítója</param>
        /// <param name="userViewModel">a felhasználó</param>
        /// <returns>
        ///     true: ha tagja
        ///     false: minden más esetben
        /// </returns>
        Task<bool> IsUsermemberOfGroup(string technicalId, UserViewModel userViewModel);

        /// <summary>
        ///     Lekéri a csoportot technikai azonosító alapján
        /// </summary>
        /// <param name="technicalId">a csoport technikai azonosítója</param>
        /// <returns>a csoport</returns>
        Task<GroupViewModel> GetGroupByTechnicalId(string technicalId);

        /// <summary>
        ///     Hozzáadja a felhasználót a csoporthoz
        /// </summary>
        /// <param name="userViewModel">a felhasználó</param>
        /// <param name="groupViewModel">a csoport</param>
        Task AddUserToGroupAsync(UserViewModel userViewModel, GroupViewModel groupViewModel);
        
        /// <summary>
        ///     Lkeéri, hogy az adott csoportban hány darab felhasználó van benne
        /// </summary>
        /// <param name="groupViewModel">a csoport</param>
        /// <returns>a darabszám</returns>
        Task<int> CountGroupMembersAsync(GroupViewModel groupViewModel);
    }
}
