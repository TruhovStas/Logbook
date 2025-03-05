using AutoMapper;
using EventsWeb.BusinessLogic.Models.Users;
using EventsWeb.DataAccess.Entities;

namespace EventsWeb.BusinessLogic.MappingProfiles
{
    public class UserProfile : Profile, IMappingProfile
    {
        public UserProfile() 
        {
            CreateMap<User, UserCreateResponseDto>();
            CreateMap<UserCreateDto, User>();
            CreateMap<UserLoginDto, User>();
        }
    }
}
