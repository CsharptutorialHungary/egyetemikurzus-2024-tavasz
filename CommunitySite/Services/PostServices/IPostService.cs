using CommunitySite.Data.ViewModels;

namespace CommunitySite.Services.PostServices
{
    public interface IPostService
    {
        /// <summary>
        ///     Poszt létrehozása adatbázisban
        /// </summary>
        /// <param name="postViewModel">a létrehozandó poszt</param>
        /// <param name="photoViewModel">a létrehozó felhasználó</param>
        Task UploadPostAsync(PostViewModel postViewModel, PhotoViewModel photoViewModel);

        /// <summary>
        ///     Lekérdezi azokat a posztokat, amit a felhasználó hozott létre
        /// </summary>
        /// <param name="userViewModel">a felhasználó</param>
        /// <returns>a posztok listája</returns>
        Task<List<PostViewModel>> GetUserPosts(UserViewModel userViewModel);

        /// <summary>
        ///     Lekérdezi a csoportban lévő posztokat
        /// </summary>
        /// <param name="groupViewModel">a csoport miből le szeretnénk kérdezni</param>
        /// <returns>a posztok listája</returns>
        Task<List<PostViewModel>> GetPostsInGroup(GroupViewModel groupViewModel);

        /// <summary>
        ///     lekéri azokat a nposztokat, amiket azok a felhasználók raktak ki, akiket követ a felhasználó.
        /// </summary>
        /// <param name="userViewModel">a felhasználó</param>
        /// <returns>a posztok listája</returns>
        Task<List<PostViewModel>> GetFollowedUserPostsAsync(UserViewModel userViewModel);
    }
}
