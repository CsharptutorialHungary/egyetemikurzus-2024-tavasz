using CommunitySite.Data.ViewModels;

namespace CommunitySite.Services.FriendServices
{
    public interface IFriendService
    {
        Task FollowUserAsync(UserViewModel loggedUser, UserViewModel currentUser);
    }
}
