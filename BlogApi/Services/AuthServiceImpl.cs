using BlogApi.DTO;
using BlogApi.DTO.AuthDTO;
using BlogApi.Entity;
using BlogApi.Repository.Interface;
using BlogApi.Services.Interface;
using Microsoft.AspNetCore.Http.HttpResults;

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
        // var user = await _userService.GetUserByEmail(userRegisterDto.email);
        // if (user != null)
        // {
        //     throw new System.Exception("User already exists.");
        // }
        var newUser = new UserEntity(userRegisterDto);
        await _userService.CreateUser(userRegisterDto);
        
        var loginCredentials = new LoginCredentialsDto()
        {
            email = newUser.Email,
            password = newUser.Password
        };

        return null;
        // return await LoginUser(loginCredentials);
    }
    
    
}