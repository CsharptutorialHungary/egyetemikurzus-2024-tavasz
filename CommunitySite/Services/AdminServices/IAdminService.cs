using CommunitySite.Data.ViewModels;

namespace CommunitySite.Services.AdminServices
{
    public interface IAdminService
    {
        Task<bool> UpdateUserRoleAsync(UserViewModel userViewModel);
        Task<bool> DeleteUserAsync(UserViewModel userViewModel);
    }
}
