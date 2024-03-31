using AutoMapper;
using CommunitySite.Data.Entities;
using CommunitySite.Data.Util;
using CommunitySite.Data.ViewModels;
using CommunitySite.Extensions.Validators;
using Microsoft.EntityFrameworkCore;

namespace CommunitySite.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;

        public UserService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void SetPermission(ref UserViewModel model, PermissionEnum permission)
        {
            model.PermissionId = (int)permission;
        }

        public async Task SetUserToDatabase(UserViewModel userViewModel)
        {
            SetPermission(ref userViewModel, PermissionEnum.DEFAULT);
            userViewModel.Passwords = PasswordHasher.EncodePasswordToBase64(userViewModel.Passwords!);

            Siteuser siteuser = _mapper.Map<Siteuser>(userViewModel);
           
            using (var dbcx = new ModelContext())
            {
                await dbcx.Siteusers.AddAsync(siteuser);
                await dbcx.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistUserEmailAndPasswordInDatabase(string email, string password)
        {
            using (var dbcx = new ModelContext())
            {
                var result = await dbcx.Siteusers.AsNoTracking().Where(x => x.Email == email && x.Passwords == password).AnyAsync();
                return !result;
            }
        } 

        public async Task<bool> ExistUser(string email)
        {
            using (var dbcx = new ModelContext())
            {
                var result = await dbcx.Siteusers.AsNoTracking().Where(x => x.Email == email).AnyAsync();
                return !result;
            }
        }

        public async Task<List<UserViewModel>> GetUsers()
        {
            using (var dbcx = new ModelContext())
            {
                var users = await dbcx.Siteusers.ToListAsync();
                return users.Select(_mapper.Map<UserViewModel>).ToList();
            }
        }
    }
}
