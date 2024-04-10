using CommunitySite.Data.ViewModels;

namespace CommunitySite.Services.UserServices
{
    public interface IUserViewService
    {
        Task CreateUserDialog(UserViewModel user);
    }
}
