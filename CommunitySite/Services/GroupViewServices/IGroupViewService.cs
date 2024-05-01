using CommunitySite.Data.ViewModels;

namespace CommunitySite.Services.GroupViewServices
{
    public interface IGroupViewService
    {
        Task CreateGroupDialog(UserViewModel user);
    }
}
