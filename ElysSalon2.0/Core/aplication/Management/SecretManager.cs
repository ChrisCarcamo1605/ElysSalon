using Microsoft.Extensions.Configuration;

namespace ElysSalon2._0.Core.aplication.Management;

public class SecretManager
{
    private static readonly IConfiguration _configuration;

    static SecretManager()
    {
        var builder = new ConfigurationBuilder();

        builder.AddUserSecrets<SecretManager>();

        _configuration = builder.Build();
    }

    public static string? GetValue(string key)
    {
        return _configuration[key];
    }
}