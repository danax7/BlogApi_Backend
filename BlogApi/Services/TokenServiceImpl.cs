using System.IdentityModel.Tokens.Jwt;
using BlogApi.Entity;
using BlogApi.Repository.Interface;
using BlogApi.Services.Interface;

namespace BlogApi.Services;

public class TokenServiceImpl : ITokenService
{
    private readonly ITokenRepository _tokenRepository;

    public TokenServiceImpl(ITokenRepository tokenRepository)
    {
        _tokenRepository = tokenRepository;
    }


    public async Task BlockAccessToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var expiredDate = handler.ReadToken(token).ValidTo;

        var tokenEntity = new AccessTokenEntity
        {
            token = token,
            expiryDate = expiredDate,
            id = Guid.NewGuid()
        };

        await _tokenRepository.CreateToken(tokenEntity);
    }
}