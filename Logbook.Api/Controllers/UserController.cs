using EventsWeb.BusinessLogic.Models;
using EventsWeb.BusinessLogic.Models.Users;
using EventsWeb.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventsWeb.Api.Controllers
{
    [Route("/api/auth")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenApiModel>> Login([FromForm] UserLoginDto userLoginDto)
        {
            return Ok(await _userService.Login(userLoginDto));
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserCreateResponseDto>> Register([FromForm] UserCreateDto userCreateDto,
            CancellationToken cancellationToken)
        {
            return Ok(await _userService.Register(userCreateDto, cancellationToken));
        }
    }
}