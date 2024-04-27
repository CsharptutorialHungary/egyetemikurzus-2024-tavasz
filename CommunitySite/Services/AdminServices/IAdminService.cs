using CommunitySite.Data.ViewModels;

namespace CommunitySite.Services.AdminServices
{
    public interface IAdminService
    {
        Task UpdateUserRoleAsync(UserViewModel userViewModel);

        Task DeleteUserAsync(UserViewModel userViewModel);
    }
}
