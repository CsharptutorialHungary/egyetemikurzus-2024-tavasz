﻿using AutoMapper;
using CommunitySite.Data.Entities;
using CommunitySite.Data.Util;
using CommunitySite.Data.ViewModels;
using CommunitySite.Extensions.Validators;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;

namespace CommunitySite.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IDbContextFactory<ModelContext> _dbContextFactory;

        public UserService(IMapper mapper, IDbContextFactory<ModelContext> dbContextFactory)
        {
            _mapper = mapper;
            _dbContextFactory = dbContextFactory;
        }

        public void SetPermission(ref UserViewModel model, PermissionEnum permission)
        {
            model.PermissionId = (int)permission;
        }

        public async Task CreateUser(UserViewModel userViewModel)
        {
            if(userViewModel.Username is null || await ExistUser(userViewModel.Username))
            {
                return;
            }

            SetPermission(ref userViewModel, PermissionEnum.DEFAULT);

            using (var dbcx = await _dbContextFactory.CreateDbContextAsync())
            {
                var siteuser = _mapper.Map<Siteuser>(userViewModel);
                await dbcx.Siteusers.AddAsync(siteuser);
                await dbcx.SaveChangesAsync();
            }
        }

        public async Task<bool> UpdateUserAsync(UserViewModel userViewModel)
        {
            return await Task.Run(() => UpdateUser(userViewModel));
        }

        private bool UpdateUser(UserViewModel userViewModel)
        {
            if(userViewModel == null || userViewModel.Username == null)
            {
                return false;
            }

            try
            {
                using (var dbcx =  _dbContextFactory.CreateDbContext())
                {
                    var res = dbcx.Siteusers.AsNoTracking().SingleOrDefault(x => x.Username == userViewModel.Username);
                    if (res == null)
                    {
                        return false;
                    }

                    var entity = _mapper.Map<Siteuser>(userViewModel);
                    dbcx.Siteusers.Update(entity);
                    dbcx.SaveChanges();
                    dbcx.Entry(entity).State = EntityState.Detached;
                }

                return true;
            }
            catch (OracleException e)
            {
                //TODO: logger
                return false;
            }
        }

        public async Task<bool> ExistUser(string userName)
        {
            using (var dbcx = await _dbContextFactory.CreateDbContextAsync())
            {
                return await dbcx.Siteusers.AsNoTracking().Where(x => x.Username == userName).AnyAsync();
            }
        }

        public async Task<List<UserViewModel>> GetUsers()
        {
            using (var dbcx = await _dbContextFactory.CreateDbContextAsync())
            {
                var users = await dbcx.Siteusers.AsNoTracking().ToListAsync();
                return users.Select(_mapper.Map<UserViewModel>).ToList();
            }
        }

        public async Task<UserViewModel> GetUser(string userName)
        {
            using (var dbcx = await _dbContextFactory.CreateDbContextAsync())
            {
                var user = await dbcx.Siteusers.AsNoTracking().Where(x => x.Username == userName).SingleOrDefaultAsync();
                return _mapper.Map<UserViewModel>(user);
            }
        }

        public async Task EnsureUserExist(string userName)
        {
            if(await ExistUser(userName))
            {
                return;
            }

            var userViewModel = new UserViewModel()
            {
                Username = userName,
                ShortName = userName.Split('\\').Last(),
            };

            await CreateUser(userViewModel);
        }
    }
}
