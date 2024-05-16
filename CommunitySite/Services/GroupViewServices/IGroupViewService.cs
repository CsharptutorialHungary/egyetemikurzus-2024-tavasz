using CommunitySite.Data.ViewModels;

namespace CommunitySite.Services.GroupViewServices
{
    public interface IGroupViewService
    {
        /// <summary>
        ///     A csoport létrehozásáért felelős dialógusablak megnyitása
        /// </summary>
        /// <param name="user">a felhasználó aki létre akarja hozni a csoportot</param>
        Task CreateGroupDialog(UserViewModel user);
    }
}
