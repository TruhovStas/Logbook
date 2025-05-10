using AutoMapper;
using Logbook.BusinessLogic.Models.Users;
using Logbook.BusinessLogic.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Logbook.DataAccess.Entities;

namespace Logbook.BusinessLogic.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager, ITokenService tokenService,
            IMapper mapper)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<TokenApiModel> Login(UserLoginDto userLoginDto)
        {
            var user = await _userManager.FindByNameAsync(userLoginDto.UserName);

            if (user == null || !await _userManager.CheckPasswordAsync(user, userLoginDto.Password))
            {
                throw new Exception("Invalid username or password.");
            }

            var refreshToken = _tokenService.GenerateRefreshToken();
            var token = new TokenApiModel()
            {
                AccessToken = _tokenService.GenerateAccessToken(await _userManager.GetClaimsAsync(user))
            };
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            await _userManager.UpdateAsync(user);

            return token;
        }

        public async Task<UserCreateResponseDto> Register(UserCreateDto userCreateDto, CancellationToken cancellationToken)
        {
            var existingUser = await _userManager.FindByNameAsync(userCreateDto.UserName);

            if (existingUser != null)
            {
                throw new Exception("Username already exists.");
            }

            var newUser = _mapper.Map<User>(userCreateDto);

            var a =await _userManager.CreateAsync(newUser, userCreateDto.Password);
            var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, newUser.UserName),
                    new Claim(ClaimTypes.NameIdentifier, newUser.Id.ToString()),
                    new Claim(ClaimTypes.UserData, newUser.Fio),
                    new Claim(ClaimTypes.Role, "User")
                };
            var addedUser = await _userManager.FindByNameAsync(newUser.UserName);
            await _userManager.AddClaimsAsync(addedUser, claims);

            return _mapper.Map<UserCreateResponseDto>(newUser);
        }

        public async Task Logout(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = DateTime.Now;

            await _userManager.UpdateAsync(user);
        }

    }
}
