using AutoMapper;
using CommunitySite.Data.Entity;
using CommunitySite.Data.ViewModels;

namespace CommunitySite.Extensions.Mapper
{
    /// <summary>
    /// Automapper osztály
    /// </summary>
    public class MapperProfile : Profile
    {
        //Nem volt időm átírni Mapperly-re, tudom azzal sokkal optimálisabb lenne.
        public MapperProfile()
        {
            CreateMap<Siteuser, UserViewModel>().ReverseMap();
            CreateMap<Photo, PhotoViewModel>().ReverseMap();
            CreateMap<Post, PostViewModel>().ReverseMap();
            CreateMap<Sitegroup, GroupViewModel>().ReverseMap();
                
        }
    }
}
