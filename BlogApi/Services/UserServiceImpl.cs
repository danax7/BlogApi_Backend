using System.Security.Claims;
using BlogApi.DTO;
using BlogApi.DTO.AuthDTO;
using BlogApi.Entity;
using BlogApi.Repository.Interface;

namespace BlogApi.Services.Interface;

public class UserServiceImpl : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenRepository _tokenRepository;

    public UserServiceImpl(IUserRepository userRepository, ITokenRepository tokenRepository)
    {
        _userRepository = userRepository;
        _tokenRepository = tokenRepository;
    }

    public async Task CreateUser(UserRegisterDto userRegisterDto)
    {
        var userEntity = new UserEntity(userRegisterDto);

        await _userRepository.CreateUser(userEntity);
    }

    public async Task<ClaimsIdentity> GetIdentity(LoginCredentialsDto userLoginDto)
    {
        var user = await _userRepository.GetUserByEmailAndPassword(userLoginDto.email, userLoginDto.password);

        if (user == null)
        {
            throw null;
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id.ToString()),
        };

        return new ClaimsIdentity(
            claims,
            "Token",
            ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);
    }
}