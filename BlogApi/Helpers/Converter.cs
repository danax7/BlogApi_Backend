using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text.RegularExpressions;
using BlogApi.Exception;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Primitives;

namespace BlogApi.Helpers;

public class Converter
{

    public static String GetTokenFromContext(HttpContext httpContext)
    {
        var headerDictionary = httpContext.Request.Headers;
        var token = headerDictionary["Authorization"];
        return GetToken(token);
    }

    public static String GetTokenFromContext(AuthorizationHandlerContext context)
    {
        var headerDictionary = context.Resource as DefaultHttpContext;
        var token = headerDictionary?.Request.Headers["Authorization"];
        return GetToken(token);
    }

    public static Guid GetId(HttpContext context)
    {
        var token = GetToken(context.Request.Headers["Authorization"]);
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(token);

        var claims = jwtSecurityToken.Claims;
        foreach (var claim in claims)
        {
            if (claim.Type == ClaimsIdentity.DefaultNameClaimType)
            {
                return Guid.Parse(claim.Value);
            }
        }

        throw new InvalidCredentialException("Cannot parse user id");
    }

    private static String GetToken(StringValues? token)
    {
        if (string.IsNullOrWhiteSpace(token.Value))
            throw new NotAuthorizedException("Authorization token is empty");

        const String tokenPattern = @"\S+\.\S+\.\S+";
        var match = Regex.Matches(token, tokenPattern);

        if (match.Count == 0)
            throw new NotAuthorizedException("Authorization token is not valid");

        return match[0].Value;
    }
    
    private static String GetPossiblyNullableTokenFromContext(StringValues? token)
    {
        if (string.IsNullOrWhiteSpace(token.Value))
        {
            return null;
        }

        const String tokenPattern = @"\S+\.\S+\.\S+";
        var match = Regex.Matches(token, tokenPattern);

        if (match.Count == 0)
        {
            return null;
        }

        return match[0].Value;
    }
    
    public static Guid? GetPossiblyNullableTokenId(HttpContext context)
    {
        var token = GetPossiblyNullableTokenFromContext(context.Request.Headers["Authorization"]);

        if (string.IsNullOrWhiteSpace(token))
        {
            return null;
        }

        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(token);

        var claims = jwtSecurityToken.Claims;
        foreach (var claim in claims)
        {
            if (claim.Type == ClaimsIdentity.DefaultNameClaimType)
            {
                return Guid.Parse(claim.Value);
            }
        }

        return null;
    }
}