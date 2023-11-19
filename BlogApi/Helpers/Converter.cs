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
    public static String NormalizeString(String input)
    {
        return input.ToLower().Trim();
    }


    public static String GetTokenFromContext(AuthorizationHandlerContext context)
    {
        var headerDictionary = context.Resource as DefaultHttpContext;
        var token = headerDictionary?.Request.Headers["Authorization"];
        return GetToken(token);
    }

    public static Guid GetId(ClaimsPrincipal user)
    {
        if (Guid.TryParse(user.Identity?.Name, out Guid id))
        {
            return id;
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
}