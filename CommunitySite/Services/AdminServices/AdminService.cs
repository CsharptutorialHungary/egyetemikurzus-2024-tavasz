using AutoMapper;
using CommunitySite.Data.Entity;
using CommunitySite.Data.Context;
using CommunitySite.Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using CommunitySite.Extensions.Exceptions;

namespace CommunitySite.Services.AdminServices
{
    public class AdminService : IAdminService
    {
        private readonly IDbContextFactory<CommunitySiteContext> _dbContextFactory;
        private readonly IMapper _mapper;

        public AdminService(IDbContextFactory<CommunitySiteContext> dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task UpdateUserRoleAsync(UserViewModel userViewModel)
        {
            try
            {
                await Task.Run(() => UpdateUserRole(userViewModel));
            }
            catch
            {
                throw new CommunitySiteException("Somethng went wrong while update user!");
            }
        }

        public async Task DeleteUserAsync(UserViewModel userViewModel)
        {
            try
            {
                using (var dbcx = await _dbContextFactory.CreateDbContextAsync())
                {
                    await dbcx.Siteusers
                        .AsNoTracking()
                        .Include(x => x.MessageReceivers)
                        .Include(x => x.MessageSenders)
                        .Include(x => x.Photos)
                        .Include(x => x.Posts)
                        .Include(x => x.Sitecomments)
                        .Include(x => x.Sitegroups)
                        .Where(x => x.Userid == userViewModel.Userid)
                        .ExecuteDeleteAsync();
                }
            }
            catch
            {
                throw new CommunitySiteException("Somethng went wrong while delete user!");
            }
        }

        private void UpdateUserRole(UserViewModel userViewModel)
        {
            try
            {
                using (var dbcx = _dbContextFactory.CreateDbContext())
                {
                    var user = dbcx.Siteusers
                        .AsNoTracking()
                        .SingleOrDefault(user => user.Userid == userViewModel.Userid);

                    var entity = _mapper.Map<Siteuser>(userViewModel);
                    dbcx.Siteusers.Update(entity);
                    dbcx.SaveChanges();
                    dbcx.Entry(entity).State = EntityState.Detached;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
