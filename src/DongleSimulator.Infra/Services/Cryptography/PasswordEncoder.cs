using System.Security.Cryptography;
using System.Text;
using DongleSimulator.Domain.Services.Cryptography;

namespace DongleSimulator.Infra.Services.Cryptography;

public class PasswordEncoder : IPasswordEncoder
{
    private readonly string _key;

    public PasswordEncoder(string key) => _key = key;
    
    public string Encrypt(string password)
    {
        var acPassword = $"{password}{_key}";

        var bytes = Encoding.UTF8.GetBytes(acPassword);
        var hash = SHA512.HashData(bytes);

        return BytesToString(hash);
    }

    private static string BytesToString(byte[] bytes)
    {
        var sb = new StringBuilder();
        foreach (var b in bytes)
        {
            var hex = b.ToString("x2");
            sb.Append(hex);
        }
        
        return sb.ToString();
    }
}