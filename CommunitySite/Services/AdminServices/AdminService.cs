using AutoMapper;
using CommunitySite.Data.Entities;
using CommunitySite.Data.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CommunitySite.Services.AdminServices
{
    public class AdminService : IAdminService
    {
        private readonly IDbContextFactory<ModelContext> _dbContextFactory;
        private readonly IMapper _mapper;

        public AdminService(IDbContextFactory<ModelContext> dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<bool> UpdateUserRoleAsync(UserViewModel userViewModel)
        {
            return await Task.Run(() => UpdateUserRole(userViewModel));
        }

        public async Task<bool> DeleteUserAsync(UserViewModel userViewModel)
        {
            try
            {
                using (var dbcx = await _dbContextFactory.CreateDbContextAsync())
                {
                    await dbcx.Siteusers.Where(x => x.Userid == userViewModel.Userid).ExecuteDeleteAsync();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        private bool UpdateUserRole(UserViewModel userViewModel)
        {
            if (userViewModel == null || userViewModel.Userid == 0 || userViewModel.Username == null)
            {
                return false;
            }

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

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
