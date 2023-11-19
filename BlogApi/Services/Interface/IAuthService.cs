using BlogApi.DTO;
using BlogApi.DTO.AuthDTO;

namespace BlogApi.Services.Interface;

public interface IAuthService
{
    public Task<TokenDto> RegisterUser(UserRegisterDto userRegisterDto);
    public Task<TokenDto> LoginUser(LoginCredentialsDto userLoginDto);
    // public Task LogoutUser(String token);
    
    
}