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
                //.ForMember(o => o.Email, x => x.MapFrom(y => y.Email));
        }
    }
}
