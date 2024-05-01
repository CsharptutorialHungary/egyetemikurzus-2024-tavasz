using AutoMapper;
using CommunitySite.Data.Context;
using CommunitySite.Data.Entity;
using CommunitySite.Data.ViewModels;
using CommunitySite.Extensions.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CommunitySite.Services.GroupServices
{
    public class GroupService : IGroupService
    {
        private readonly IDbContextFactory<CommunitySiteContext> _dbContextFactory;
        private readonly IMapper _mapper;

        public GroupService(IDbContextFactory<CommunitySiteContext> dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task CreateGroupAsync(GroupViewModel groupViewModel)
        {
            try
            {
                using (var dbcx = await _dbContextFactory.CreateDbContextAsync())
                {
                    var group = _mapper.Map<Sitegroup>(groupViewModel);
                    await dbcx.Sitegroups.AddAsync(group);
                    await dbcx.SaveChangesAsync();

                    await AddMemberToGroup(group);
                }
            }
            catch
            {
                throw new CommunitySiteException("Something went wrong while creating group!");
            }
        }

        private async Task AddMemberToGroup(Sitegroup sitegroup)
        {
            try
            {
                using (var dbcx = await _dbContextFactory.CreateDbContextAsync())
                {
                    var memberInGroup = new Managegroup()
                    {
                        Groupid = sitegroup.Groupid,
                        Userid = sitegroup.Ownerid,
                        JoinDate = DateTime.Now.ToString(),
                    };

                    await dbcx.Managegroups.AddAsync(memberInGroup);
                    await dbcx.SaveChangesAsync();
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> CountGroupMembersAsync(GroupViewModel groupViewModel)
        {
            try
            {
                using (var dbcx = await _dbContextFactory.CreateDbContextAsync())
                {
                    return await dbcx.Managegroups
                        .Where(x => x.Groupid == groupViewModel.Groupid)
                        .CountAsync();
                }
            }
            catch
            {
                throw new CommunitySiteException("Something went wrong while counting members!");
            }
        }

        public async Task<bool> IsUsermemberOfGroup(string technicalId, UserViewModel userViewModel)
        {
            try
            {
                using (var dbcx = await _dbContextFactory.CreateDbContextAsync())
                {
                    var group = await GetGroupByTechnicalId(technicalId);

                    return await dbcx.Managegroups
                        .Where(x => x.Groupid == group.Groupid && x.Userid == userViewModel.Userid)
                        .AnyAsync();
                }
            }
            catch
            {
                throw new CommunitySiteException("Something went wrong while checking member!");
            }
        }

        public async Task<GroupViewModel> GetGroupByTechnicalId(string technicalId)
        {
            try
            {
                using (var dbcx = await _dbContextFactory.CreateDbContextAsync())
                {
                    var group = await dbcx.Sitegroups
                        .AsNoTracking()
                        .Include(x => x.Posts)
                        .Where(x => x.Grouptechnicalname == new Guid(technicalId))
                        .SingleOrDefaultAsync();

                    return _mapper.Map<GroupViewModel>(group);
                }
            }
            catch
            {
                throw new CommunitySiteException("Something went wrong while getting group!");
            }
        }

        public async Task AddUserToGroupAsync(UserViewModel userViewModel, GroupViewModel groupViewModel)
        {
            try
            {
                using (var dbcx = await _dbContextFactory.CreateDbContextAsync())
                {
                    var joinGroup = new Managegroup()
                    {
                        Groupid = groupViewModel.Groupid,
                        Userid = userViewModel.Userid,
                        JoinDate = DateTime.Now.ToString()
                    };

                    await dbcx.Managegroups.AddAsync(joinGroup);
                    await dbcx.SaveChangesAsync();
                }
            }
            catch
            {
                throw new CommunitySiteException("Something went wrong while joining group!");
            }
        }
    }
}
