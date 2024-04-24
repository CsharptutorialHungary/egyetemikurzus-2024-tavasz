using CommunitySite.Data.ViewModels;

namespace CommunitySite.Services.AdminViewServices
{
    public interface IAdminViewService
    {
        Task CreateAdminDialog(UserViewModel user);
    }
}
