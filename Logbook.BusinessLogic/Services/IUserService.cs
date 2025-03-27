using Logbook.BusinessLogic.Models;
using Logbook.BusinessLogic.Models.Users;

namespace Logbook.BusinessLogic.Services
{
    public interface IUserService
    {
        public Task<TokenApiModel> Login(UserLoginDto userLoginDto);
        public Task<UserCreateResponseDto> Register(UserCreateDto userCreateDtoDto, CancellationToken cancellationToken);
    }
}
