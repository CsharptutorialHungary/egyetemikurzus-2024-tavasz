using CommunitySite.Data.ViewModels;

namespace CommunitySite.Services.UserViewServices
{
    public interface IUserViewService
    {
        Task CreateUserDialog(UserViewModel user);
    }
}
