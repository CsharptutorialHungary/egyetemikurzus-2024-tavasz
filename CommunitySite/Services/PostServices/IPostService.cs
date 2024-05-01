using CommunitySite.Data.ViewModels;

namespace CommunitySite.Services.PostServices
{
    public interface IPostService
    {
        Task UploadPostAsync(PostViewModel postViewModel, PhotoViewModel photoViewModel);

        Task<List<PostViewModel>> GetUserPosts(UserViewModel userViewModel);

        Task<List<PostViewModel>> GetPostsInGroup(GroupViewModel groupViewModel);

        Task<List<PostViewModel>> GetFollowedUserPostsAsync(UserViewModel userViewModel);
    }
}
