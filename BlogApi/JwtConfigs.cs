using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BlogApi;

public class JwtConfigs
{
    public static string Issuer { get; private set; }
    public static string Audience { get; private set; }
    private static string AccessKey { get; }
    public static int AccessLifetime { get; private set; }

    static JwtConfigs()
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        Issuer = configuration.GetValue<string>("Jwt:Issuer");
        Audience = configuration.GetValue<string>("Jwt:Audience");
        AccessKey = configuration.GetValue<string>("Jwt:AccessKey");
        AccessLifetime = configuration.GetValue<int>("Jwt:AccessLifetime");
    }

    public static SymmetricSecurityKey GetSymmetricSecurityAccessKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AccessKey));
    }
}