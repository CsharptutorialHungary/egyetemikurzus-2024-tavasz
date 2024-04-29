using AutoMapper;
using CommunitySite.Data.Context;
using CommunitySite.Data.Entity;
using CommunitySite.Data.ViewModels;
using CommunitySite.Extensions.Exceptions;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using System.Data.SqlClient;

namespace CommunitySite.Services.PostServices
{
    public class PostService : IPostService
    {

        private readonly IDbContextFactory<CommunitySiteContext> _dbContextFactory;
        private readonly IMapper _mapper;

        public PostService(IDbContextFactory<CommunitySiteContext> dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task UploadPostAsync(PostViewModel postViewModel, PhotoViewModel photoViewModel)
        {
            try
            {
                using(var dbcx = await _dbContextFactory.CreateDbContextAsync())
                {
                    if(photoViewModel != null)
                    {
                        var photo = _mapper.Map<Photo>(photoViewModel);
                        await dbcx.AddAsync(photo);
                        await dbcx.SaveChangesAsync();
                        postViewModel.Photoid = photo.Photoid;
                    }

                    var post = _mapper.Map<Post>(postViewModel);
                    await dbcx.AddAsync(post);
                    await dbcx.SaveChangesAsync();
                }
            }
            catch
            {
                throw new CommunitySiteException("Something went wrong while saving post!");
            }
        }

        public async Task<List<PostViewModel>> GetUserPosts(UserViewModel userViewModel)
        {
            try
            {
                using (var dbcx = await _dbContextFactory.CreateDbContextAsync())
                {
                    var posts = await dbcx.Posts
                        .AsNoTracking()
                        .Include(x => x.Photo)
                        .Where(x => x.Userid == userViewModel.Userid)
                        .OrderByDescending(x => x.PostDate)
                        .ToListAsync();

                    return posts.Select(_mapper.Map<PostViewModel>).ToList();
                }
            }
            catch
            {
                throw new CommunitySiteException("Something went wrong while getting post!");
            }
        }

    }
}
