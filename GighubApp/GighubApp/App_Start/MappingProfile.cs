using AutoMapper;
using GighubApp.Dtos;
using GighubApp.Models;

namespace GighubApp
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            Mapper.CreateMap<ApplicationUser, UserDto>();
            Mapper.CreateMap<Gig, GigDto>();
            Mapper.CreateMap<Notification, NotificationDto>();

        }
    }
}