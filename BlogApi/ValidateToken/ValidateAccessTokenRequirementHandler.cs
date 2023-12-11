using System.IdentityModel.Tokens.Jwt;
using BlogApi.Exception;
using BlogApi.Helpers;
using BlogApi.Repository.Interface;
using Microsoft.AspNetCore.Authorization;

namespace BlogApi.ValidateToken;

public class ValidateAccessTokenRequirementHandler : AuthorizationHandler<ValidateAccessTokenRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITokenRepository _tokenRepository;

    public ValidateAccessTokenRequirementHandler(IHttpContextAccessor httpContextAccessor,
        ITokenRepository tokenRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _tokenRepository = tokenRepository;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        ValidateAccessTokenRequirement requirement)
    {
        if (_httpContextAccessor.HttpContext != null)
        {
            var token = Converter.GetTokenFromContext(context);

            var tokenEntity = await _tokenRepository.GetToken(token);

            if (tokenEntity != null)
            {
                throw new NotAuthorizedException("Not authorized");
            }
            
            //TODO:Валидация времени жизни токена
            // var handler = new JwtSecurityTokenHandler();
            // var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            //
            // if (jsonToken != null && jsonToken.ValidTo < DateTime.UtcNow)
            // {
            //     throw new NotAuthorizedException("Token has expired");
            // }
            
            
            context.Succeed(requirement);
        }
        else
        {
            throw new System.Exception("Bad request");
        }
    }
}