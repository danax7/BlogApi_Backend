using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using BlogApi.DTO;
using BlogApi.DTO.AuthDTO;
using BlogApi.Entity;
using BlogApi.Helpers;
using BlogApi.Repository.Interface;
using BlogApi.Services.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;

namespace BlogApi.Services;

public class AuthServiceImpl : IAuthService
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;
    private readonly ITokenRepository _tokenRepository;


    public AuthServiceImpl(IUserService userService, ITokenService tokenService, ITokenRepository tokenRepository)
    {
        _userService = userService;
        _tokenService = tokenService;
        _tokenRepository = tokenRepository;
    }

    public async Task<TokenDto> RegisterUser(UserRegisterDto userRegisterDto)
    {
        var newUser = new UserEntity(userRegisterDto);
        await _userService.CreateUser(userRegisterDto);

        var loginCredentials = new LoginCredentialsDto()
        {
            email = newUser.Email,
            password = newUser.Password
        };


        return await LoginUser(loginCredentials);
    }

    public async Task<TokenDto> LoginUser(LoginCredentialsDto userLoginDto)
    {
        var identity = await _userService.GetIdentity(userLoginDto);
        //
        var now = DateTime.UtcNow;

        var accessJwt = new JwtSecurityToken(
            issuer: JwtConfigs.Issuer,
            audience: JwtConfigs.Audience,
            notBefore: now,
            claims: identity.Claims,
            expires: now.AddMinutes(JwtConfigs.AccessLifetime),
            signingCredentials: new SigningCredentials(JwtConfigs.GetSymmetricSecurityAccessKey(),
                SecurityAlgorithms.HmacSha256));

        var accessToken = new JwtSecurityTokenHandler().WriteToken(accessJwt);

        return new TokenDto
        {
            accessToken = accessToken,
        };
    }


    public async Task LogoutUser(String token)
    {
        await _tokenService.BlockAccessToken(token);
    }
}