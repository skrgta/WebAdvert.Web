using AdvertApi.Models;
using AutoMapper;
using WebAdvert.Web.Models;
using WebAdvert.Web.Models.AdvertManagement;
using WebAdvert.Web.Models.Home;
using WebAdvert.Web.ServiceClient;

namespace WebAdvert.Web.Classes
{
    public class WebsiteProfiles: Profile
    {
        public WebsiteProfiles()
        {
            CreateMap<CreateAdvertViewModel, CreateAdvertModel>();

            CreateMap<AdvertModel, Advertisement>().ReverseMap();

            CreateMap<Advertisement, IndexViewModel>()
                .ForMember(
                    dest => dest.Title, src => src.MapFrom(field => field.Title))
                .ForMember(dest => dest.Image, src => src.MapFrom(field => field.FilePath));

            CreateMap<AdvertType, SearchViewModel>()
                .ForMember(
                    dest => dest.Id, src => src.MapFrom(field => field.Id))
                .ForMember(
                    dest => dest.Title, src => src.MapFrom(field => field.Title));
        }
    }
}
