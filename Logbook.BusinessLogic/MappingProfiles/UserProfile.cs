using AutoMapper;
using Logbook.BusinessLogic.Models.Users;
using Logbook.DataAccess.Entities;

namespace Logbook.BusinessLogic.MappingProfiles
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
