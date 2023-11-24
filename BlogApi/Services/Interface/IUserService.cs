using System.Security.Claims;
using BlogApi.DTO;
using BlogApi.DTO.AuthDTO;

namespace BlogApi.Repository.Interface;

public interface IUserService
{
    Task CreateUser(UserRegisterDto userRegisterDto);
    Task<ClaimsIdentity> GetIdentity(LoginCredentialsDto userLoginDto);

    Task<UserDto> GetUserProfile(Guid userId);
    // Task UpdateUserProfile(Guid userId, UserEditDto userEditDto);
}