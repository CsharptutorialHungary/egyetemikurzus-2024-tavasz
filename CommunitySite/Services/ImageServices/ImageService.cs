using AutoMapper;
using CommunitySite.Data.Entity;
using CommunitySite.Data.Context;
using CommunitySite.Data.ViewModels;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using System.Text;
using CommunitySite.Extensions.Exceptions;

namespace CommunitySite.Services.ImageServices
{
    public class ImageService : IImageService
    {

        private readonly IDbContextFactory<CommunitySiteContext> _dbContextFactory;
        private readonly IMapper _mapper;

        public ImageService(IDbContextFactory<CommunitySiteContext> dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<byte[]> ImageToByteArrayConverter(IBrowserFile imageFile)
        {
            try
            {
                var path = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
                await using var fs = new FileStream(path, FileMode.Create);
                await imageFile.OpenReadStream(imageFile.Size).CopyToAsync(fs);
                var byteArray = new byte[imageFile.Size];
                fs.Position = 0;
                await fs.ReadAsync(byteArray);
                fs.Close();
                File.Delete(path);
                return byteArray;
            }
            catch
            {
                throw new CommunitySiteException("Something went wrong!");
            }
        }

        public async Task SaveImageInDatabase(PhotoViewModel photoViewModel)
        {
            try
            {
                using (var dbcx = await _dbContextFactory.CreateDbContextAsync())
                {
                    var profImage = await dbcx.Photos.Where(x => x.Userid == photoViewModel.Userid && x.PhotoType == "Profile").FirstOrDefaultAsync();
                    if(profImage != null)
                    {
                        await DeletePhoto(profImage);
                    }

                    var image = _mapper.Map<Photo>(photoViewModel);
                    await dbcx
                        .AddAsync(image);

                    await dbcx.SaveChangesAsync();
                }
            }
            catch
            {
                throw new CommunitySiteException("Something went wrong while saving your image!");
            }
        }

        private async Task DeletePhoto(Photo photo)
        {
            try
            {
                using (var dbcx = await _dbContextFactory.CreateDbContextAsync())
                {
                    await dbcx.Photos.Where(x => x.Photoid == photo.Photoid).ExecuteDeleteAsync();
                    await dbcx.SaveChangesAsync();
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<PhotoViewModel>> GetUserImages(UserViewModel userViewModel)
        {
            try
            {
                using (var dbcx = await _dbContextFactory.CreateDbContextAsync())
                {
                    var images = await dbcx.Photos
                        .Include(x => x.User)
                        .Include(x => x.Posts)
                        .AsNoTracking()
                        .ToListAsync();
                    return images.Where(x => x.Userid == userViewModel.Userid).Select(_mapper.Map<PhotoViewModel>).ToList();
                }
            }
            catch
            {
                throw new CommunitySiteException("Something went wrong while listing your images!");
            }
        }

        public async Task<PhotoViewModel> GetUserProfilePicture(UserViewModel userViewModel)
        {
            try
            {
                using (var dbcx = await _dbContextFactory.CreateDbContextAsync())
                {
                    var user = await dbcx.Siteusers
                        .Include(x => x.Photos)
                        .Where(x => x.Userid == userViewModel.Userid)
                        .SingleOrDefaultAsync();

                    var profilePic = user!.Photos.Where(x => x.PhotoType == "Profile").SingleOrDefault();
                    return _mapper.Map<PhotoViewModel>(profilePic);
                }
            }
            catch
            {
                return null!;
            }
        }
    }
}
