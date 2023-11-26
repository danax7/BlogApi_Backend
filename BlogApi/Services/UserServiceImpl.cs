using System.Security.Authentication;
using System.Security.Claims;
using BlogApi.DTO;
using BlogApi.DTO.AuthDTO;
using BlogApi.Entity;
using BlogApi.Exception;
using BlogApi.Repository.Interface;

namespace BlogApi.Services.Interface;

public class UserServiceImpl : IUserService
{
    private readonly IUserRepository _userRepository;


    public UserServiceImpl(IUserRepository userRepository, ITokenRepository tokenRepository)
    {
        _userRepository = userRepository;
    }

    public async Task CreateUser(UserRegisterDto userRegisterDto)
    {
        var userEntity = new UserEntity(userRegisterDto);

        await _userRepository.CreateUser(userEntity);
    }

    public async Task<ClaimsIdentity> GetIdentity(LoginCredentialsDto userLoginDto)
    {
        Console.WriteLine(userLoginDto.email, userLoginDto.password);
        var user = await _userRepository.GetUserByEmailAndPassword(userLoginDto.email, userLoginDto.password);

        if (user == null)
        {
            throw new InvalidCredentialException("Invalid credentials");
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

    public async Task<UserDto> GetUserProfile(Guid userId)
    {
        var user = await _userRepository.GetUserById(userId);

        if (user == null)
        {
            throw new NotFoundException($"User with id {userId} not found");
        }

        var userDto = new UserDto(user);

        return userDto;
    }

    public async Task UpdateUserProfile(Guid userId, UserEditDto userEditDto)
    {
        var user = await _userRepository.GetUserById(userId);

        if (user == null)
        {
            throw new NotFoundException($"User with id {userId} not found");
        }

        user.UpdateUser(userEditDto);

        await _userRepository.UpdateUser(user);
    }
}