using CommunitySite.Data.ViewModels;

namespace CommunitySite.Services.PostViewService
{
    public interface IPostViewService
    {
        Task CreateUploadPostDialog(UserViewModel user);
    }
}
