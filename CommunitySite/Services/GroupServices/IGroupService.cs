using CommunitySite.Data.ViewModels;

namespace CommunitySite.Services.GroupServices
{
    public interface IGroupService
    {
        Task CreateGroupAsync(GroupViewModel groupViewModel);

        Task<bool> IsUsermemberOfGroup(string technicalId, UserViewModel userViewModel);

        Task<GroupViewModel> GetGroupByTechnicalId(string technicalId);

        Task AddUserToGroupAsync(UserViewModel userViewModel, GroupViewModel groupViewModel);

        Task<int> CountGroupMembersAsync(GroupViewModel groupViewModel);
    }
}
