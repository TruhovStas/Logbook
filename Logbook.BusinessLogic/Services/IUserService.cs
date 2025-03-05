using EventsWeb.BusinessLogic.Models;
using EventsWeb.BusinessLogic.Models.Users;

namespace EventsWeb.BusinessLogic.Services
{
    public interface IUserService
    {
        public Task<TokenApiModel> Login(UserLoginDto userLoginDto);
        public Task<UserCreateResponseDto> Register(UserCreateDto userCreateDtoDto, CancellationToken cancellationToken);
    }
}
