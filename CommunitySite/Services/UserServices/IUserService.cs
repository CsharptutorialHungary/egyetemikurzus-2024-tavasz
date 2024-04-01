using CommunitySite.Data.ViewModels;

namespace CommunitySite.Services.UserServices
{
    public interface IUserService
    {
        Task SetUserToDatabase(UserViewModel userViewModel);

        Task<List<UserViewModel>> GetUsers();

        Task<UserViewModel> GetUser(string email);

        Task<bool> ExistUser(string email);

        Task<bool> ExistUserEmailAndPasswordInDatabase(string email, string password);
    }
}
