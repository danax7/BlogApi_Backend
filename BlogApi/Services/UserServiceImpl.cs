using BlogApi.DTO;
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
}