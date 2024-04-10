using CommunitySite.Data.ViewModels;

namespace CommunitySite.Services.UserServices
{
    public interface IUserService
    {
        Task CreateUser(UserViewModel userViewModel);

        Task<List<UserViewModel>> GetUsers();

        Task<UserViewModel> GetUser(string userName);

        Task<bool> ExistUser(string userName);

        Task EnsureUserExist(string userName);
    }
}
