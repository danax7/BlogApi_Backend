namespace BlogApi.Services.Interface;

public interface ITokenService
{
    Task BlockAccessToken(string token);
}