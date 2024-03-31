using CommunitySite.Data.ViewModels;

namespace CommunitySite.Services.UserServices
{
    public interface IUserService
    {
        Task SetUserToDatabase(UserViewModel userViewModel);
        Task<List<UserViewModel>> GetUsers();
        Task<bool> ExistUser(string email);
    }
}
