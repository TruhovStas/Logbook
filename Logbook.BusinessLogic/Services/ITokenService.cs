﻿using EventsWeb.BusinessLogic.Models;
using System.Security.Claims;

namespace EventsWeb.BusinessLogic.Services
{
    public interface ITokenService
    {
        public string GenerateAccessToken(IEnumerable<Claim> claims);
        public string GenerateRefreshToken();
        public Task<TokenApiModel> RefreshAsync(TokenApiModel tokenApiModel);
        public Task RevokeAsync(TokenApiModel tokenApiModel);
    }
}
