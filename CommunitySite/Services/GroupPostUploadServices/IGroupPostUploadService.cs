using CommunitySite.Data.ViewModels;

namespace CommunitySite.Services.GroupPostUploadServices
{
    public interface IGroupPostUploadService
    {
        Task UploadPostToGroupDialog(UserViewModel user, GroupViewModel group);
    }
}
