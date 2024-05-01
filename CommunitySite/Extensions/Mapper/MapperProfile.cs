using AutoMapper;
using CommunitySite.Data.Entity;
using CommunitySite.Data.ViewModels;

namespace CommunitySite.Extensions.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Siteuser, UserViewModel>().ReverseMap();
            CreateMap<Photo, PhotoViewModel>().ReverseMap();
            CreateMap<Post, PostViewModel>().ReverseMap();
            CreateMap<Sitegroup, GroupViewModel>().ReverseMap();
                
        }
    }
}
