using AutoMapper;
using CommunitySite.Data.Context;
using CommunitySite.Data.Entity;
using CommunitySite.Data.ViewModels;
using CommunitySite.Extensions.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CommunitySite.Services.FriendServices
{
    public class FriendService : IFriendService
    {
        private readonly IDbContextFactory<CommunitySiteContext> _dbContextFactory;
        private readonly IMapper _mapper;

        public FriendService(IDbContextFactory<CommunitySiteContext> dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task FollowUserAsync(UserViewModel loggedUser, UserViewModel currentUser)
        {
            try
            {
                using (var dbcx = await _dbContextFactory.CreateDbContextAsync())
                {
                    Friend friend = new Friend()
                    {
                        Friendid1 = loggedUser.Userid,
                        Friendid2 = currentUser.Userid,
                        IsFriend = 1,
                        FriendStartDate = DateTime.Now.ToString(),
                    };

                    await dbcx.Friends.AddAsync(friend);
                    await dbcx.SaveChangesAsync();
                }
            }
            catch
            {
                throw new CommunitySiteException("Something went wrong while follow user");
            }
        }
    }
}
