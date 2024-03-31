using AutoMapper;
using CommunitySite.Data.Entities;
using CommunitySite.Data.ViewModels;

namespace CommunitySite.Extensions.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Siteuser, UserViewModel>().ReverseMap();
        }
    }
}
