using AutoMapper;
using CommunitySite.Data.Entity;
using CommunitySite.Data.Context;
using CommunitySite.Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using CommunitySite.Extensions.Exceptions;
using System.Data;

namespace CommunitySite.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IDbContextFactory<CommunitySiteContext> _dbContextFactory;


        public UserService(IMapper mapper, IDbContextFactory<CommunitySiteContext> dbContextFactory)
        {
            _mapper = mapper;
            _dbContextFactory = dbContextFactory;
        }

        public async Task CreateUser(UserViewModel userViewModel)
        {
            try
            {
                using (var dbcx = await _dbContextFactory.CreateDbContextAsync())
                {
                    var siteuser = _mapper.Map<Siteuser>(userViewModel);
                    await dbcx.Siteusers
                        .AddAsync(siteuser);

                    await dbcx.SaveChangesAsync();
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateUserAsync(UserViewModel userViewModel)
        {
            try
            {
                await Task.Run(() => UpdateUser(userViewModel));
            }
            catch
            {
                throw new CommunitySiteException("Something went wrong while update user");
            }
        }

        private void UpdateUser(UserViewModel userViewModel)
        {
            try
            {
                using (var dbcx = _dbContextFactory.CreateDbContext())
                {
                    var user = dbcx.Siteusers
                        .AsNoTracking()
                        .Include(x => x.MessageReceivers)
                        .Include(x => x.MessageSenders)
                        .Include(x => x.Photos)
                        .Include(x => x.Posts)
                        .Include(x => x.Sitecomments)
                        .Include(x => x.Sitegroups)
                        .SingleOrDefault(x => x.Username == userViewModel.Username);

                    if (user == null)
                    {
                        throw new CommunitySiteException("User does not exist!");
                    }

                    user = _mapper.Map<Siteuser>(userViewModel);
                    dbcx.Siteusers.Update(user);
                    dbcx.Entry(user).State = EntityState.Modified;
                    dbcx.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> ExistUser(string userName)
        {
            using (var dbcx = await _dbContextFactory.CreateDbContextAsync())
            {
                return await dbcx.Siteusers
                    .AsNoTracking()
                    .Where(x => x.Username == userName)
                    .AnyAsync();
            }
        }

        public async Task<List<UserViewModel>> GetUsers()
        {
            try
            {
                using (var dbcx = await _dbContextFactory.CreateDbContextAsync())
                {
                    var users = await dbcx.Siteusers
                        .AsNoTracking()
                        .Include(x => x.MessageReceivers)
                        .Include(x => x.MessageSenders)
                        .Include(x => x.Photos)
                        .Include(x => x.Posts)
                        .Include(x => x.Sitecomments)
                        .Include(x => x.Sitegroups)
                        .ToListAsync();

                    return users
                       
                        .Select(_mapper.Map<UserViewModel>)
                        .ToList();
                }
            }
            catch
            {
                throw new CommunitySiteException("Something went wrong while getting users");
            }
        }

        public async Task<UserViewModel> GetUser(string userName)
        {
            try
            {
                using (var dbcx = await _dbContextFactory.CreateDbContextAsync())
                {
                    var user = await dbcx.Siteusers
                        .AsNoTracking()
                        .Include(x => x.MessageReceivers)
                        .Include(x => x.MessageSenders)
                        .Include(x => x.Photos)
                        .Include(x => x.Posts)
                        .Include(x => x.Sitecomments)
                        .Include(x => x.Sitegroups)
                        .Where(x => x.Username == userName)
                        .SingleOrDefaultAsync();
                    return _mapper.Map<UserViewModel>(user);
                }
            }
            catch
            {
                throw new CommunitySiteException("Something went wrong while getting user");
            }
        }

        public async Task<UserViewModel> GetUserByTechnicalId(string userTechnicalId)
        {
            try
            {
                using (var dbcx = await _dbContextFactory.CreateDbContextAsync())
                {
                    var user = await dbcx.Siteusers
                        .AsNoTracking()
                        .Include(x => x.MessageReceivers)
                        .Include(x => x.MessageSenders)
                        .Include(x => x.Photos)
                        .Include(x => x.Posts)
                        .Include(x => x.Sitecomments)
                        .Include(x => x.Sitegroups)
                        .Where(x => x.Usertechnicalname == new Guid(userTechnicalId))
                        .SingleOrDefaultAsync();
                    return _mapper.Map<UserViewModel>(user);
                }
            }
            catch
            {
                throw new CommunitySiteException("Something went wrong while getting user");
            }
        }

        public async Task EnsureUserExist(string userName)
        {
            if(await ExistUser(userName))
            {
                return;
            }

            try
            {
                var userViewModel = new UserViewModel()
                {
                    Username = userName,
                    ShortName = userName.Split('\\').Last(),
                };
                await CreateUser(userViewModel);
            }
            catch
            {
                throw new CommunitySiteException("Something went wrong while create user!");
            }
        }

        public async Task<List<GroupViewModel>> ListUserGroupsAsync(UserViewModel userViewModel)
        {
            try
            {
                using (var dbcx = await _dbContextFactory.CreateDbContextAsync())
                {
                    var groupIds = await dbcx.Managegroups
                        .Where(x => x.Userid == userViewModel.Userid)
                        .OrderByDescending(x => x.JoinDate)
                        .Select(x => x.Groupid)
                        .ToArrayAsync();

                    var groups = await dbcx.Sitegroups
                        .AsNoTracking()
                        .Include(x => x.Posts)
                        .Where(x => groupIds.Contains(x.Groupid))
                        .ToListAsync();

                    return groups.Select(_mapper.Map<GroupViewModel>).ToList();
                }
            }
            catch
            {
                throw new CommunitySiteException("Something went wrong while listing your groups!");
            }
        }

        public async Task<List<GroupViewModel>> ListAllGroupAsync(UserViewModel userViewModel)
        {
            try
            {
                using (var dbcx = await _dbContextFactory.CreateDbContextAsync())
                {
                    var groupIds = await dbcx.Managegroups
                        .AsNoTracking()
                        .Where(x => x.Userid == userViewModel.Userid)
                        .Select(x => x.Groupid)
                        .ToArrayAsync();

                    var allGroup = await dbcx.Sitegroups
                        .ToListAsync();

                    var userGroups = await dbcx.Sitegroups
                        .Where(x => groupIds.Contains(x.Groupid))
                        .ToListAsync();

                    var groups = allGroup.Except(userGroups).ToList();

                    return groups.Select(_mapper.Map<GroupViewModel>).ToList();
                }
            }
            catch
            {
                throw new CommunitySiteException("Something went wrong while listing all groups!");
            }
        }
    }
}
