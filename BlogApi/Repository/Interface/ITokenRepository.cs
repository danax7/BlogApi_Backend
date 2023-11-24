using BlogApi.Entity;

namespace BlogApi.Repository.Interface;

public interface ITokenRepository
{
    Task CreateToken(AccessTokenEntity accessTokenEntity);
    Task<AccessTokenEntity?> GetToken(String token);
}